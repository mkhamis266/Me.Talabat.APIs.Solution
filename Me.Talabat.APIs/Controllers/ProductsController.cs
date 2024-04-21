using System.Net;
using AutoMapper;
using Me.Talabat.APIs.DTOs;
using Me.Talabat.APIs.Errors;
using Me.Talabat.APIs.Pagination;
using Me.Talabt.Core.Entities;
using Me.Talabt.Core.Repositories;
using Me.Talabt.Core.Specifications.ProductSpecs;
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
		[ProducesResponseType(typeof(IReadOnlyList<ProductToReturnDTO>), StatusCodes.Status200OK)]
		[ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
		public async Task<ActionResult<IReadOnlyList<ProductToReturnDTO>>> GetProducts([FromQuery] ProductSpecsParams specs)
		{
			//var products = await _productRepository.GetAllAsync();0
			var productSpecs = new ProductsWithBrandAndCategorySpecifications(specs);
			var products = await _productRepository.GetAllWithSpecsAsync(productSpecs);

			if (products is null)
				return NotFound(new ApiResponse(404));
			var data = _mapper.Map<IReadOnlyList<Product>, IReadOnlyList<ProductToReturnDTO>>(products);
			var productsSpecsForCount = new ProductsWithFiltersForCountSpecifications(specs);
			var count = await _productRepository.GetCount(productsSpecsForCount);
			var paginatedProducts = new Pagination<ProductToReturnDTO>(specs.PageIndex,specs.PageSize,count,data);
			return Ok(paginatedProducts);
		}


		[HttpGet("{id}")]
		[ProducesResponseType(typeof(ProductToReturnDTO), 200)]
		[ProducesResponseType(typeof(ApiResponse), 404)]
		public async Task<ActionResult<ProductToReturnDTO>> GetProduct(int id)
		{
			//var product = await _productRepository.GetAsync(id);
			var productSpecs = new ProductsWithBrandAndCategorySpecifications(id);
			var product = await _productRepository.GetWithSpecAsync(productSpecs);
			if (product is null)
				return NotFound(new ApiResponse(404));
			
			return Ok(_mapper.Map < Product,ProductToReturnDTO>(product));
		}

		[HttpGet("brands")]
		public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetBrands()
		{
			var brands = await _productBrandsRepo.GetAllAsync();
			return Ok(brands);
		}
		
		[HttpGet("categories")]
		public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetCategories()
		{
			var brands = await _productCategoriesRepo.GetAllAsync();
			return Ok(brands);
		}

	}
}
