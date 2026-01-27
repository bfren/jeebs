// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;
using System.Data;
using System.Text.Json;
using Jeebs.Functions;
using Npgsql;
using NpgsqlTypes;

namespace Jeebs.Data.Clients.PostgreSql.TypeHandlers;

/// <summary>
/// Jsonb Type Handler to map JSON as jsonb so it is queryable.
/// </summary>
/// <typeparam name="T">Value type.</typeparam>
public class JsonbTypeHandler<T> : Dapper.SqlMapper.TypeHandler<T>
{
	/// <summary>
	/// Parse value as JSON into object of type <typeparamref name="T"/>.
	/// </summary>
	/// <param name="value">Database value.</param>
	/// <exception cref="JsonException">When deserialisation fails</exception>
	public override T Parse(object value) =>
		value switch
		{
			string json when !string.IsNullOrWhiteSpace(json) =>
				JsonF.Deserialise<T>(json).Unwrap(),

			_ =>
				throw new JsonException($"Invalid JSON: {value}.")
		};

	/// <summary>
	/// Serialise value and set column type as <see cref="NpgsqlDbType.Jsonb"/>.
	/// </summary>
	/// <param name="parameter">IDbDataParameter.</param>
	/// <param name="value">Value object.</param>
	public override void SetValue(IDbDataParameter parameter, T? value)
	{
		if (parameter is NpgsqlParameter npgsqlParameter)
		{
			npgsqlParameter.NpgsqlDbType = NpgsqlDbType.Jsonb;
		}

		parameter.Value = value switch
		{
			T x =>
				JsonF.Serialise(x).Unwrap(_ => JsonF.Empty),

			_ =>
				DBNull.Value
		};
	}
}
