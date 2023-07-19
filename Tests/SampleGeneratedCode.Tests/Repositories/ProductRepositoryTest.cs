using Microsoft.Extensions.Logging;
using Moq;
using SampleGeneratedCodeApplication.BLL;
using SampleGeneratedCodeInfrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CliWrap;
using SampleGeneratedCodeApplication.Commons.Interfaces.Repositories;
using SampleGeneratedCodeApplication.Commons.Interfaces.Infrastructure;
using SampleGeneratedCodeInfrastructure;
using Microsoft.Extensions.Configuration;
using SampleGeneratedCodeDomain.Entities;

namespace SampleGeneratedCode.Tests.Repositories
{
    public  class ProductRepositoryTest
    {
        public ProductRepositoryTest()
        {
            var result1 = Task.Run(async () =>
            {
               var result2 =await CliWrap.Cli.Wrap(Path.Combine(GlobalVariables.SQLCopyBat, "copia.bat"))
                 .WithWorkingDirectory(GlobalVariables.SQLCopyBat)
                 .WithValidation(CommandResultValidation.None)
                 .ExecuteAsync();
                return result2;
            }).Result;
            Console.WriteLine($"Stop-Copy-Start-Result:{result1}");
        }

        private IConfiguration buildConfig()
        {
            var myConfiguration = new Dictionary<string, string?>
            {
                {"ConnectionStrings:Default",GlobalVariables.SQLLocalDB}
            };
            var configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(myConfiguration)
                .Build();
            return configuration;
        }

        [Fact]
        public async Task ProductRepository_GetProductById_ValidId_Return_Product()
        {
            //arrange
            Mock<ILogger<ProductRepository>> logger = new Mock<ILogger<ProductRepository>>();
            IConfiguration config = buildConfig();
            IDatabaseHelper db = new SqlDatabaseHelper(config);
            IProductRepository sut = new ProductRepository(logger.Object, db);
            ProductEntity? product;
            bool bolR;
            //act
            (bolR, product) = await sut.GetByIdAsync("prod01");

            //assert
            Assert.True( bolR);
            Assert.NotNull(product);
            Assert.True( string.Compare( "prod01", product.IdProduct,true)==0);
        }
        [Fact]
        public async Task ProductRepository_GetProductById_InValidId_Return_Null()
        {
            //arrange
            Mock<ILogger<ProductRepository>> logger = new Mock<ILogger<ProductRepository>>();
            IConfiguration config = buildConfig();
            IDatabaseHelper db = new SqlDatabaseHelper(config);
            IProductRepository sut = new ProductRepository(logger.Object, db);
            ProductEntity? product;
            bool bolR;
            //act
            (bolR, product) = await sut.GetByIdAsync("prod01xx");

            //assert
            Assert.True(bolR);
            Assert.Null(product);
        }
    }
}
