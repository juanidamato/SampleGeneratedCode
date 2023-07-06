using Castle.DynamicProxy;
using SampleGeneratedCodeApplication;
using SampleGeneratedCodeApplication.BLL;
using SampleGeneratedCodeApplication.Commons.Attributes;
using SampleGeneratedCodeApplication.Commons.Interfaces.BLL;
using SampleGeneratedCodeApplication.Commons.Interfaces.Infrastructure;
using SampleGeneratedCodeApplication.Commons.Interfaces.Repositories;
using SampleGeneratedCodeInfrastructure;
using SampleGeneratedCodeInfrastructure.Repositories;
using Serilog;
using Serilog.Core;
using Serilog.Events;

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

                builder.Services.AddSampleGeneratedCodeApplication();

                //persistence helper
                builder.Services.AddTransient<IDatabaseHelper, SQLDatabaseHelper>();

                //repositories
                builder.Services.AddProxiedTransient<IProductRepository, ProductRepository>();

                //managers
                builder.Services.AddProxiedTransient<IProductManager, ProductManager>();

                //other services
                builder.Services.AddControllers();
                // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
                builder.Services.AddEndpointsApiExplorer();
                builder.Services.AddSwaggerGen();

                var app = builder.Build();

                // Configure the HTTP request pipeline.
                if (app.Environment.IsDevelopment())
                {
                    app.UseSwagger();
                    app.UseSwaggerUI();
                }

                app.UseHttpsRedirection();

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