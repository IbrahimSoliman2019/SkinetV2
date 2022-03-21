using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Core.Specification;
using AutoMapper;
using API.DTOS;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IGenericRepository<Product> genericProductRepo;
        private readonly IGenericRepository<ProductBrand> genericBrandRepo;
        private readonly IGenericRepository<ProductType> genericTypeRepo;
        private readonly IMapper mapper;

        public ProductsController(IGenericRepository<Product> genericProductRepo,IGenericRepository<ProductBrand> genericBrandRepo, IGenericRepository<ProductType> genericTypeRepo
            ,IMapper mapper)
        {
            this.genericProductRepo = genericProductRepo;
            this.genericBrandRepo = genericBrandRepo;
            this.genericTypeRepo = genericTypeRepo;
            this.mapper = mapper;
        }

        [HttpGet]
        public async  Task<ActionResult<List<Product>>> GetProducts()
        {
            var spec = new ProductWithTypeAndBrandSpecification();
            var products =await genericProductRepo.ListAsyncBySpec(spec);
            return Ok(products);
        }

        [HttpGet("{id}")]
        public async Task<ProductToReturnDto> GetProduct(int id)
        {
            var spec = new ProductWithTypeAndBrandSpecification(id);

            var product = await genericProductRepo.GetEntityBySpec(spec);

            return mapper.Map<Product, ProductToReturnDto>(product);
        }

        [HttpGet("brands")]
        public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetBrands()
        {
            return Ok( await genericBrandRepo.ListAllAsync());
        }
        [HttpGet("types")]
        public async Task<ActionResult<IReadOnlyList<ProductType>>> GetTypes()
        {
            return Ok(await genericTypeRepo.ListAllAsync());
        }
    }
}
