﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Me.Talabt.Core.Entities;

namespace Me.Talabt.Core.Specifications
{
	public class ProductSpecifications : BaseSpecifications<Product>
	{
		public ProductSpecifications()
		{
			AddIncludes();
		}
		public ProductSpecifications(int id) : base(P => P.Id == id)
		{
			AddIncludes();
		}

		private void AddIncludes()
		{
			Includes.Add(P => P.Brand);
			Includes.Add(P => P.Category);
		}
	}
}
