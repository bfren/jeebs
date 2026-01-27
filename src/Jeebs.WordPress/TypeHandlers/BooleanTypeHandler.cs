// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Jeebs.WordPress.TypeHandlers;

/// <summary>
/// Boolean TypeHandler.
/// </summary>
public sealed class BooleanTypeHandler : Dapper.SqlMapper.TypeHandler<bool>
{
	private static readonly string[] YesValues = ["1", "y", "yes"];

	/// <summary>
	/// Columns where value is 1 / 0.
	/// </summary>
	private readonly List<string> oneZero = new() { { "comment_approved" } };

	/// <summary>
	/// Columns where value is yes / no.
	/// </summary>
	private readonly List<string> yesNo = new() { { "autoload" } };

	/// <summary>
	/// Columns where value is Y / N.
	/// </summary>
	private readonly List<string> yN = new() { { "comment_subscribe" }, { "link_visible" } };

	/// <summary>
	/// Parse the various options of boolean values in WordPress database.
	/// </summary>
	/// <param name="value">Database value.</param>
	public override bool Parse(object value) =>
		YesValues.Contains(value?.ToString()?.ToLower(F.DefaultCulture));

	/// <summary>
	/// Set the value based on the column name.
	/// </summary>
	/// <param name="parameter">IDbDataParameter object.</param>
	/// <param name="value">True / False.</param>
	/// <exception cref="InvalidOperationException"></exception>
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
