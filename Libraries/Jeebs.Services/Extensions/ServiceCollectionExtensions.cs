using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Extensions.DependencyInjection;

namespace Jeebs.Services
{
	public static class ServiceCollectionExtensions
	{
		public static IServiceCollection AddDrivers(this IServiceCollection @this)
		{
			var types = from a in AppDomain.CurrentDomain.GetAssemblies()
						from t in a.GetLoadableTypes()
						from i in t.GetInterfaces()
						where !t.IsAbstract && !t.IsInterface
						&& i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IDriver<>)
						select t;

			foreach (var t in types.ToList())
			{
				@this.AddSingleton(t);
			}

			return @this;
		}
	}
}
