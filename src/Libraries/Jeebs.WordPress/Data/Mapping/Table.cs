// Jeebs Rapid Application Development
// Copyright (c) bfren.uk - licensed under https://mit.bfren.uk/2013

namespace Jeebs.WordPress.Data.Mapping
{
	/// <summary>
	/// Table
	/// </summary>
	public abstract class Table : ITable
	{
		/// <summary>
		/// Table name
		/// </summary>
		private string Name { get; }

		/// <summary>
		/// Create object
		/// </summary>
		/// <param name="name">Table name</param>
		protected Table(string name) =>
			Name = name;

		/// <summary>
		/// Table name (unescaped)
		/// </summary>
		/// <returns>Table name</returns>
		public override string ToString() =>
			Name;
	}
}
