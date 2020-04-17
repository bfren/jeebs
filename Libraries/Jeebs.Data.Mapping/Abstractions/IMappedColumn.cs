using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Jeebs.Data
{
	/// <summary>
	/// Mapped Column interface
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
