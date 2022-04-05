// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using StrongId;

namespace Jeebs.Data.Query;

public sealed partial record class FluentQuery<TEntity, TId> : FluentQuery, IFluentQuery<TEntity, TId>
	where TEntity : IWithId<TId>
	where TId : class, IStrongId, new()
{
	/// <inheritdoc/>
	public IFluentQuery<TEntity, TId> Maximum(ulong number) =>
		number switch
		{
			> 0 =>
				Update(parts => parts with { Maximum = number }),

			_ =>
				this
		};

	/// <inheritdoc/>
	public IFluentQuery<TEntity, TId> Skip(ulong number) =>
		number switch
		{
			> 0 =>
				Update(parts => parts with { Skip = number }),

			_ =>
				this
		};
}
