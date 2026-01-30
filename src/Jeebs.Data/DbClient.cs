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
	public IEntityMapper Entities { get; private init; }

	/// <summary>
	/// Create using default instances.
	/// </summary>
	protected DbClient() : this(EntityMapper.Instance) { }

	/// <summary>
	/// Create using default instances.
	/// </summary>
	protected DbClient(IEntityMapper entities) =>
		Entities = entities;
}
