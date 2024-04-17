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

		public ProductsController(IGenericRepository<Product> productRepository, IMapper mapper)
		{
			_productRepository = productRepository;
			_mapper = mapper;
		}

		[HttpGet]
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
		public async Task<ActionResult<ProductToReturnDTO>> GetProduct(int id)
		{
			//var product = await _productRepository.GetAsync(id);
			var productSpecs = new ProductSpecifications(id);
			var product = await _productRepository.GetWithSpecAsync(productSpecs);
			if (product is null)
				return NotFound(new ApiResponse(404));
			
			return Ok(_mapper.Map < Product,ProductToReturnDTO>(product));
		}
	}
}
