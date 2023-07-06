using SampleGeneratedCodeApplication.Features.Products.Queries;
using SampleGeneratedCodeDomain.Commons;
using SampleGeneratedCodeDomain.Entities;

namespace SampleGeneratedCodeApplication.Commons.Interfaces.BLL
{
    public interface IProductManager
    {
        Task<(OperationStatusModel, ProductEntity?)> GetProductById(GetProductByIdQuery request);
    }
}