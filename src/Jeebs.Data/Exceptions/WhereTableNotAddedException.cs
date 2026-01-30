// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;
using Jeebs.Data.Map;

namespace Jeebs.Data.QueryBuilder.Exceptions;

/// <inheritdoc cref="QueryBuilderException{TTable}"/>
public class WhereTableNotAddedException<TTable> : QueryBuilderException<TTable>
	where TTable : ITable
{
	/// <inheritdoc/>
	public WhereTableNotAddedException() { }

	/// <inheritdoc/>
	public WhereTableNotAddedException(string message) : base(message) { }

	/// <inheritdoc/>
	public WhereTableNotAddedException(string message, Exception inner) : base(message, inner) { }
}
