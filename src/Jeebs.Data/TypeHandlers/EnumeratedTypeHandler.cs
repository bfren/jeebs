// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;
using System.Data;

namespace Jeebs.Data.TypeHandlers;

/// <summary>
/// Enumerated TypeHandler.
/// </summary>
/// <typeparam name="T">Enumerated type</typeparam>
public abstract class EnumeratedTypeHandler<T> : Dapper.SqlMapper.TypeHandler<T>
	where T : Enumerated
{
	/// <summary>
	/// Parse the Enumerated value.
	/// </summary>
	/// <param name="value">Database table value.</param>
	/// <param name="parseValue">Function to parse <paramref name="value"/> to Enumerated value.</param>
	/// <param name="ifNullValue">Enumerated value to return if <paramref name="value"/> is null.</param>
	protected T Parse(object value, Func<string, T> parseValue, T ifNullValue) =>
		value?.ToString() switch
		{
			string valueString =>
				parseValue(valueString),

			_ =>
				ifNullValue
		};

	/// <summary>
	/// Set the Enumerated table value.
	/// </summary>
	/// <param name="parameter">IDbDataParameter object.</param>
	/// <param name="value">Enumerated value.</param>
	public override void SetValue(IDbDataParameter parameter, T? value) =>
		parameter.Value = value?.ToString();

	#region Testing

	internal T ParseTest(object value, Func<string, T> parse, T ifNull) =>
		Parse(value, parse, ifNull);

	#endregion Testing
}
