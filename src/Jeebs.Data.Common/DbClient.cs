// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;
using System.Data.Common;
using System.Threading;
using Jeebs.Data.Map;

namespace Jeebs.Data.Common;

/// <inheritdoc cref="IDbClient"/>
public abstract partial class DbClient : Data.DbClient, IDbClient
{
	protected static readonly Lock X = new();

	/// <summary>
	/// Executes queries and maps to result types.
	/// </summary>
	public IAdapter Adapter { get; init; }

	/// <summary>
	/// Inject dependencies.
	/// </summary>
	/// <param name="adapter">IAdapter.</param>
	protected DbClient(IAdapter adapter) : this(adapter, Map.EntityMapper.Instance) { }

	/// <summary>
	/// Inject dependencies.
	/// </summary>
	/// <param name="adapter">IAdapter.</param>
	/// <param name="entityMapper">IEntityMapper.</param>
	protected DbClient(IAdapter adapter, IEntityMapper entityMapper) : base(entityMapper) =>
		Adapter = adapter;

	/// <inheritdoc/>
	public abstract DbConnection GetConnection(string connectionString);

	/// <inheritdoc/>
	public virtual void TypeMap(Action<ITypeMapper> mapper)
	{
		lock (X)
		{
			mapper(Adapter.Mapper);
		}
	}
}
