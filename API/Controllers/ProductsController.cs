using System.Collections.Generic;
using System.Threading.Tasks;
using API.DTOS;
using API.Errors;
using API.Helpers;
using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using Core.Specification;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class ProductsController : BaseApiController
    {
        private readonly IGenericRepository<Product> genericProductRepo;

        private readonly IGenericRepository<ProductBrand> genericBrandRepo;

        private readonly IGenericRepository<ProductType> genericTypeRepo;

        private readonly IMapper mapper;

        public ProductsController(
            IGenericRepository<Product> genericProductRepo,
            IGenericRepository<ProductBrand> genericBrandRepo,
            IGenericRepository<ProductType> genericTypeRepo,
            IMapper mapper
        )
        {
            this.genericProductRepo = genericProductRepo;
            this.genericBrandRepo = genericBrandRepo;
            this.genericTypeRepo = genericTypeRepo;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<Pagination<ProductToReturnDto>>>
        GetProducts([FromQuery] ProductSpecParams productSpecParams)
        {
            var spec =
                new ProductWithTypeAndBrandSpecification(productSpecParams);
            var specCount =
                new ProductWithFilterForCountSpecification(productSpecParams);
            var count = await genericProductRepo.CountAsync(specCount);
            var products = await genericProductRepo.ListAsyncBySpec(spec);
            var data =
                mapper
                    .Map
                    <IReadOnlyList<Product>, IReadOnlyList<ProductToReturnDto>
                    >(products);

            
             return Ok(new Pagination<ProductToReturnDto> (productSpecParams.PageSize,productSpecParams.PageIndex,count,data));
        }

        [HttpGet("{id}")]
        [
            ProducesResponseType(
                typeof (ApiErrorResponse),
                StatusCodes.Status404NotFound)
        ]
        public async Task<ActionResult<ProductToReturnDto>> GetProduct(int id)
        {
            var spec = new ProductWithTypeAndBrandSpecification(id);

            var product = await genericProductRepo.GetEntityBySpec(spec);

            if (product == null) return NotFound(new ApiErrorResponse(404));

            return mapper.Map<Product, ProductToReturnDto>(product);
        }

        [HttpGet("brands")]
        public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetBrands()
        {
            return Ok(await genericBrandRepo.ListAllAsync());
        }

        [HttpGet("types")]
        public async Task<ActionResult<IReadOnlyList<ProductType>>> GetTypes()
        {
            return Ok(await genericTypeRepo.ListAllAsync());
        }
    }
}
