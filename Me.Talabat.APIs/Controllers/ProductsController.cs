﻿using System.Net;
using AutoMapper;
using Me.Talabat.APIs.DTOs;
using Me.Talabat.APIs.Errors;
using Me.Talabt.Core.Entities;
using Me.Talabt.Core.Repositories;
using Me.Talabt.Core.Specifications;
using Microsoft.AspNetCore.Mvc;

namespace Me.Talabat.APIs.Controllers
{

	public class ProductsController : BaseApiController
	{
		private readonly IGenericRepository<Product> _productRepository;
		private readonly IMapper _mapper;
		private readonly IGenericRepository<ProductBrand> _productBrandsRepo;
		private readonly IGenericRepository<ProductCategory> _productCategoriesRepo;

		public ProductsController(IGenericRepository<Product> productRepository, IMapper mapper,IGenericRepository<ProductBrand> productBrandsRepo,IGenericRepository<ProductCategory> productCategoriesRepo)
		{
			_productRepository = productRepository;
			_mapper = mapper;
			_productBrandsRepo = productBrandsRepo;
			_productCategoriesRepo = productCategoriesRepo;
		}

		[HttpGet]
		[ProducesResponseType(typeof(IEnumerable<ProductToReturnDTO>), StatusCodes.Status200OK)]
		[ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
		public async Task<ActionResult<IEnumerable<ProductToReturnDTO>>> GetProducts()
		{
			//var products = await _productRepository.GetAllAsync();0
			var productSpecs = new ProductSpecifications();
			var products = await _productRepository.GetAllWithSpecsAsync(productSpecs);

			if (products is null)
				return NotFound(new ApiResponse(404));
			var result = _mapper.Map<IEnumerable<Product>, IEnumerable<ProductToReturnDTO>>(products);
			return Ok(result);
		}


		[HttpGet("{id}")]
		[ProducesResponseType(typeof(ProductToReturnDTO), 200)]
		[ProducesResponseType(typeof(ApiResponse), 404)]
		public async Task<ActionResult<ProductToReturnDTO>> GetProduct(int id)
		{
			//var product = await _productRepository.GetAsync(id);
			var productSpecs = new ProductSpecifications(id);
			var product = await _productRepository.GetWithSpecAsync(productSpecs);
			if (product is null)
				return NotFound(new ApiResponse(404));
			
			return Ok(_mapper.Map < Product,ProductToReturnDTO>(product));
		}

		[HttpGet("brands")]
		public async Task<ActionResult<IEnumerable<ProductBrand>>> GetBrands()
		{
			var brands = await _productBrandsRepo.GetAllAsync();
			return Ok(brands);
		}
		
		[HttpGet("categories")]
		public async Task<ActionResult<IEnumerable<ProductBrand>>> GetCategories()
		{
			var brands = await _productCategoriesRepo.GetAllAsync();
			return Ok(brands);
		}

	}
}
