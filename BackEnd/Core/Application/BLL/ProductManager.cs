﻿using Microsoft.Extensions.Logging;
using SampleGeneratedCodeApplication.Commons.Attributes;
using SampleGeneratedCodeApplication.Commons.Interfaces.BLL;
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

namespace SampleGeneratedCodeApplication.BLL
{
    public class ProductManager : IProductManager
    {
        private readonly ILogger<ProductManager> _logger;
        private readonly IProductRepository _productRepo;

        public ProductManager(ILogger<ProductManager> logger,
                              IProductRepository productRepo)
        {
            _logger = logger;
            _productRepo = productRepo;
        }

        [TraceAndTime]
        public async Task<(OperationStatusModel,ProductEntity?)> GetProductById(GetProductByIdQuery request)
        {
            OperationStatusModel response = new OperationStatusModel();
            ProductEntity? oneProduct;
            bool bolR;
            GetProductByIdQueryValidator validator = new GetProductByIdQueryValidator();

            try
            {
                var validationResult = validator.Validate(request);
                if (!validationResult.IsValid)
                {
                    response.code = OperationResultCodes.BAD_REQUEST;
                    response.message = "Invalid request";
                    return (response,null);
                }

                (bolR, oneProduct) = await _productRepo.GetByIdAsync(request.Id);
                if (!bolR)
                {
                    response.code = OperationResultCodes.SERVER_ERROR;
                    response.message = "Error getting product by id";
                    return (response, null);
                }
                if (oneProduct is null)
                {
                    response.code = OperationResultCodes.NOT_FOUND;
                    response.message = "Product not found";
                    return (response, null);
                }
                response.code = OperationResultCodes.OK;
                response.message = "Product found";
                return (response, oneProduct);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception GetProductById");
                response.code = OperationResultCodes.SERVER_ERROR;
                response.message = "Exception getting product by id";
                return (response, null);
            }
        }
    }
}
