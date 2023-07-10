using Castle.DynamicProxy;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Identity.Web;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using SampleGeneratedCodeApplication;
using SampleGeneratedCodeApplication.BLL;
using SampleGeneratedCodeApplication.Commons.Attributes;
using SampleGeneratedCodeApplication.Commons.Interfaces.BLL;
using SampleGeneratedCodeApplication.Commons.Interfaces.Infrastructure;
using SampleGeneratedCodeApplication.Commons.Interfaces.Repositories;
using SampleGeneratedCodeApplication.Commons.Interfaces.Utils;
using SampleGeneratedCodeApplication.Commons.Utils;
using SampleGeneratedCodeInfrastructure;
using SampleGeneratedCodeInfrastructure.Repositories;
using Serilog;
using Serilog.Core;
using Serilog.Events;
using System.Text;

namespace SampleGeneratedCodeAPI
{
    public class Program
    {
        private static LoggingLevelSwitch? _levelSwitch;

        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);


            _levelSwitch = new LoggingLevelSwitch();
            Console.WriteLine($"Environment:{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}");
            if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Development")
            {
                _levelSwitch.MinimumLevel = LogEventLevel.Information;
            }
            else
            {
                _levelSwitch.MinimumLevel = LogEventLevel.Warning;
            }

            builder.Host.UseSerilog();

            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.ControlledBy(_levelSwitch)
                .Enrich.FromLogContext()
                .WriteTo.Console()
                .CreateLogger();

            try
            {
                Log.Information("Starting Sample generated code with clean architecture...");

                // Add services to the container.
                // Setup Interception
                builder.Services.AddSingleton(new ProxyGenerator());
                builder.Services.AddTransient<IInterceptor, TraceAndTimeAttibuteInterceptor>();

                //utils
                builder.Services.AddSingleton<IReverseHash, ReverseHash>();


                builder.Services.AddSampleGeneratedCodeApplication();

                //persistence helper
                builder.Services.AddTransient<IDatabaseHelper, SQLDatabaseHelper>();

                //repositories
                builder.Services.AddProxiedTransient<IProductRepository, ProductRepository>();

                //managers
                builder.Services.AddProxiedTransient<IProductManager, ProductManager>();


                builder.Services
                    .AddHttpContextAccessor()
                    .AddAuthorization()
                    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                    .AddJwtBearer(options =>
                    {
                        options.TokenValidationParameters = new TokenValidationParameters
                        {
                            ValidateIssuer = true,
                            ValidateAudience = true,
                            ValidateLifetime = true,
                            ValidateIssuerSigningKey = true,
                            ValidIssuer = builder.Configuration["Jwt:Issuer"],
                            ValidAudience = builder.Configuration["Jwt:Audience"],
                            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
                        };
                    });

                builder.Services.AddRequiredScopeAuthorization();

                //other services
                builder.Services.AddControllers();
                // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
                builder.Services.AddEndpointsApiExplorer();
                builder.Services.AddSwaggerGen(c =>
                {
                    c.SwaggerDoc("v1", new OpenApiInfo
                    {
                        Title = "My API",
                        Version = "v1"
                    });
                    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                    {
                        In = ParameterLocation.Header,
                        Description = "Please insert JWT with Bearer into field",
                        Name = "Authorization",
                        Type = SecuritySchemeType.ApiKey
                    });
                    c.AddSecurityRequirement(new OpenApiSecurityRequirement {
                       {
                         new OpenApiSecurityScheme
                         {
                           Reference = new OpenApiReference
                           {
                             Type = ReferenceType.SecurityScheme,
                             Id = "Bearer"
                           }
                          },
                          new string[] { }
                        }
                      });
                });

                var app = builder.Build();

                string? salt = app.Configuration.GetValue<string>("HashIdsSalt");
                if (salt is null)
                {
                    salt = "should define a salt";
                }
                IReverseHash rh = app.Services.GetRequiredService<IReverseHash>();
                rh.Init(salt);


                // Configure the HTTP request pipeline.
                if (app.Environment.IsDevelopment())
                {
                    app.UseSwagger();
                    app.UseSwaggerUI();
                }

                app.UseHttpsRedirection();

                app.UseAuthentication();
                app.UseAuthorization();


                app.MapControllers();

                app.Run();

            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Host terminated unexpectedly");
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        public static void changeLogLevel(LogEventLevel newLogLevel)
        {
            _levelSwitch!.MinimumLevel = newLogLevel;
        }
    }
}