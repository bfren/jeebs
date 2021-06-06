// Jeebs Rapid Application Development
// Copyright (c) bfren.uk - licensed under https://mit.bfren.uk/2013

using System;
using System.Data;

namespace Jeebs.Data.TypeHandlers
{
	/// <summary>
	/// Enumerated TypeHandler
	/// </summary>
	public abstract class EnumeratedTypeHandler<T> : Dapper.SqlMapper.TypeHandler<T>
		where T : Enumerated
	{
		/// <summary>
		/// Parse the Enumerated value
		/// </summary>
		/// <param name="value">Database table value</param>
		/// <param name="parse">Function to parse <paramref name="value"/> to Enumerated value</param>
		/// <param name="ifNull">Enumerated value to return if <paramref name="value"/> is null</param>
		protected T Parse(object value, Func<string, T> parse, T ifNull) =>
			value?.ToString() switch
			{
				string valueString =>
					parse(valueString),

				_ =>
					ifNull
			};

		/// <summary>
		/// Set the Enumerated table value
		/// </summary>
		/// <param name="parameter">IDbDataParameter object</param>
		/// <param name="value">Enumerated value</param>
		public override void SetValue(IDbDataParameter parameter, T value) =>
			parameter.Value = value?.ToString();

		#region Testing

		internal T ParseTest(object value, Func<string, T> parse, T ifNull) =>
			Parse(value, parse, ifNull);

		#endregion
	}
}
