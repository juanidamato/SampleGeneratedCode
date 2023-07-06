using SampleGeneratedCodeApplication;
using SampleGeneratedCodeApplication.BLL;
using SampleGeneratedCodeApplication.Commons.Interfaces.BLL;
using SampleGeneratedCodeApplication.Commons.Interfaces.Infrastructure;
using SampleGeneratedCodeApplication.Commons.Interfaces.Repositories;
using SampleGeneratedCodeInfrastructure;
using SampleGeneratedCodeInfrastructure.Repositories;

namespace SampleGeneratedCodeAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddSampleGeneratedCodeApplication();




            //persistence helper
            builder.Services.AddTransient<IDatabaseHelper, SQLDatabaseHelper>();


            //repositories
            builder.Services.AddTransient<IProductRepository, ProductRepository>();

            //managers
            builder.Services.AddTransient<IProductManager, ProductManager>();




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
    }
}