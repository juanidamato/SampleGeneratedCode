﻿using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Web.Resource;
using SampleGeneratedCodeAPI.Utils;
using SampleGeneratedCodeApplication.Commons.Interfaces.Utils;
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

        [Authorize]
        [RequiredScope("api.access")]
        [HttpGet]
        [Route("api/product/{id}")]
        public async Task<ActionResult> GetProductbyId(string id)
        {
            GetProductByIdQuery query = new GetProductByIdQuery();
            GetProductByIdQueryResponse result;
            HttpMapperResultUtil mapperResultUtil = new();
            query.Id = id;

            result = await _mediator.Send(query);
            return mapperResultUtil.MapToActionResult(result);

        }

       
    }
}
