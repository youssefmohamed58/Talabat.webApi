using System.Collections.Generic;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Talabat.Apis.DTOS;
using Talabat.Apis.Helpers;
using Talabat.Core;
using Talabat.Core.Entities;
using Talabat.Core.Repositories;
using Talabat.Core.Specification;
using Talabat.Core.Specification.ProductSpecification;

namespace Talabat.Apis.Controllers
{

    public class ProductsController : BaseController
    {
        private readonly IUnitOfWork unitofwork;
        private readonly IMapper _mapper;

        public ProductsController(IUnitOfWork _unitofwork, IMapper mapper)
        {
            unitofwork = _unitofwork;
            _mapper = mapper;
  
        }
        [CachedAttribute(300)]
        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<Product>>> GetAllProducts([FromQuery]ProductSpecParams Params)
        {
            //var products = await _productRepo.GetAllAsync();
            var spec = new ProductSpecification(Params);
            var products = await unitofwork.Repositories<Product>().GetAllWithSpecAsync(spec);
            var ProductsDto = _mapper.Map<IReadOnlyList<Product>, IReadOnlyList<ProductToReturnDto>>(products);
            var CountSpec = new ProductCountSpecification(Params);
            var count = await unitofwork.Repositories<Product>().GetCountwithSpecAsync(CountSpec);
            var ResponseWithPymtagination = new Pagination<ProductToReturnDto>(Params.PageSize, Params.PageIndex, ProductsDto , count);
            return Ok(ResponseWithPymtagination);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            //var product = await _productRepo.GetByIdAsync(id);
            var spec = new ProductSpecification(id);
            var products = await unitofwork.Repositories<Product>().GetByIdWithSpecAsync(spec);
            var ProductsDto = _mapper.Map<Product, ProductToReturnDto>(products);

            return Ok(ProductsDto);
        }

        [HttpGet("Types")]
        public async Task<ActionResult<IReadOnlyList<ProductType>>> GetAllTypes()
        {
            var Types = await unitofwork.Repositories<ProductType>().GetAllAsync();
            return Ok(Types);
        }

        [HttpGet("Brands")]
        public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetAllBrands()
        {
            var Brands = await unitofwork.Repositories<ProductBrand>().GetAllAsync();
            return Ok(Brands);
        }
    }
}
