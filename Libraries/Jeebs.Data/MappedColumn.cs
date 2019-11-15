using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Jeebs.Data
{
	/// <summary>
	/// Mapped Column
	/// </summary>
	public class MappedColumn
	{
		/// <summary>
		/// Escaped Column Name
		/// </summary>
		public string Column { get; }

		/// <summary>
		/// Entity Property
		/// </summary>
		public PropertyInfo Property { get; }

		/// <summary>
		/// Create object
		/// </summary>
		/// <param name="column">Escaped Column Name</param>
		/// <param name="property">Entity Property</param>
		public MappedColumn(string column, PropertyInfo property)
		{
			Column = column;
			Property = property;
		}
	}
}
