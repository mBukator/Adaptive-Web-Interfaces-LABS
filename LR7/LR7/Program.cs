using Asp.Versioning;
using LR7.Context.Database;
using LR7.Services.Auth;
using LR7.Services.HealthCheck;
using LR7.Services.ParkingSections;
using LR7.Services.ParkingSpaces;
using LR7.Services.PasswordHash;
using LR7.Services.Users;
using LR7.Services.Vehicles;
using LR7.SwaggerConfig;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using HealthChecks.UI.Client;
using HealthChecks.UI.Configuration;
using System.Text;
using Serilog;

var builder = WebApplication.CreateBuilder(args);


// SERILOG
Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .CreateLogger();

builder.Host.UseSerilog();

// REGISTRATING DB
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<MyDatabaseContext>(options => options.UseSqlServer(connectionString));



// HEALTH CHECKS
builder.Services.AddHealthChecks()
    .AddDbContextCheck<MyDatabaseContext>("dbcontext_health_check")
    .AddCheck<DatabaseHealthCheck>("custom_db_health_check", tags: new[] { "db", "sql" })
    .AddTypeActivatedCheck<UserHealthCheck>("user_health_check", args: new object[] { "Token type - JWT Bearer" });

builder.Services.AddScoped<DatabaseHealthCheck>(_ => new DatabaseHealthCheck(connectionString));


builder.Services.AddHealthChecksUI(options => {
    options.SetEvaluationTimeInSeconds(10);
    options.MaximumHistoryEntriesPerEndpoint(60);
    options.SetApiMaxActiveRequests(1);
    options.AddHealthCheckEndpoint("User Health Check", "/api/user-health");
    options.AddHealthCheckEndpoint("Database Context Health Check", "/api/dbcontext-health");
    options.AddHealthCheckEndpoint("Custom Database Health Check", "/api/custom-db-health");
}).AddInMemoryStorage();




// SERVICES
builder.Services.AddScoped<LR7.V1.Services.IGenerateMethodService, LR7.V1.Services.GenerateMethodService>();
builder.Services.AddScoped<LR7.V2.Services.IGenerateMethodService, LR7.V2.Services.GenerateMethodService>();
builder.Services.AddScoped<LR7.V3.Services.IGenerateMethodService, LR7.V3.Services.GenerateMethodService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IVehicleService, VehicleService>();
builder.Services.AddScoped<IParkingSectionService, ParkingSectionService>();
builder.Services.AddScoped<IParkingSpaceService, ParkingSpaceService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IPasswordHashService, HashPasswordService>();



// AUTHENTICATION
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options => {
    options.TokenValidationParameters = new TokenValidationParameters {
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JwtSettings:Key"])),
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateIssuerSigningKey = true,
    };
});



//  SWAGGER
builder.Services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();
builder.Services.AddSwaggerGen(c => {

    c.OperationFilter<SwaggerDefaultValues>();

    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme {
        Description = "Enter JWT Bearer token",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
    }
    );

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme {
                Reference = new OpenApiReference {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[]{ }
        }
    });
});

builder.Services.AddApiVersioning(options => {
    options.DefaultApiVersion = new ApiVersion(2, 0);
    options.AssumeDefaultVersionWhenUnspecified = true;
    options.ReportApiVersions = true;
    options.ApiVersionReader = new UrlSegmentApiVersionReader();
}).AddApiExplorer(options => {
    options.GroupNameFormat = "'v'VVV";
    options.SubstituteApiVersionInUrl = true;
});



builder.Services.AddControllers();


builder.Services.AddProblemDetails();


builder.Services.AddEndpointsApiExplorer();




var app = builder.Build();



if (app.Environment.IsDevelopment()) {
    app.UseSwagger();
    app.UseSwaggerUI(options => {
        var descs = app.DescribeApiVersions();

        foreach (var desc in descs) {
            var url = $"/swagger/{desc.GroupName}/swagger.json";
            var name = desc.GroupName.ToUpperInvariant();
            Console.WriteLine(url + " " + name);
            options.SwaggerEndpoint(url, name);
        }
    });

    app.UseHealthChecksUI(opt => {
        opt.UIPath = "/health";
    });

}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();


// SERILOG
app.UseSerilogRequestLogging();



// HEALTH CHECKS
app.MapHealthChecks("/api/user-health", new HealthCheckOptions {
    Predicate = healthCheck => healthCheck.Name == "user_health_check",
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse,
});

app.MapHealthChecks("/api/dbcontext-health", new HealthCheckOptions {
    Predicate = healthCheck => healthCheck.Name == "dbcontext_health_check",
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse,
});

app.MapHealthChecks("/api/custom-db-health", new HealthCheckOptions {
    Predicate = healthCheck => healthCheck.Name == "custom_db_health_check",
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse,
});


app.Run();
