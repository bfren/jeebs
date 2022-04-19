// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Dapper;
using Jeebs.Cryptography;
using Jeebs.Data.Clients.PostgreSql.TypeHandlers;

namespace Jeebs.Data.Clients.PostgreSql;

/// <summary>
/// <see cref="DbTypeMap"/> extension methods
/// </summary>
public static class DbTypeMapExtensions
{
	/// <summary>
	/// Persist an <see cref="Collections.EnumeratedList{T}"/> to the database by encoding it as JSON
	/// </summary>
	/// <typeparam name="T">Type to handle</typeparam>
	/// <param name="this"></param>
	public static void AddEnumeratedListJsonbTypeHandler<T>(this DbTypeMap @this)
		where T : Enumerated =>
		@this.AddTypeHandler(new EnumeratedListJsonbTypeHandler<T>());

	/// <summary>
	/// Persist an <see cref="Collections.ImmutableList{T}"/> to the database by encoding it as JSON
	/// </summary>
	/// <typeparam name="T"></typeparam>
	/// <param name="this"></param>
	public static void AddImmutableListJsonTypeHandler<T>(this DbTypeMap @this) =>
		@this.AddTypeHandler(new ImmutableListJsonbTypeHandler<T>());

	/// <summary>
	/// Persist a type to the database by encoding it as JSON
	/// </summary>
	/// <typeparam name="T">Type to handle</typeparam>
	/// <param name="this"></param>
	public static void AddJsonbTypeHandler<T>(this DbTypeMap @this) =>
		@this.AddTypeHandler(new JsonbTypeHandler<T>());

	/// <summary>
	/// Persist <see cref="Locked{T}"/> properties to the database
	/// </summary>
	/// <param name="this"></param>
	public static void AddLockedJsonbTypeHandlers(this DbTypeMap @this) =>
		@this.AddGenericTypeHandlers<Locked>(typeof(JsonbTypeHandler<>), SqlMapper.AddTypeHandler);
}
