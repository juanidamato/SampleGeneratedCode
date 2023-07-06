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
using SampleGeneratedCodeApplication.Commons.Interfaces.BLL;

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
                .ForMember(dest => dest.Price, opt => opt.MapFrom(map => map.Price ))
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
        private readonly IProductManager _productManager;

        public GetProductByIdQueryHandler(IMapper mapper,
                                          IProductManager productManager)
        {
            _mapper = mapper;;
            _productManager = productManager;
        }

        public async Task<GetProductByIdQueryResponse> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
        {
            GetProductByIdQueryResponse response=new GetProductByIdQueryResponse();
            OperationStatusModel operation;
            ProductEntity? oneProduct;
            (operation, oneProduct) = await _productManager.GetProductById(request);
            response.code = operation.code;
            response.message = operation.message;
            if (operation.code == OperationResultCodesEnum.OK)
            {
                response.payload = _mapper.Map<GetProductByIdQueryViewModel>(oneProduct);
            }

            return response;
        }

        
    }


}
