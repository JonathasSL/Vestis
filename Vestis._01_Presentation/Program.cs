using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Proxies;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Microsoft.OpenApi.Writers;
using Swashbuckle.AspNetCore.Swagger;
using System.Text;
using Vestis._02_Application;
using Vestis._02_Application.Behavior;
using Vestis._02_Application.Common;
using Vestis._02_Application.Configurations;
using Vestis._02_Application.Mapping;
using Vestis._02_Application.Services;
using Vestis._04_Infrasctructure.Data;
using Vestis.Shared.Extensions;

#region builder

var builder = WebApplication.CreateBuilder(args);
var logger = builder.Services.BuildServiceProvider().GetRequiredService<ILogger<Program>>();

builder.Services.RegisterAllScopedDependencies(logger);
builder.Services.AddAutoMapper(typeof(MappingProfile));

var _allowSpecificOrigins = "_allowCORS";

builder.Services.AddCors(options =>
{
	options.AddPolicy(name: _allowSpecificOrigins, policy =>
	{
		if (builder.Environment.IsDevelopment())
		{
			policy.AllowAnyOrigin()
				.AllowAnyHeader()
				.AllowAnyMethod();
			return;
		}

		var corsOriginsRaw = builder.Configuration["CORS_ORIGINS"];
		var origins = string.IsNullOrWhiteSpace(corsOriginsRaw)
			? ["https://vestis-frontend.vercel.app"]
			: corsOriginsRaw.Split(';', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);

		policy.WithOrigins(origins)
			.AllowAnyHeader()
			.AllowAnyMethod();
	});
});

// Configuração: sempre prioriza env var (Azure). Se não existir, usa appsettings (local).
if (builder.Environment.IsDevelopment())
{
	builder.Configuration
		.AddJsonFile("appsettings.Development.json", optional: true, reloadOnChange: true)
		.AddEnvironmentVariables();

	builder.WebHost.ConfigureKestrel(serverOptions =>
	{
		// LAN-friendly endpoints for development
		// - HTTP avoids local network HTTPS certificate issues when accessing by IP
		// - HTTPS keeps parity with local dev scenarios
		serverOptions.ListenAnyIP(5209, listenOptions =>
		{
			Console.WriteLine($"Listening in: {listenOptions.IPEndPoint.Address} port: {listenOptions.IPEndPoint.Port} (HTTP)");
		});

		serverOptions.ListenAnyIP(7232, listenOptions =>
		{
			listenOptions.UseHttps();
			Console.WriteLine($"Listening in: {listenOptions.IPEndPoint.Address} port: {listenOptions.IPEndPoint.Port} (HTTPS)");
		});
	});
}
else
{
	builder.Configuration.AddEnvironmentVariables();
}

var connectionString =
	builder.Configuration["AZURE_SQL_CONNECTIONSTRING"]
	?? builder.Configuration.GetConnectionString("DefaultConnection");

if (string.IsNullOrWhiteSpace(connectionString))
	throw new InvalidOperationException(
		"Connection string não configurada. Defina a env var 'AZURE_SQL_CONNECTIONSTRING' (prioritário) ou configure 'ConnectionStrings:DefaultConnection' em appsettings*.json.");

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddHttpClient();

ConfigureJWT();

AddDatabse(connectionString);

AddSwagger();

builder.Services.AddSingleton<JwtService>();

AddCQRS();
#endregion Builder

#region builder methods
void AddDatabse(string connectionString)
	=> builder.Services.AddDbContext<ApplicationDbContext>(
	 options => options
		.UseLazyLoadingProxies()
		.UseSqlServer(
			connectionString,
			b => b.MigrationsAssembly("Vestis._04_Infrasctructure"))
	);

