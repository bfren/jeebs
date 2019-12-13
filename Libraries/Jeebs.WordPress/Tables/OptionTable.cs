﻿using System;
using System.Collections.Generic;
using System.Text;
using Jeebs.Data;
using Jeebs.Data.Clients.MySql;
using Jeebs.WordPress.Entities;

namespace Jeebs.WordPress.Tables
{
	/// <summary>
	/// Option Table
	/// </summary>
	public sealed class OptionTable<T> : Table<T>
		where T : WpOptionEntity
	{
		/// <summary>
		/// OptionId
		/// </summary>
		public readonly string OptionId = "option_id";

		/// <summary>
		/// Key
		/// </summary>
		public readonly string Key = "option_name";

		/// <summary>
		/// Value
		/// </summary>
		public readonly string Value = "option_value";

		/// <summary>
		/// IsAutoloaded
		/// </summary>
		public readonly string IsAutoloaded = "autoload";

		/// <summary>
		/// Create object
		/// </summary>
		/// <param name="adapter">IAdapter</param>
		/// <param name="prefix">Table prefix</param>
		public OptionTable(in IAdapter adapter, in string prefix) : base(adapter, $"{prefix}options") { }
	}
}
