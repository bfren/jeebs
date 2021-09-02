// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Data.Mapping;

namespace Jeebs.Data.Querying.Exceptions;

/// <inheritdoc cref="QueryBuilderException{TTable}"/>
public class UnableToGetColumnFromExpressionException<TTable> : QueryBuilderException<TTable>
	where TTable : ITable
{
	/// <inheritdoc/>
	public UnableToGetColumnFromExpressionException() { }

	/// <inheritdoc/>
	public UnableToGetColumnFromExpressionException(string message) : base(message) { }

	/// <inheritdoc/>
	public UnableToGetColumnFromExpressionException(string message, Exception inner) : base(message, inner) { }
}
