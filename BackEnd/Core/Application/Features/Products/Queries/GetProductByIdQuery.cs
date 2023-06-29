﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using FluentValidation;
using SampleGeneratedCodeDomain.Entities;
using SampleGeneratedCodeApplication.Commons.Utils;


namespace SampleGeneratedCodeApplication.Features.Products.Queries
{
    //Request
    public class GetProductByIdQuery: IRequest<GetProductByIdQueryResponse>
    {
        public string Id { get; set; } = string.Empty;

    }

    //Response
    public class GetProductByIdQueryResponse
    {
        public string IdProduct { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string IdCategory { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public string? Notes { get; set; }
    }

    //Mapper Entity to Response
    public class GetProductByIdQueryMapper : Profile
    {
        public GetProductByIdQueryMapper()
        {
            CreateMap<ProductEntity, GetProductByIdQueryResponse>()
                .ForMember(dest => dest.IdProduct, opt => opt.MapFrom(map => map.IdProduct))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(map => map.Description))
                .ForMember(dest => dest.IdCategory, opt => opt.MapFrom(map => map.IdCategory))
                .ForMember(dest => dest.Price, opt => opt.MapFrom(map => map.Price*2))
                .ForMember(dest => dest.Notes, opt => opt.MapFrom(map => map.Notes));
        }
    }

    //Validator
    public class GetProductByIdQueryValidator:AbstractValidator<GetProductByIdQuery>
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

        public GetProductByIdQueryHandler(IMapper mapper)
        {
            _mapper = mapper;
        }

        public async Task<GetProductByIdQueryResponse> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
        {
            await Task.Delay(1);
            GetProductByIdQueryValidator validator = new GetProductByIdQueryValidator();
           
            var validationResult=validator.Validate(request);
            if(!validationResult.IsValid)
            {
                //todo
                await Task.Delay(1);
            }

            ProductEntity oneProduct = new ProductEntity();
            oneProduct.IdProduct = "prod1";
            oneProduct.Description = "producto 1";
            oneProduct.IdCategory = "cat1";
            oneProduct.Price = 1500.25M;
            oneProduct.Notes = "interesting notes";
            return _mapper.Map<GetProductByIdQueryResponse>(oneProduct);
        }
    }


}
