using AutoMapper;
using Me.Talabat.APIs.DTOs;
using Me.Talabt.Core.Entities;

namespace Me.Talabat.APIs.Helpers
{
	public class MappingProfiles: Profile
	{
		public MappingProfiles() 
		{
			CreateMap<Product, ProductToReturnDTO>()
				.ForMember(P => P.Brand, O => O.MapFrom(S => S.Brand.Name))
				.ForMember(P => P.Category, O => O.MapFrom(S => S.Category.Name))
				.ForMember(P => P.PictureUrl, O => O.MapFrom<ProductPictureUrlResolver>());
		}
	}
}
