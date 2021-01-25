using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Jeebs.Data.Mapping
{
	/// <summary>
	/// Table
	/// </summary>
	public abstract class Table
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
