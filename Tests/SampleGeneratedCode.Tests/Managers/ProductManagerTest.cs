using Microsoft.Extensions.Logging;
using Moq;
using SampleGeneratedCodeApplication.BLL;
using SampleGeneratedCodeApplication.Commons.Interfaces.Repositories;
using SampleGeneratedCodeApplication.Features.Products.Queries;
using SampleGeneratedCodeDomain.Commons;
using SampleGeneratedCodeDomain.Entities;
using SampleGeneratedCodeDomain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleGeneratedCode.Tests.Managers
{
    public class ProductManagerTest
    {
        [Fact]
        public async Task ProductManager_GetProductById_ValidId_Return_Product()
        {
            //arrange
            Mock<ILogger<ProductManager>> logger = new Mock<ILogger<ProductManager>>();
            Mock<IProductRepository> repo = new Mock<IProductRepository>();
            OperationStatusModel op;
            ProductEntity prod1 = new ProductEntity();
            ProductEntity? prod2;
            GetProductByIdQuery request = new GetProductByIdQuery();

            prod1.IdProduct = "prod01";
            prod1.Description = "product";
            prod1.IdCategory = "cat1";
            prod1.Notes = null;
            prod1.Price = null;

            repo.Setup(m => m.GetByIdAsync(It.IsAny<string>()))
                .Returns(Task.FromResult<(bool, ProductEntity?)>((true, prod1)));

            request.Id = "prod01";
            var sut = new ProductManager(logger.Object, repo.Object);

            //act
            (op, prod2) = await sut.GetProductById(request);

            //assert
            Assert.Equal(OperationResultCodes.OK, op.code);
        }
        
        [Fact]
        public async Task ProductManager_GetProductById_InValidId_Return_Error()
        {
            //arrange
            Mock<ILogger<ProductManager>> logger = new Mock<ILogger<ProductManager>>();
            Mock<IProductRepository> repo = new Mock<IProductRepository>();
            OperationStatusModel op;

            ProductEntity? prod2;
            GetProductByIdQuery request = new GetProductByIdQuery();

            repo.Setup(m => m.GetByIdAsync(It.IsAny<string>()))
                .Returns(Task.FromResult<(bool, ProductEntity?)>((true, null)));

            request.Id = "prod01%%";
            var sut = new ProductManager(logger.Object, repo.Object);

            //act
            (op, prod2) = await sut.GetProductById(request);

            //assert
            Assert.Equal(OperationResultCodes.BAD_REQUEST, op.code);
        }

        [Fact]
        public async Task ProductManager_GetProductById_IdNotFound_Return_Error()
        {
            //arrange
            Mock<ILogger<ProductManager>> logger = new Mock<ILogger<ProductManager>>();
            Mock<IProductRepository> repo = new Mock<IProductRepository>();
            OperationStatusModel op;

            ProductEntity? prod2;
            GetProductByIdQuery request = new GetProductByIdQuery();

            repo.Setup(m => m.GetByIdAsync(It.IsAny<string>()))
                .Returns(Task.FromResult<(bool, ProductEntity?)>((true, null)));

            request.Id = "prod01";
            var sut = new ProductManager(logger.Object, repo.Object);

            //act
            (op, prod2) = await sut.GetProductById(request);

            //assert
            Assert.Equal(OperationResultCodes.NOT_FOUND, op.code);
        }

        [Fact]
        public async Task ProductManager_GetProductById_ServerError_Return_Error()
        {
            //arrange
            Mock<ILogger<ProductManager>> logger = new Mock<ILogger<ProductManager>>();
            Mock<IProductRepository> repo = new Mock<IProductRepository>();
            OperationStatusModel op;

            ProductEntity? prod2;
            GetProductByIdQuery request = new GetProductByIdQuery();

            repo.Setup(m => m.GetByIdAsync(It.IsAny<string>()))
                .Returns(Task.FromResult<(bool, ProductEntity?)>((false, null)));

            request.Id = "prod01";
            var sut = new ProductManager(logger.Object, repo.Object);

            //act
            (op, prod2) = await sut.GetProductById(request);

            //assert
            Assert.Equal(OperationResultCodes.SERVER_ERROR, op.code);
        }

        [Fact]
        public async Task ProductManager_GetProductById_Exception_Return_Error()
        {
            //arrange
            Mock<ILogger<ProductManager>> logger = new Mock<ILogger<ProductManager>>();
            Mock<IProductRepository> repo = new Mock<IProductRepository>();
            OperationStatusModel op;

            ProductEntity? prod2;
            GetProductByIdQuery request = new GetProductByIdQuery();

            repo.Setup(m => m.GetByIdAsync(It.IsAny<string>()))
                .Throws(new Exception("Mock Exception"));

            request.Id = "prod01";
            var sut = new ProductManager(logger.Object, repo.Object);

            //act
            (op, prod2) = await sut.GetProductById(request);

            //assert
            Assert.Equal(OperationResultCodes.SERVER_ERROR, op.code);
        }
    }
}
