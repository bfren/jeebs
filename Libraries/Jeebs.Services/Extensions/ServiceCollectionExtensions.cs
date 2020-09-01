using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Microsoft.Extensions.DependencyInjection;

namespace Jeebs.Services
{
	/// <summary>
	/// <see cref="IServiceCollection"/> Extensions - AddDrivers
	/// </summary>
	public static class ServiceCollectionExtensions
	{
		/// <summary>
		/// Add IDriver<> and IDriverArgs<> implementations to the <see cref="IServiceCollection"/>
		/// </summary>
		/// <param name="this">IServiceCollection</param>
		public static IServiceCollection AddDrivers(this IServiceCollection @this)
		{
			// Get implementations of a type with a generic parameter
			IEnumerable<Type> GetImplementations(Type typeWithGenericParam)
			{
				return from a in AppDomain.CurrentDomain.GetAssemblies()
					   from t in a.GetLoadableTypes()
					   from i in t.GetInterfaces()
					   where !t.IsAbstract && !t.IsInterface
					   && i.IsGenericType && i.GetGenericTypeDefinition() == typeWithGenericParam
					   select t;
			}

			// Get drivers and add them to the service collection
			foreach (var t in GetImplementations(typeof(IDriver<>)))
			{
				@this.AddSingleton(t);

				// Add required services
				const BindingFlags flags = BindingFlags.NonPublic
					| BindingFlags.Public
					| BindingFlags.Static
					| BindingFlags.FlattenHierarchy;

				if (t.GetMethod("AddRequiredServices", flags) is MethodInfo addRequiredServices)
				{
					addRequiredServices.Invoke(null, new object[] { @this });
				}
			}

			// Get driver args and add them to the service collection
			foreach (var t in GetImplementations(typeof(IDriverArgs<>)))
			{
				@this.AddTransient(t);
			}

			// Return service collection
			return @this;
		}
	}
}
