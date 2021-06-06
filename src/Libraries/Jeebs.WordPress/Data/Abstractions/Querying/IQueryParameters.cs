// Jeebs Rapid Application Development
// Copyright (c) bfren.uk - licensed under https://mit.bfren.uk/2013

using System.Collections.Generic;

namespace Jeebs.WordPress.Data
{
	/// <summary>
	/// Shorthand to make working with query parameters easier
	/// </summary>
	public interface IQueryParameters : IDictionary<string, object>
	{
		/// <summary>
		/// Add an anonymous object of parameters to the dictionary
		/// Properties must be simple key/value pairs
		/// </summary>
		/// <param name="parameters">Parameters to add</param>
		bool TryAdd(object? parameters);
	}
}
