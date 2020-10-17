using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Jeebs.Reflection;

namespace Jeebs.Data.Querying
{
	/// <inheritdoc cref="IQueryParameters"/>
	public sealed class QueryParameters : Dictionary<string, object>, IQueryParameters
	{
		/// <inheritdoc/>
		public bool TryAdd<T>(T parameters)
		{
			if (parameters is null)
			{
				return false;
			}
			else if (parameters is IQueryParameters queryParameters)
			{
				foreach (var p in queryParameters)
				{
					Add(p.Key, p.Value);
				}

				return true;
			}
			else if(getReadableProperties() is var objectProperties && objectProperties.Count() > 0)
			{
				foreach (var p in objectProperties)
				{
					var name = p.Name;
					var value = p.GetValue(parameters);
					Add(name, value);
				}

				return true;
			}

			return false;

			static IEnumerable<PropertyInfo> getReadableProperties()
			{
				return from p in typeof(T).GetProperties()
					   where p.MemberType == MemberTypes.Property
					   && p.GetMethod.IsPublic
					   select p;
			}
		}

		/// <summary>
		/// Return parameters as JSON
		/// </summary>
		public override string ToString()
			=> F.JsonF.Serialise(this);
	}
}
