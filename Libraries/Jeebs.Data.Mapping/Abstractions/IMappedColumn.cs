using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

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
