﻿// Copyright (c) bcg|design.
// Licensed under https://mit.bcgdesign.com/2013.

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Jeebs.WordPress.TypeHandlers
{
	/// <summary>
	/// Boolean TypeHandler
	/// </summary>
	public sealed class BooleanTypeHandler : Dapper.SqlMapper.TypeHandler<bool>
	{
		/// <summary>
		/// Columns where value is 1 / 0
		/// </summary>
		private readonly List<string> oneZero = new(new[] { "comment_approved" });

		/// <summary>
		/// Columns where value is yes / no
		/// </summary>
		private readonly List<string> yesNo = new(new[] { "autoload" });

		/// <summary>
		/// Columns where value is Y / N
		/// </summary>
		private readonly List<string> yN = new(new[] { "comment_subscribe", "link_visible" });

		/// <summary>
		/// Parse the various options of boolean values in WordPress database
		/// </summary>
		/// <param name="value">Database value</param>
		/// <returns>True / False</returns>
		public override bool Parse(object value) =>
			new[] { "1", "y", "yes" }.Contains(value.ToString()?.ToLower());

		/// <summary>
		/// Set the value based on the column name
		/// </summary>
		/// <param name="parameter">IDbDataParameter object</param>
		/// <param name="value">True / False</param>
		public override void SetValue(IDbDataParameter parameter, bool value)
		{
			if (oneZero.Contains(parameter.SourceColumn))
			{
				parameter.Value = value ? "1" : "0";
			}
			else if (yesNo.Contains(parameter.SourceColumn))
			{
				parameter.Value = value ? "yes" : "no";
			}
			else if (yN.Contains(parameter.SourceColumn))
			{
				parameter.Value = value ? "Y" : "N";
			}
			else
			{
				throw new InvalidOperationException($"Don't know how to map boolean column: '{parameter.SourceColumn}'.");
			}
		}
	}
}
