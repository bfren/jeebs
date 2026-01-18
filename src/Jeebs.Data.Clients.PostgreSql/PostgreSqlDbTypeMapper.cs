// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System.Collections.Generic;
using Dapper;
using Jeebs.Cryptography;
using Jeebs.Data.Clients.PostgreSql.TypeHandlers;
using Npgsql;

namespace Jeebs.Data.Clients.PostgreSql;

/// <summary>
/// PostgreSQL database type mapping
/// </summary>
public sealed class PostgreSqlDbTypeMapper : DbTypeMapper
{
	/// <summary>
	/// Initializes a new instance of the <see cref="PostgreSqlDbTypeMapper"/> class.
	/// </summary>
#pragma warning disable CS0618 // Type or member is obsolete
#pragma warning disable NPG9001 // Type is for evaluation purposes only and is subject to change or removal in future updates. Suppress this diagnostic to proceed.
	public PostgreSqlDbTypeMapper() =>
		NpgsqlConnection.GlobalTypeMapper.AddTypeInfoResolverFactory(new LegacyDateAndTimeResolverFactory());
#pragma warning restore NPG9001 // Type is for evaluation purposes only and is subject to change or removal in future updates. Suppress this diagnostic to proceed.
#pragma warning restore CS0618 // Type or member is obsolete

	/// <inheritdoc/>
	public override void AddEnumeratedListTypeHandler<T>() =>
		AddTypeHandler(new EnumeratedListJsonbTypeHandler<T>());

	/// <inheritdoc/>
	public override void AddJsonTypeHandler<T>() =>
		AddTypeHandler(new JsonbTypeHandler<T>());

	/// <inheritdoc/>
	public override void AddListTypeHandlers<T>()
	{
		AddJsonTypeHandler<T[]>();
		AddJsonTypeHandler<List<T>>();
		AddTypeHandler(new ImmutableListJsonbTypeHandler<T>());
	}

	/// <inheritdoc/>
	public override void AddLockedTypeHandlers() =>
		AddGenericTypeHandlers<Locked>(typeof(JsonbTypeHandler<>), SqlMapper.AddTypeHandler);
}
