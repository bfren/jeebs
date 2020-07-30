using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Jeebs.Data
{
	/// <summary>
	/// Table
	/// </summary>
	public abstract class Table
	{
		/// <summary>
		/// Table name
		/// </summary>
		private readonly string name;

		/// <summary>
		/// Create object
		/// </summary>
		/// <param name="name">Table name</param>
		protected Table(string name)
			=> this.name = name;

		/// <summary>
		/// Table name (unescaped)
		/// </summary>
		/// <returns>Table name</returns>
		public override string ToString()
			=> name;
	}
}
