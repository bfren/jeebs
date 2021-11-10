// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System.Data;
using Npgsql;
using NpgsqlTypes;
using static F.JsonF;

namespace Jeebs.Data.Clients.PostgreSql.Parameters;

/// <summary>
/// Jsonb Parameter to allow inserting objects into a database as Jsonb (and therefore to be queryable)
/// </summary>
public sealed class Jsonb : Dapper.SqlMapper.ICustomQueryParameter
{
	/// <summary>
	/// Create a new Jsonb parameter
	/// </summary>
	/// <typeparam name="T">Object type</typeparam>
	/// <param name="obj">Object value</param>
	public static Jsonb Create<T>(T obj) =>
		obj switch
		{
			{ } =>
				new(Serialise(obj).Unwrap(Empty)),

			_ =>
				new(Empty)
		};

	/// <summary>
	/// JSON-encoded value
	/// </summary>
	private readonly string value;

	/// <summary>
	/// Create object
	/// </summary>
	/// <param name="value">JSON-encoded value</param>
	internal Jsonb(string value) =>
		this.value = value;

	/// <summary>
	/// Add this parameter to <paramref name="command"/>
	/// </summary>
	/// <param name="command">IDbCommand</param>
	/// <param name="name">Parameter name</param>
	public void AddParameter(IDbCommand command, string name) =>
		command.Parameters.Add(new NpgsqlParameter(name, NpgsqlDbType.Jsonb)
		{
			Value = value
		});

	/// <summary>
	/// Return JSON-encoded value
	/// </summary>
	public override string ToString() =>
		value;
}
