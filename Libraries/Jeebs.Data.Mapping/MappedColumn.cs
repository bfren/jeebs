using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Jeebs.Data
{
	/// <inheritdoc cref="IMappedColumn"/>
	public sealed class MappedColumn : IMappedColumn
	{
		/// <inheritdoc/>
		public string Column { get; }

		/// <inheritdoc/>
		public PropertyInfo Property { get; }

		/// <summary>
		/// Create object
		/// </summary>
		/// <param name="column">Escaped Column Name</param>
		/// <param name="property">Entity property PropertyInfo</param>
		public MappedColumn(string column, PropertyInfo property)
		{
			Column = column;
			Property = property;
		}

		/// <summary>
		/// Return Escaped Column Name
		/// </summary>
		public override string ToString() => Column;
	}
}
