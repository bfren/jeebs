// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

namespace Jeebs.Data.FluentQuery;

public abstract partial record class FluentQuery<TFluentQuery, TEntity, TId>
{
	/// <inheritdoc/>
	public TFluentQuery Maximum(ulong number) =>
		number switch
		{
			> 0 =>
				Update(parts => parts with { Maximum = number }),

			_ =>
				Unchanged()
		};

	/// <inheritdoc/>
	public TFluentQuery Skip(ulong number) =>
		number switch
		{
			> 0 =>
				Update(parts => parts with { Skip = number }),

			_ =>
				Unchanged()
		};
}
