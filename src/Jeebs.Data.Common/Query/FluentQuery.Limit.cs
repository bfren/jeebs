// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

namespace Jeebs.Data.Query;

public sealed partial record class FluentQuery<TEntity, TId>
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
