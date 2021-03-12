// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Jeebs.Reflection;

namespace Jeebs.Data.Querying
{
	/// <inheritdoc cref="IQueryParameters"/>
	public sealed class QueryParameters : Dictionary<string, object>, IQueryParameters
	{
		/// <inheritdoc/>
		public bool TryAdd(object? parameters)
		{
			// Stop int / long / char / etc being added as parameters
			if (parameters?.GetType().IsPrimitive != false)
			{
				return false;
			}
			// Merge another IQueryParameters with this one 
			else if (parameters is IQueryParameters queryParameters)
			{
				foreach (var p in queryParameters)
				{
					Add(p.Key, p.Value);
				}

				return true;
			}
			// Handle anonymous / standard objects
			else if (getProperties() is var objectProperties && objectProperties.Any())
			{
				foreach (var p in objectProperties)
				{
					var name = p.Name;
					if (p.GetValue(parameters) is object value)
					{
						Add(name, value);
					}
				}

				return true;
			}

			return false;

			// Get all publicly-readable properties
			IEnumerable<PropertyInfo> getProperties() =>
				from p in parameters.GetProperties()
				where p.MemberType == MemberTypes.Property
				&& p.GetMethod?.IsPublic == true
				&& p.GetMethod?.GetParameters().Length == 0 // exclude index get accessors e.g. this[1]
				select p;
		}

		/// <summary>
		/// Return parameters as JSON
		/// </summary>
		public override string ToString() =>
			F.JsonF.Serialise(this);
	}
}
