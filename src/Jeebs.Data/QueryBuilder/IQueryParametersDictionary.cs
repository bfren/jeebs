// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System.Collections.Generic;

namespace Jeebs.Data.QueryBuilder;

/// <summary>
/// Shorthand to make working with query parameters easier.
/// </summary>
public interface IQueryParametersDictionary : IDictionary<string, object>
{
	/// <summary>
	/// Merge another <see cref="IQueryParametersDictionary"/> into this one.
	/// </summary>
	/// <param name="parameters">Parameters to merge.</param>
	bool Merge(IQueryParametersDictionary parameters);

	/// <summary>
	/// Add an anonymous object of parameters to the dictionary
	/// Properties must be simple key/value pairs
	/// </summary>
	/// <param name="parameters">Parameters to add.</param>
	bool TryAdd(object? parameters);
}
