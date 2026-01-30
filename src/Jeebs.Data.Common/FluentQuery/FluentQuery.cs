// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Data.Map;
using Jeebs.Logging;

namespace Jeebs.Data.Common.FluentQuery;

/// <inheritdoc cref="IFluentQuery{TEntity, TId}"/>
public sealed partial record class FluentQuery<TEntity, TId> : Data.FluentQuery.FluentQuery<FluentQuery<TEntity, TId>, TEntity, TId>, IFluentQuery<FluentQuery<TEntity, TId>, TEntity, TId>
	where TEntity : IWithId
	where TId : class, IUnion, new()
{
	/// <summary>
	/// Database object.
	/// </summary>
	internal new IDb Db { get; private init; }

	/// <summary>
	/// Create object.
	/// </summary>
	/// <param name="db">Database instance.</param>
	/// <param name="mapper">IMapper.</param>
	/// <param name="log">ILog (should come with the context of the calling class).</param>
	internal FluentQuery(IDb db, IEntityMapper mapper, ILog log) : base(db, mapper, log) =>
		Db = db;
}
