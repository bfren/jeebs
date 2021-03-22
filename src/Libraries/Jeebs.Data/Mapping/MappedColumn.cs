// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System.Reflection;

namespace Jeebs.Data
{
	/// <inheritdoc cref="IMappedColumn"/>
	/// <param name="table">Escaped table name</param>
	/// <param name="name">Escaped Column Name</param>
	/// <param name="property">Entity property PropertyInfo</param>
	public sealed record MappedColumn(string Table, string Name, PropertyInfo Property) :
		Column(Table, Name, Property.Name), IMappedColumn
	{
		/// <summary>
		/// Return table name
		/// </summary>
		public override string ToString() =>
			Name;
	}
}
