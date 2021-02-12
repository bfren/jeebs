﻿using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Jeebs.Data.Mapping
{
	/// <inheritdoc cref="IMappedColumn"/>
	public sealed class MappedColumn : Column, IMappedColumn
	{
		/// <inheritdoc/>
		public PropertyInfo Property { get; }

		/// <summary>
		/// Create object
		/// </summary>
		/// <param name="table">Escaped table name</param>
		/// <param name="name">Escaped Column Name</param>
		/// <param name="property">Entity property PropertyInfo</param>
		public MappedColumn(string table, string name, PropertyInfo property) : base(table, name, property.Name) =>
			Property = property;

		/// <summary>
		/// Return Escaped Column Name
		/// </summary>
		public override string ToString() =>
			Name;
	}
}
