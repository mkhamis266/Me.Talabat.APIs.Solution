using Me.Talabat.APIs.Errors;
using Me.Talabat.APIs.Helpers;
using Me.Talabat.InfraStructure;
using Me.Talabt.Core.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;

namespace Me.Talabat.APIs.Extentions
{
	public static class ApplicationServicesExtention
	{
		public static IServiceCollection AddApplicationService(this IServiceCollection services)
		{
			services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
			services.AddAutoMapper(typeof(MappingProfiles));

			services.Configure<ApiBehaviorOptions>(options =>
			{
				options.InvalidModelStateResponseFactory = (actionContext) =>
				{
					var errors = actionContext.ModelState.Where(P => P.Value.Errors.Count > 0)
															.SelectMany(P => P.Value.Errors)
															.Select(P => P.ErrorMessage)
															.ToArray();
					var response = new ValidationErrorApiResponse()
					{
						Errors = errors
					};
					return new BadRequestObjectResult(response);
				};
			});
			return services;
		}
	}
}
