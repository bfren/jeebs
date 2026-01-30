// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System.Threading.Tasks;

namespace Jeebs.Data.FluentQuery;

public abstract partial record class FluentQuery<TEntity, TId>
{
	#region Count

	/// <inheritdoc/>
	public abstract Task<Result<long>> CountAsync();

	#endregion Count
}
