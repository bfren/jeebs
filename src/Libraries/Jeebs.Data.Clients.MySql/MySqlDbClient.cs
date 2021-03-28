// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System.Collections.Generic;
using System.Data;
using Jeebs.Data.Enums;
using MySql.Data.MySqlClient;

namespace Jeebs.Data.Clients.MySql
{
	/// <inheritdoc cref="IDbClient"/>
	public partial class MySqlDbClient : DbClient
	{
		/// <inheritdoc/>
		public override IDbConnection Connect(string connectionString) =>
			new MySqlConnection(connectionString);

		/// <inheritdoc/>
		protected override string Escape(IColumn column) =>
			Escape(column.Name);

		/// <inheritdoc/>
		protected override string Escape(ITable table) =>
			Escape(table.GetName());

		/// <inheritdoc/>
		protected override string Escape(string columnOrTable) =>
			$"`{columnOrTable}`";

		/// <inheritdoc/>
		protected override string Escape(string column, string table) =>
			Escape(table) + "." + Escape(column);

		/// <inheritdoc/>
		protected override string GetOperator(SearchOperator op) =>
			op.ToOperator();

		/// <inheritdoc/>
		protected override string GetParamRef(string paramName) =>
			$"@{paramName}";

		/// <inheritdoc/>
		protected override string JoinList(List<string> objects, bool wrap)
		{
			var list = string.Join(", ", objects);
			return wrap switch
			{
				true =>
					$"({list})",

				false =>
					list
			};
		}
	}
}
