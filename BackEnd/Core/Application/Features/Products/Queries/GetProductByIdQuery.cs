using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using FluentValidation;
using SampleGeneratedCodeDomain.Entities;
using SampleGeneratedCodeApplication.Commons.Utils;
using SampleGeneratedCodeApplication.Commons.Interfaces.Repositories;
using SampleGeneratedCodeDomain.Commons;
using System.Diagnostics;
using SampleGeneratedCodeDomain.Enums;

namespace SampleGeneratedCodeApplication.Features.Products.Queries
{
    //Request
    public class GetProductByIdQuery : IRequest<GetProductByIdQueryResponse>
    {
        public string Id { get; set; } = string.Empty;

    }

    //ViewModel
    public class GetProductByIdQueryViewModel
    {
        public string IdProduct { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string IdCategory { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public string? Notes { get; set; }
    }

    //Response
    public class GetProductByIdQueryResponse : OperationResultModel<GetProductByIdQueryViewModel>
    {

    }

    //Mapper Entity to Response
    public class GetProductByIdQueryMapper : Profile
    {
        public GetProductByIdQueryMapper()
        {
            CreateMap<ProductEntity, GetProductByIdQueryViewModel>()
                .ForMember(dest => dest.IdProduct, opt => opt.MapFrom(map => map.IdProduct))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(map => map.Description))
                .ForMember(dest => dest.IdCategory, opt => opt.MapFrom(map => map.IdCategory))
                .ForMember(dest => dest.Price, opt => opt.MapFrom(map => map.Price * 2))
                .ForMember(dest => dest.Notes, opt => opt.MapFrom(map => map.Notes));
        }
    }

    //Validator
    public class GetProductByIdQueryValidator : AbstractValidator<GetProductByIdQuery>
    {
        public GetProductByIdQueryValidator()
        {
            RuleFor(x => x.Id)
             .NotEmpty().WithMessage("You must supply Id Product")
             .MaximumLength(20).WithMessage("Id length must be less than 20 characters")
             .Must(StringUtils.ValidateLatinCharacters).WithMessage("Id contains invalid characters");
        }
    }

    //Handler
    public class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQuery, GetProductByIdQueryResponse>
    {
        private readonly IMapper _mapper;
        private readonly IProductRepository _productRepo;

        public GetProductByIdQueryHandler(IMapper mapper, IProductRepository productRepo)
        {
            _mapper = mapper;
            _productRepo = productRepo;
        }

        public async Task<GetProductByIdQueryResponse> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
        {
            GetProductByIdQueryResponse response = new GetProductByIdQueryResponse();
            ProductEntity? oneProduct;
            bool bolR;
            GetProductByIdQueryValidator validator = new GetProductByIdQueryValidator();

            try
            {
                var validationResult = validator.Validate(request);
                if (!validationResult.IsValid)
                {
                    response.code = OperationResultCodesEnum.BAD_REQUEST;
                    response.message = "Invalid request";
                    return response;
                }

                (bolR, oneProduct) = await _productRepo.GetByIdAsync(request.Id);
                if (!bolR)
                {
                    response.code = OperationResultCodesEnum.SERVER_ERROR;
                    response.message = "Error getting product by id";
                    return response;
                }
                if (oneProduct is null)
                {
                    response.code = OperationResultCodesEnum.NOT_FOUND;
                    response.message = "Product not found";
                    return response;
                }
                response.code = OperationResultCodesEnum.OK;
                response.message = "";
                response.payload = _mapper.Map<GetProductByIdQueryViewModel>(oneProduct);
                return response;
            }
            catch(Exception ex)
            {
                //todo
                response.code = OperationResultCodesEnum.SERVER_ERROR;
                response.message = "Exception getting product by id";
                return response;
            }
           
        }
    }


}
