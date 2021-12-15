using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
        public ProductsController(IGenericRepository<Product> ProductsRepo
            , IGenericRepository<ProductType> ProductTypeRepo
            , IGenericRepository<ProductBrand> ProductBrandRepo)
        {
            _ProductsRepo = ProductsRepo;
            _ProductTypeRepo = ProductTypeRepo;
            _ProductBrandRepo = ProductBrandRepo;
        }
        [HttpGet]
        public async Task<ActionResult<List<Product>>> GetProducts()
        {
            var spec = new ProductTypeAndBrandSpecifications();
            var Products=await _ProductsRepo.ListAsync(spec);
            return Ok(Products);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            var spec= new ProductTypeAndBrandSpecifications(id);
            return await _ProductsRepo.GetEntityWithSpec(spec);
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
