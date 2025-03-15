using Humanizer;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Microsoft.OpenApi.Writers;
using Swashbuckle.AspNetCore.Swagger;
using System.Reflection;
using System.Text;
using Vestis._01_Application.Mapping;
using Vestis._01_Application.Services;
using Vestis.Configurations;
using Vestis.Data;

#region builder

var builder = WebApplication.CreateBuilder(args);
var logger = builder.Services.BuildServiceProvider().GetRequiredService<ILogger<Program>>();

builder.Services.RegisterAllScopedDependencies(logger);
builder.Services.AddAutoMapper(typeof(MappingProfile));

// Add services to the container.
builder.Services.AddControllers();

//Configurar JWT
ConfigureJWT();

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
//builder.Services.AddOpenApi();

AddDatabse();
AddSwagger();

builder.Services.AddSingleton<JwtService>();

#endregion Builder

#region build app
var app = builder.Build();

// Verifica se foi passado um argumento para gerar o YAML
if (args.Length > 0 && args.Any(arg => arg.Equals("generate_yaml", StringComparison.OrdinalIgnoreCase)))
{
    GenerateYaml();
    return;
}

// Configure the HTTP request pipeline.
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

if (app.Environment.IsDevelopment())
{
    //app.MapOpenApi();
    UseSwagger();
}

System.Console.WriteLine("       ____   ____               __  .__            _____ __________.___ \r\n       \\   \\ /   /____   _______/  |_|__| ______   /  _  \\\\______   \\   |\r\n        \\   Y   // __ \\ /  ___/\\   __\\  |/  ___/  /  /_\\  \\|     ___/   |\r\n         \\     /\\  ___/ \\___ \\  |  | |  |\\___ \\  /    |    \\    |   |   |\r\n          \\___/  \\___  >____  > |__| |__/____  > \\____|__  /____|   |___|\r\n                     \\/     \\/               \\/          \\/\n");
System.Console.WriteLine(app.Environment.EnvironmentName+"\n");
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
                Console.WriteLine($"[Authentication failed] {DateTime.Now.TimeOfDay}\n" + PrintExceptionStack(context.Exception,out _));
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

string PrintExceptionStack(Exception exception, out int order)
{
    order = 0;
    if (exception.InnerException == null)
    {
        order = 1;
        return $"[{order.Ordinalize()}] {exception.Message}";
    }

    var stackedMessages = PrintExceptionStack(exception.InnerException, out order);
    order++;

    return $"{stackedMessages}\n\n[{order.Ordinalize()}] {exception.Message}\n";
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
        /*
        //Habilita comentários XML
        var xmlFile = $"{System.Reflection.Assembly.GetExecutingAssembly().GetName().Name}.xml";
        var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);

        options.IncludeXmlComments(xmlPath);
        */
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

void AddDatabse() => builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

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

#endregion methods

public static class ServiceCollectionExtensions
{
    public static void RegisterAllScopedDependencies(this IServiceCollection services, ILogger logger)
    {
        var assembly = Assembly.GetExecutingAssembly();

        var types = assembly.GetTypes();
        var interfaces = types.Where(type => type.IsInterface && type.Name.StartsWith("I")).ToList();

        foreach (var interfaceType in interfaces)
        {
            var implementationType = types.FirstOrDefault(type => type.IsClass && !type.IsAbstract && type.Name.Equals(interfaceType.Name.Substring(1)));
            if (implementationType == null)
            {
                logger.LogWarning($"[DI] No implementation found for interface {interfaceType.Name}");
                continue;
            }
            services.AddScoped(interfaceType, implementationType);
            logger.LogInformation($"[DI] Registrando: {interfaceType}, {implementationType}");
        }
    }
}