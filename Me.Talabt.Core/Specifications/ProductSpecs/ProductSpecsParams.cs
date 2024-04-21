using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Me.Talabt.Core.Specifications.ProductSpecs
{
	public class ProductSpecsParams
	{
		public string? Sort {  get; set; }
		public int? BrandId { get; set; }
		public int? CategoryId { get; set; }
		public int PageIndex { get; set; } = 1;

		private int padeSize = 5;

		public int PageSize
		{
			get { return padeSize; }
			set { padeSize = padeSize > 10 ? 5: value; }
		}

	}
}
