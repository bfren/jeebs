// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

namespace Jeebs.WordPress.Data.Mapping
{
	/// <inheritdoc cref="IColumn"/>
	public partial class Column : IColumn
	{
		/// <inheritdoc/>
		public string Table { get; }

		/// <inheritdoc/>
		public string Name { get; }

		/// <inheritdoc/>
		public string Alias { get; }

		/// <summary>
		/// Create object
		/// </summary>
		/// <param name="table">Table Name</param>
		/// <param name="name">Column Name</param>
		/// <param name="alias">Column Alias</param>
		public Column(string table, string name, string alias) =>
			(Table, Name, Alias) = (table, name, alias);
	}
}
