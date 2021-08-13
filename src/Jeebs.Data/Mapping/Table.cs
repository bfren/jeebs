// Jeebs Rapid Application Development
// Copyright (c) bfren.uk - licensed under https://mit.bfren.uk/2013

namespace Jeebs.Data.Mapping
{
	/// <inheritdoc cref="ITable"/>
	public abstract record class Table : ITable
	{
		private readonly string name;

		/// <summary>
		/// Create with table name
		/// </summary>
		/// <param name="name">Table Name</param>
		public Table(string name) =>
			this.name = name;

		/// <inheritdoc/>
		public virtual string GetName() =>
			name;

		/// <inheritdoc cref="GetName"/>
		public override string ToString() =>
			GetName();
	}
}
