using Me.Talabat.InfraStructure.Data;
using Me.Talabt.Core.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Me.Talabat.APIs.Controllers
{
	public class BuggyController : BaseApiController
	{
		private readonly ApplicationDbContext _dbcontext;

		public BuggyController(ApplicationDbContext dbcontext)
		{
			_dbcontext = dbcontext;
		}

		[HttpGet("notfound")]
		public ActionResult GetNotFoundRequest()
		{
			var Product = _dbcontext.Set<Product>().Find(100);
			if (Product is not null)
				return Ok(Product);

			return NotFound();
		}

		[HttpGet("servererror")]
		public ActionResult GetServerError()
		{
			var Product = _dbcontext.Set<Product>().Find(100);
			var ProductToReturn = Product.ToString();
			return Ok(ProductToReturn);
		}
		[HttpGet("badrequest")]
		public ActionResult GetBaddRequest()
		{
			return BadRequest();
		}
		[HttpGet("badrequest/{id}")] //badrequest/five
		public ActionResult GetBaddRequest(int id)
		{
			return Ok();
		}
		[HttpGet("unauthorized")] //badrequest/five
		public ActionResult GetUnauthorized(int id)
		{
			return Unauthorized();
		}
	}
}
