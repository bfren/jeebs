// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System.Reflection;

namespace Jeebs.Data.Mapping
{
	/// <summary>
	/// Holds information about a column mapped using <seealso cref="Map{TEntity}"/>
	/// </summary>
	public interface IMappedColumn : IColumn
	{
		/// <summary>
		/// Entity Property
		/// </summary>
		PropertyInfo Property { get; }
	}
}
