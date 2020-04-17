using System;
using System.Collections.Generic;
using System.Text;
using Jeebs.Reflection;

namespace Jeebs.Data
{
	/// <summary>
	/// Shorthand to make working with query parameters easier
	/// </summary>
	[Serializable]
	public sealed class QueryParameters : Dictionary<string, object>, IQueryParameters
	{
		/// <summary>
		/// Add an anonymous object of parameters to the dictionary
		/// Properties must be simple key/value pairs
		/// </summary>
		/// <param name="parameters">Parameters to add</param>
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
	}
}
