// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Data.Query;
using StrongId;

namespace Jeebs.WordPress.Query;

/// <summary>
/// WordPress Query Options
/// </summary>
/// <typeparam name="TId">Entity ID type</typeparam>
public abstract record class Options<TId> : QueryOptions<TId>
	where TId : ULongId, new()
{
	/// <summary>
	/// IWpDbSchema shorthand
	/// </summary>
	protected IWpDbSchema T { get; private init; }

	internal IWpDbSchema TTest =>
		T;

	/// <summary>
	/// Inject dependencies
	/// </summary>
	/// <param name="schema">IWpDbSchema</param>
	/// <param name="builder">IQueryPartsBuilder</param>
	protected Options(IWpDbSchema schema, IQueryPartsBuilder<TId> builder) : base(builder) =>
		T = schema;
}
