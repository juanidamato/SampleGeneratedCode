using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using SampleGeneratedCodeApplication.Commons.Attributes;
using SampleGeneratedCodeApplication.Commons.Interfaces.Infrastructure;
using SampleGeneratedCodeApplication.Commons.Interfaces.Repositories;
using SampleGeneratedCodeDomain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleGeneratedCodeInfrastructure.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ILogger<ProductRepository> _logger;
        private readonly IDatabaseHelper _db;

        public ProductRepository(ILogger<ProductRepository> logger,
                                 IDatabaseHelper db)
        {
            _logger = logger;
            _db = db;
        }

        [TraceAndTime]
        public async Task<(bool,ProductEntity?)> GetByIdAsync(string id)
        {
            try
            {
                var r = await _db.GetArrayDataAsync<ProductEntity, dynamic>("Product_Select_ByPK", new { IdProduct = id });
             
                var element = r.FirstOrDefault<ProductEntity?>();
                return (true,element);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception Error in GetByIdAsync");
                return (false, null);
            }
            
           
        }
    }
}
