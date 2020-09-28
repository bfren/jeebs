using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Jeebs.Data.Mapping
{
	/// <summary>
	/// Holds information about a column mapped using <seealso cref="Map{TEntity}"/>
	/// </summary>
	public interface IMappedColumn
	{
		/// <summary>
		/// Escaped Column Name
		/// </summary>
		string Column { get; }

		/// <summary>
		/// Entity Property
		/// </summary>
		PropertyInfo Property { get; }
	}
}
