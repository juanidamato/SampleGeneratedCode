using Microsoft.Extensions.Configuration;
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
      
        private readonly IDatabaseHelper _db;

        public ProductRepository(IDatabaseHelper db)
        {
            _db = db;
        }

        public async Task<ProductEntity?> GetByIdAsync(string id)
        {
            var r= await _db.GetArrayDataAsync<ProductEntity,dynamic>( "Product_GetById", new { IdProduct = id });
            if (r is  null)
            {
                return null;
            }
            var element = r.FirstOrDefault<ProductEntity?>();
            return element;

        }
    }
}
