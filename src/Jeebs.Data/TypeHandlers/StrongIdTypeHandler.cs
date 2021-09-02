﻿// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System.Data;

namespace Jeebs.Data.TypeHandlers
{
	/// <summary>
	/// <see cref="IStrongId"/> TypeHandler
	/// </summary>
	/// <typeparam name="T"><see cref="IStrongId"/> type</typeparam>
	public sealed class StrongIdTypeHandler<T> : Dapper.SqlMapper.TypeHandler<T>
		where T : IStrongId, new()
	{
		/// <summary>
		/// Parse value as ulong and create new <see cref="IStrongId"/>
		/// </summary>
		/// <param name="value"><see cref="IStrongId"/> Value</param>
		public override T Parse(object value) =>
			value switch
			{
				ulong id =>
					new() { Value = id },

				_ =>
					ulong.TryParse(value?.ToString(), out ulong id) switch
					{
						true =>
							new() { Value = id },

						false =>
							new()
					}
			};

		/// <summary>
		/// Set ID value
		/// </summary>
		/// <param name="parameter">IDbDataParameter</param>
		/// <param name="value"><see cref="IStrongId"/> value</param>
		public override void SetValue(IDbDataParameter parameter, T value) =>
			parameter.Value = value.Value;
	}
}
