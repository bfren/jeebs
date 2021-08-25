// Jeebs Rapid Application Development
// Copyright (c) bfren.uk - licensed under https://mit.bfren.uk/2013

using Jeebs.Data.Mapping;

namespace Jeebs.Data.Querying.Exceptions
{
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
}
