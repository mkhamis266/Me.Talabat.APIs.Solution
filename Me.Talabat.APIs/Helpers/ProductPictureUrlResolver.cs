using AutoMapper;
using Me.Talabat.APIs.DTOs;
using Me.Talabt.Core.Entities;

namespace Me.Talabat.APIs.Helpers
{
	public class ProductPictureUrlResolver : IValueResolver<Product, ProductToReturnDTO, string>
	{
		private readonly IConfiguration _configs;

		public ProductPictureUrlResolver(IConfiguration configs)
		{
			_configs = configs;
		}
		public string Resolve(Product source, ProductToReturnDTO destination, string destMember, ResolutionContext context)
		{
			if (!string.IsNullOrEmpty(source.PictureUrl))
				return $"{_configs["ApiBaseUrl"]}/{source.PictureUrl}";
			return string.Empty;
		}
	}
}
