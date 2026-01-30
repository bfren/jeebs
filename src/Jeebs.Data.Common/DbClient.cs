// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System.Data.Common;

namespace Jeebs.Data.Common;

/// <inheritdoc cref="IDbClient"/>
public abstract partial class DbClient : IDbClient
{
	/// <summary>
	/// IEntityMapper.
	/// </summary>
	public IEntityMapper Entities { get; private init; }

	/// <summary>
	/// IDbTypeMapper.
	/// </summary>
	public IDbTypeMapper Types { get; private init; }

	/// <summary>
	/// Create using default instances.
	/// </summary>
	protected DbClient() : this(EntityMapper.Instance, DbTypeMapper.Instance) { }

	/// <summary>
	/// Inject dependencies.
	/// </summary>
	/// <param name="types"></param>
	protected DbClient(IDbTypeMapper types) : this(EntityMapper.Instance, types) { }

	/// <summary>
	/// Inject dependencies.
	/// </summary>
	/// <param name="entities"></param>
	/// <param name="types"></param>
	protected DbClient(IEntityMapper entities, IDbTypeMapper types) =>
		(Entities, Types) = (entities, types);

	/// <inheritdoc/>
	public abstract DbConnection GetConnection(string connectionString);
}
