// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;
using Jeebs.Data.Map;

namespace Jeebs.Data.Query.Exceptions;

/// <inheritdoc cref="QueryBuilderException{TTable}"/>
public class SortByTableNotAddedException<TTable> : QueryBuilderException<TTable>
	where TTable : ITable
{
	/// <inheritdoc/>
	public SortByTableNotAddedException() { }

	/// <inheritdoc/>
	public SortByTableNotAddedException(string message) : base(message) { }

	/// <inheritdoc/>
	public SortByTableNotAddedException(string message, Exception inner) : base(message, inner) { }
}
