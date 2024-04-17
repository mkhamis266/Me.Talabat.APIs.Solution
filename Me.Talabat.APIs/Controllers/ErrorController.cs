using Me.Talabat.APIs.Errors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Me.Talabat.APIs.Controllers
{
	[Route("errors/{code}")]
	[ApiController]
	[ApiExplorerSettings(IgnoreApi =true)]
	public class ErrorController : ControllerBase
	{
		public ActionResult GetError(int code)
		{
			if (code == 404)
				return NotFound(new ApiResponse(404));
			else if (code == 401)
				return Unauthorized(new ApiResponse(401));
			else if (code == 400)
				return BadRequest(new ApiResponse(400));
			else
				return BadRequest();
		}
	}
}
