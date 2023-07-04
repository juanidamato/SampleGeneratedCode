using SampleGeneratedCodeDomain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleGeneratedCodeApplication.Commons.Interfaces.Repositories
{
    public interface IProductRepository
    {
        public Task<ProductEntity?> GetByIdAsync(string id);
    }
}
