// Jeebs Rapid Application Development
// Copyright (c) bfren.uk - licensed under https://mit.bfren.uk/2013

using System;
using System.Collections.Generic;
using System.Linq;

namespace Jeebs
{
	/// <summary>
	/// AppDomain Extensions
	/// </summary>
	public static class AppDomainExtensions
	{
		/// <summary>
		/// Get distinct property types that implement type <typeparamref name="T"/>
		/// </summary>
		/// <typeparam name="T">Property Type</typeparam>
		/// <param name="this">AppDomain</param>
		public static List<Type> GetTypesOfPropertiesImplenting<T>(this AppDomain @this)
		{
			var types = from a in @this.GetAssemblies()
						from t in a.GetTypes()
						from p in t.GetProperties()
						where p.PropertyType.Implements<T>()
						&& !p.PropertyType.IsAbstract
						&& !p.PropertyType.IsInterface
						&& !p.PropertyType.IsGenericParameter
						select p.PropertyType;

			return types.Distinct().ToList();
		}
	}
}
