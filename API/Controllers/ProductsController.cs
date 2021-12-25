using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.DTO;
using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using Core.Specifications;
using Infrastructure.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IGenericRepository<Product> _ProductsRepo;
        private readonly IGenericRepository<ProductType> _ProductTypeRepo;
        private readonly IGenericRepository<ProductBrand> _ProductBrandRepo;
        private readonly IMapper _mapper;
        public ProductsController(IGenericRepository<Product> ProductsRepo
            , IGenericRepository<ProductType> ProductTypeRepo
            , IGenericRepository<ProductBrand> ProductBrandRepo
            , IMapper mapper)
        {
            _ProductsRepo = ProductsRepo;
            _ProductTypeRepo = ProductTypeRepo;
            _ProductBrandRepo = ProductBrandRepo;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<ProductToReturnDto>>> GetProducts()
        {
            var spec = new ProductTypeAndBrandSpecifications();
            var Products=await _ProductsRepo.ListAsync(spec);
            return Ok(_mapper.Map<IReadOnlyList<Product>, IReadOnlyList<ProductToReturnDto>>(Products));
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductToReturnDto>> GetProduct(int id)
        {
            var spec= new ProductTypeAndBrandSpecifications(id);
            var product = await _ProductsRepo.GetEntityWithSpec(spec);
            return _mapper.Map<Product, ProductToReturnDto>(product);
        }
        [HttpGet("Brands")]
        public async Task<ActionResult<List<ProductBrand>>> GetProductBrands()
        {
            var ProductBrands =await _ProductBrandRepo.ListAllAsync();
            return Ok(ProductBrands);
        }
        [HttpGet("Types")]
        public async Task<ActionResult<List<ProductBrand>>> GetProductTypes()
        {
            var ProductTypes = await _ProductTypeRepo.ListAllAsync();
            return Ok(ProductTypes);
        }
    }
}
