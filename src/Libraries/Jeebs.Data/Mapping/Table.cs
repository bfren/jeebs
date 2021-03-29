// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

namespace Jeebs.Data
{
	/// <inheritdoc cref="ITable"/>
	public abstract record Table : ITable
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
