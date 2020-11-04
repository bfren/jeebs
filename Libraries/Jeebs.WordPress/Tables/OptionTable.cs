using System;
using System.Collections.Generic;
using System.Text;
using Jeebs.Data.Mapping;

namespace Jeebs.WordPress.Tables
{
	/// <summary>
	/// Option Table
	/// </summary>
	public sealed class OptionTable : Table
	{
		/// <summary>
		/// OptionId
		/// </summary>
		public string OptionId { get; } = "option_id";

		/// <summary>
		/// Key
		/// </summary>
		public string Key { get; } = "option_name";

		/// <summary>
		/// Value
		/// </summary>
		public string Value { get; } = "option_value";

		/// <summary>
		/// IsAutoloaded
		/// </summary>
		public string IsAutoloaded { get; } = "autoload";

		/// <summary>
		/// Create object
		/// </summary>
		/// <param name="prefix">Table prefix</param>
		public OptionTable(string prefix) : base($"{prefix}options") { }
	}
}
