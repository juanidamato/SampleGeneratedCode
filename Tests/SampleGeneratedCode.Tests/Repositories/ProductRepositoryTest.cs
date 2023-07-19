using Microsoft.Extensions.Logging;
using Moq;
using SampleGeneratedCodeApplication.BLL;
using SampleGeneratedCodeInfrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleGeneratedCode.Tests.Repositories
{
    public  class ProductRepositoryTest
    {
        public ProductRepositoryTest()
        {
                
        }

        [Fact]
        public async Task ProductRepository_GetProductById_ValidId_Return_Product()
        {
            //arrange
            Mock<ILogger<ProductRepository>> logger = new Mock<ILogger<ProductRepository>>();

        }
    }
}
