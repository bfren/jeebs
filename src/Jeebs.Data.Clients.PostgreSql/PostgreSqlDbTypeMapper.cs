// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Dapper;
using Jeebs.Cryptography;
using Jeebs.Data.Clients.PostgreSql.TypeHandlers;

namespace Jeebs.Data.Clients.PostgreSql;

/// <summary>
/// PostgreSQL database type mapping
/// </summary>
public sealed class PostgreSqlDbTypeMapper : DbTypeMapper
{
	/// <inheritdoc/>
	public override void AddEnumeratedListTypeHandler<T>() =>
		AddTypeHandler(new EnumeratedListJsonbTypeHandler<T>());

	/// <inheritdoc/>
	public override void AddJsonTypeHandler<T>() =>
		AddTypeHandler(new JsonbTypeHandler<T>());

	/// <inheritdoc/>
	public override void AddImmutableListTypeHandler<T>() =>
		AddTypeHandler(new ImmutableListJsonbTypeHandler<T>());

	/// <inheritdoc/>
	public override void AddLockedTypeHandlers() =>
		AddGenericTypeHandlers<Locked>(typeof(JsonbTypeHandler<>), SqlMapper.AddTypeHandler);
}
