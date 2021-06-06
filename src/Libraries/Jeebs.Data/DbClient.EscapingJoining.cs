// Jeebs Rapid Application Development
// Copyright (c) bfren.uk - licensed under https://mit.bfren.uk/2013

using System.Collections.Generic;
using Jeebs.Data.Enums;

namespace Jeebs.Data
{
	public abstract partial class DbClient : IDbClient
	{
		/// <inheritdoc/>
		public abstract string Escape(IColumn column, bool withAlias = false);

		/// <inheritdoc/>
		public abstract string EscapeWithTable(IColumn column, bool withAlias = false);

		/// <inheritdoc/>
		public abstract string Escape(ITable table);

		/// <inheritdoc/>
		public abstract string Escape(string columnOrTable);

		/// <inheritdoc/>
		public abstract string Escape(string column, string table);

		/// <inheritdoc/>
		public abstract string GetOperator(SearchOperator op);

		/// <inheritdoc/>
		public abstract string GetParamRef(string paramName);

		/// <inheritdoc/>
		public abstract string JoinList(List<string> objects, bool wrap);
	}
}
