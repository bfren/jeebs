// Jeebs Rapid Application Development
// Copyright (c) bfren.uk - licensed under https://mit.bfren.uk/2013

using System.Data;

namespace Jeebs.Data.TypeHandlers
{
	/// <summary>
	/// StrongId TypeHandler
	/// </summary>
	/// <typeparam name="T">StrongId type</typeparam>
	public sealed class StrongIdTypeHandler<T> : Dapper.SqlMapper.TypeHandler<T>
		where T : StrongId, new()
	{
		/// <summary>
		/// Parse value as ID and create new StrongId
		/// </summary>
		/// <param name="value">Id Value</param>
		public override T Parse(object value) =>
			ulong.TryParse(value?.ToString(), out ulong id) switch
			{
				true =>
					new() { Value = id },

				false =>
					new()
			};

		/// <summary>
		/// Set ID value
		/// </summary>
		/// <param name="parameter">IDbDataParameter</param>
		/// <param name="value">StrongId value</param>
		public override void SetValue(IDbDataParameter parameter, T value) =>
			parameter.Value = value.Value;
	}
}
