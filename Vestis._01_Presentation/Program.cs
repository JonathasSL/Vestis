using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Microsoft.OpenApi.Writers;
using Swashbuckle.AspNetCore.Swagger;
using System.Reflection;
using System.Text;
using Vestis._01_Presentation.Swagger;
using Vestis._02_Application;
using Vestis._02_Application.Behavior;
using Vestis._02_Application.Common;
using Vestis._02_Application.Configurations;
using Vestis._02_Application.Mapping;
using Vestis._02_Application.Services;
using Vestis._04_Infrastructure.Data;
using Vestis._04_Infrastructure.Email;
using Vestis._04_Infrastructure.Email.Interfaces;
using Vestis._04_Infrastructure.Email.Settings;
using Vestis.Shared.Extensions;

var systemName = "Vestis.API";
Console.Title = systemName;

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
		if (builder.Environment.IsDevelopment() || builder.Environment.IsEnvironment("local"))
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
if (builder.Environment.IsEnvironment("local"))
{
	builder.Configuration
		.AddJsonFile("appsettings.local.json", optional: false, reloadOnChange: true)
		.AddEnvironmentVariables();


	builder.WebHost.ConfigureKestrel(serverOptions =>
	{
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
else if (builder.Environment.IsDevelopment())
{
	builder.Configuration
		.AddJsonFile("appsettings.development.json", optional: true, reloadOnChange: true)
		.AddEnvironmentVariables();
}
else if (builder.Environment.IsProduction())
{
	builder.Configuration
		.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
		.AddEnvironmentVariables();
}



var env = Environment.GetEnvironmentVariable("AZURE_SQL_CONNECTIONSTRING");

var connectionString =
	env.EmptyToNull();

if (string.IsNullOrWhiteSpace(connectionString))
	throw new InvalidOperationException(
		$"Connection string não configurada: '{connectionString}'. Defina a env var 'AZURE_SQL_CONNECTIONSTRING' (prioritário) ou configure 'ConnectionStrings:DefaultConnection' em appsettings*.json.");

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddHttpClient();

ConfigureJWT();

AddDatabse(connectionString);

AddSwagger();

builder.Services.AddSingleton<JwtService>();

AddCQRS();

ConfigureEmailService();
#endregion Builder

#region builder methods
void AddDatabse(string connectionString)
	=> builder.Services.AddDbContext<ApplicationDbContext>(
	 options => options
		.UseLazyLoadingProxies()
		.UseSqlServer(
			connectionString,
			b => b.MigrationsAssembly("Vestis._04_Infrastructure"))
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

		options.CustomSchemaIds(type => type.FullName);

		var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
		var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
		if (File.Exists(xmlPath))
			options.IncludeXmlComments(xmlPath);

		var bearerScheme = new OpenApiSecurityScheme
		{
			Name = "Authorization",
			Description = "JWT Authorization header usando o esquema Bearer. Ex: \"Bearer {token}\"",
			In = ParameterLocation.Header,
			Type = SecuritySchemeType.Http,
			Scheme = "bearer",
			BearerFormat = "JWT",
			Reference = new OpenApiReference
			{
				Type = ReferenceType.SecurityScheme,
				Id = "Bearer"
			}
		};

		options.AddSecurityDefinition("Bearer", bearerScheme);

		options.OperationFilter<AuthorizeCheckOperationFilter>();
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

void ConfigureEmailService()
{
	var emailSettings = builder.Configuration.GetSection("EmailSettings").Get<EmailSettings>();

	builder.Services.AddSingleton(emailSettings);


	if (builder.Environment.IsProduction())
		builder.Services.AddScoped<IEmailSender, AzureEmailSender>();
	else if (builder.Environment.IsEnvironment("Local"))
	{
		var localSettings = builder.Configuration.GetSection("EmailSettings:Local").Get<LocalEmailSettings>();
		builder.Services.AddSingleton(localSettings);
		builder.Services.AddScoped<IEmailSender, LocalEmailSender>();
	}
	else
		builder.Services.AddScoped<IEmailSender, LocalEmailSender>();


}
#endregion builder methods

#region build app
var app = builder.Build();



if (args.Length > 0)
{
	if (args.Any(arg => arg.Equals("generate_yaml", StringComparison.OrdinalIgnoreCase)))
	{
		GenerateYaml();
		Console.WriteLine($"[{systemName}] YAML file generated.");
	}
	if (args.Any(arg => arg.Equals("migrate_database", StringComparison.OrdinalIgnoreCase)))
	{
		using (var scope = app.Services.CreateScope())
		{
			var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
			dbContext.Database.Migrate();
			Console.WriteLine($"[{systemName}] Database migrated");
		}
	}
	return;
}
else if (!app.Environment.IsProduction() && !app.Environment.IsEnvironment("Local"))
{
	using (var scope = app.Services.CreateScope())
	{
		var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
		dbContext.Database.Migrate();
		app.Logger.LogInformation($"[{systemName}] Database migrated");
	}
	app.Logger.LogInformation($"Application started in {app.Environment.EnvironmentName} environment. Database migrated.");
}


if (app.Environment.IsProduction())
	app.UseHttpsRedirection();

// CORS deve vir antes de autenticação/autorização e endpoints.
app.UseCors(_allowSpecificOrigins);

app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

if (app.Environment.IsDevelopment() || app.Environment.IsEnvironment("local"))
{
	UseSwagger();
}

System.Console.WriteLine(
@"       ____   ____               __  .__            _____ __________.___ 
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
		c.RoutePrefix = "swagger";
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
	var directoryPath = Path.Combine(app.Environment.ContentRootPath, "artifacts", "openapi");
	var filePath = Path.Combine(directoryPath, "api-spec.v1.yaml");

	// Garante que o diretório existe
	if (!Directory.Exists(directoryPath))
		Directory.CreateDirectory(directoryPath);

	// Salva o arquivo
	File.WriteAllText(filePath, yamlOutput);

	Console.WriteLine($"Arquivo YAML gerado em: {filePath}");
}

#endregion build app methods
