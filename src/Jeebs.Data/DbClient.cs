// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Data.Map;

namespace Jeebs.Data;

/// <inheritdoc cref="IDbClient"/>
public abstract partial class DbClient : IDbClient
{
	/// <summary>
	/// IEntityMapper.
	/// </summary>
	public IEntityMapper EntityMapper { get; private init; }

	/// <summary>
	/// Create using default instances.
	/// </summary>
	protected DbClient() : this(Map.EntityMapper.Instance) { }

	/// <summary>
	/// Create using default instances.
	/// </summary>
	protected DbClient(IEntityMapper entities) =>
		EntityMapper = entities;
}
