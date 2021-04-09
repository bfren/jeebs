// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System;
using Jeebs.Data.Mapping;

namespace Jeebs.Data.Querying.Exceptions
{
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
}