void ConfigureJWT()
{
	var jwtSettings = new JwtSettings();
	builder.Configuration.GetSection("JwtSettings").Bind(jwtSettings);
	builder.Services.AddSingleton(jwtSettings);

	var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.SecretKey));

	builder.Services.AddAuthentication(options =>
	{
		options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
		options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
	}).AddJwtBearer(options =>
	{
		options.TokenValidationParameters = new TokenValidationParameters
		{
			ValidateIssuer = true,
			ValidateAudience = true,
			ValidateLifetime = true,
			ValidateIssuerSigningKey = true,
			ValidIssuer = jwtSettings.Issuer,
			ValidAudience = jwtSettings.Audience,
			IssuerSigningKey = key
		};
		options.Events = new JwtBearerEvents
		{
			OnAuthenticationFailed = context =>
			{
				Console.WriteLine($"[Authentication failed] {DateTime.Now.TimeOfDay}\n" + context.Exception.ExceptionStack(out _));
				return Task.CompletedTask;
			},
			OnTokenValidated = context =>
			{
				Console.WriteLine("[Token validated]\n" + context.SecurityToken);
				return Task.CompletedTask;
			}
		};
	});
	builder.Services.AddAuthorization();
}

void AddSwagger()
{
	//Add Swagger
	builder.Services.AddEndpointsApiExplorer();
	builder.Services.AddSwaggerGen(options =>
	{
		options.SwaggerDoc("v1", new OpenApiInfo
		{
			Title = "Vestis API",
			Version = "v1",
			Description = "Manage your tailoring",
			Contact = new OpenApiContact
			{
				Name = "Vestis",
				Email = "contact@vestis.com",
				Url = new Uri("https://vestis.com")
			}
		});
	});
}

void AddCQRS()
{
	// Adiciona MediatR — busca todos os Handlers no Application
	builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(ApplicationAssemblyMarker).Assembly));

	// Pipeline global
	builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
	builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(TransactionBehavior<,>));

	// Adiciona o contexto de notificação de negócios
	builder.Services.AddScoped<BusinessNotificationContext>();
}
#endregion builder methods

#region build app
var app = builder.Build();

if (args.Length > 0)
{
    // Verifica se foi passado um argumento para gerar o YAML
    if (args.Any(arg => arg.Equals("generate_yaml", StringComparison.OrdinalIgnoreCase)))
    {
        GenerateYaml();
        return;
    }
}


if (app.Environment.IsProduction())
    app.UseHttpsRedirection();

// CORS deve vir antes de autenticação/autorização e endpoints.
app.UseCors(_allowSpecificOrigins);

app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

if (app.Environment.IsDevelopment())
{
    UseSwagger();
}

System.Console.WriteLine(@"       ____   ____               __  .__            _____ __________.___ 
       \   \ /   /____   _______/  |_|__| ______   /  _  \______   \   |
        \   Y   // __ \ /  ___/\   __\  |/  ___/  /  /_\  \|     ___/   |
         \     /\  ___/ \___ \  |  | |  |\___ \  /    |    \    |   |   |
          \___/  \___  >____  > |__| |__/____  > \____|__  /____|   |___|
                     \/     \/               \/          \/
");
System.Console.WriteLine($"EnvironmentName: {app.Environment.EnvironmentName}");
System.Console.WriteLine($"Application started at {DateTime.Now}\n");

app.Run();
#endregion build app

#region build app methods
void UseSwagger()
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Vestis API V1");
        c.RoutePrefix = string.Empty;
    });
}

void GenerateYaml()
{
        // Obtém o serviço responsável por gerar a documentação Swagger
        var swaggerProvider = app.Services.GetRequiredService<ISwaggerProvider>();
        var swaggerDoc = swaggerProvider.GetSwagger("v1");

        // Serializa para YAML
        var stringWriter = new StringWriter();
        swaggerDoc.SerializeAsV3(new OpenApiYamlWriter(stringWriter));
        var yamlOutput = stringWriter.ToString();

        // Define o caminho para salvar o arquivo dentro do projeto
        var directoryPath = Path.Combine(app.Environment.ContentRootPath, "wwwroot", "swagger");
        var filePath = Path.Combine(directoryPath, "api-spec.yaml");

        // Garante que o diretório existe
        if (!Directory.Exists(directoryPath))
            Directory.CreateDirectory(directoryPath);

        // Salva o arquivo
        File.WriteAllText(filePath, yamlOutput);

        Console.WriteLine($"Arquivo YAML gerado em: {filePath}");
        return; // Encerra a aplicação após gerar o YAML
}

#endregion build app methods
