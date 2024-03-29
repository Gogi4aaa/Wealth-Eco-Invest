﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wealth_Eco_Invest.Web.Infrastructure.ModelBinders
{
	using Microsoft.AspNetCore.Mvc.ModelBinding;

	public class DecimalModelBinderProvider : IModelBinderProvider
	{
		public IModelBinder GetBinder(ModelBinderProviderContext context)
		{
			if (context == null)
			{
				throw new ArgumentNullException(nameof(context));
			}

			if (context.Metadata.ModelType == typeof(decimal) ||
			    context.Metadata.ModelType == typeof(decimal?))
			{
				return new DecimalModelBinder();
			}

			return null!;
		}
	}
}
