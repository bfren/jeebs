using System;
using System.Collections.Generic;
using System.Text;
using Jeebs.Reflection;

namespace Jeebs.Data.Querying
{
	/// <inheritdoc cref="IQueryParameters"/>
	public sealed class QueryParameters : Dictionary<string, object>, IQueryParameters
	{
		/// <inheritdoc/>
		public void Add(object parameters)
		{
			if (parameters is QueryParameters keyValuePairs)
			{
				foreach (var p in keyValuePairs)
				{
					Add(p.Key, p.Value);
				}
			}
			else
			{
				foreach (var p in parameters.GetProperties())
				{
					var name = p.Name;
					var value = p.GetValue(parameters);

					Add(name, value);
				}
			}
		}

		/// <summary>
		/// Return parameters as JSON
		/// </summary>
		public override string ToString()
			=> F.JsonF.Serialise(this);
	}
}
