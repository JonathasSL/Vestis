using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
if (builder.Environment.IsDevelopment())
{
    builder.Services.AddCors(options =>
    {
        options.AddPolicy(name: _allowSpecificOrigins,
            policy =>
            {
                policy.AllowAnyOrigin()
                    //.WithOrigins("development.internal")
                    .AllowAnyHeader()
                    .AllowAnyMethod();
            });
    });

    builder.WebHost.ConfigureKestrel(serverOptions =>
    {
        serverOptions.ListenAnyIP(7232, listenOptions =>
        {
            listenOptions.UseHttps();
            Console.WriteLine($"Listening in: {listenOptions.IPEndPoint.Address} port: {listenOptions.IPEndPoint.Port}");
        });
    });
}

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddHttpClient();

ConfigureJWT();

AddDatabse();

AddSwagger();

builder.Services.AddSingleton<JwtService>();

AddCQRS();
#endregion Builder

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

app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

if (app.Environment.IsDevelopment())
{
    UseSwagger();
    app.UseCors(_allowSpecificOrigins);
}

System.Console.WriteLine("       ____   ____               __  .__            _____ __________.___ \r\n       \\   \\ /   /____   _______/  |_|__| ______   /  _  \\\\______   \\   |\r\n        \\   Y   // __ \\ /  ___/\\   __\\  |/  ___/  /  /_\\  \\|     ___/   |\r\n         \\     /\\  ___/ \\___ \\  |  | |  |\\___ \\  /    |    \\    |   |   |\r\n          \\___/  \\___  >____  > |__| |__/____  > \\____|__  /____|   |___|\r\n                     \\/     \\/               \\/          \\/\n");
System.Console.WriteLine($"EnvironmentName: {app.Environment.EnvironmentName}\n");
app.Run();
#endregion build app

#region methods

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

void UseSwagger()
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Vestis API V1");
        c.RoutePrefix = string.Empty;
    });
}

void AddDatabse() 
    => builder.Services.AddDbContext<ApplicationDbContext>(
        options => options.UseSqlServer(
            builder.Configuration.GetConnectionString("DefaultConnection"),
            b => b.MigrationsAssembly("Vestis._04_Infrasctructure"))
    );

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
#endregion methods
