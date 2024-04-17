using Me.Talabt.Core.Entities;
using Me.Talabt.Core.Repositories;
using Me.Talabt.Core.Specifications;
using Microsoft.AspNetCore.Mvc;

namespace Me.Talabat.APIs.Controllers
{

	public class ProductsController : BaseApiController
	{
		private readonly IGenericRepository<Product> _productRepository;

		public ProductsController(IGenericRepository<Product> productRepository)
		{
			_productRepository = productRepository;
		}

		[HttpGet]
		public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
		{
			//var products = await _productRepository.GetAllAsync();
			var productSpecs = new ProductSpecifications();
			var products = await _productRepository.GetAllWithSpecsAsync(productSpecs);
			if (products is null)
				return NotFound(new { statusCode = 404, message = "NotFound" });
			
			return Ok(products);
		}


		[HttpGet("{id}")]
		public async Task<ActionResult<Product>> GetProduct(int id)
		{
			//var product = await _productRepository.GetAsync(id);
			var productSpecs = new ProductSpecifications(id);
			var product = await _productRepository.GetWithSpecAsync(productSpecs);
			if (product is null)
				return NotFound(new { statusCode = 404, message = "NotFound" });

			return Ok(product);
		}
	}
}
