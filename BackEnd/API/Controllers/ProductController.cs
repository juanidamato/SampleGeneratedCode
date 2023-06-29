using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SampleGeneratedCodeApplication.Features.Products.Queries;

namespace SampleGeneratedCodeAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProductController(IMediator mediator)
        {
            _mediator = mediator;  
        }

        [HttpGet]
        [Route("api/product/{id}")]
        public async Task<ActionResult<GetProductByIdQueryResponse>> GetProductbyId(string id)
        {
            GetProductByIdQuery query = new GetProductByIdQuery();
            GetProductByIdQueryResponse result;
            query.Id = id;

            result=await _mediator.Send(query);
            if (result==null)
            {
                return NotFound();
            }
            return Ok(result);

        }
    }
}
