// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.uk/2013

using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.Json;
using Jeebs.Reflection;

namespace Jeebs.Data.Querying
{
	/// <inheritdoc cref="IQueryParameters"/>
	public sealed class QueryParameters : Dictionary<string, object>, IQueryParameters
	{
		/// <inheritdoc/>
		public bool Merge(IQueryParameters parameters)
		{
			foreach (var p in parameters)
			{
				if (ContainsKey(p.Key))
				{
					return false;
				}

				Add(p.Key, p.Value);
			}

			return true;
		}

		/// <inheritdoc/>
		public bool TryAdd(object? parameters)
		{
			// Stop null / int / long / char / etc being added as parameters
			if (parameters?.GetType().IsPrimitive != false)
			{
				return false;
			}
			// Merge another IQueryParameters with this one 
			else if (parameters is IQueryParameters queryParameters)
			{
				return Merge(queryParameters);
			}
			// Handle anonymous / standard objects
			else if (getProperties() is var objectProperties && objectProperties.Any())
			{
				foreach (var p in objectProperties)
				{
					var name = p.Name;
					if (p.GetValue(parameters) is object value)
					{
						if (ContainsKey(name))
						{
							return false;
						}

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
			F.JsonF.Serialise(this, JsonSerializerOptions).Unwrap(GetType().ToString());

		/// <summary>
		/// Don't change parameter names when serialising to JSON
		/// </summary>
		internal static JsonSerializerOptions JsonSerializerOptions
		{
			get
			{
				var options = F.JsonF.CopyOptions();
				options.DictionaryKeyPolicy = null;
				options.PropertyNamingPolicy = null;
				return options;
			}
		}
	}
}
