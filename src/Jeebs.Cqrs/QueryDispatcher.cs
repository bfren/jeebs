// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;
using System.Threading;
using System.Threading.Tasks;
using MaybeF;
using Microsoft.Extensions.DependencyInjection;

namespace Jeebs.Cqrs;

/// <inheritdoc cref="IQueryDispatcher"/>
public sealed class QueryDispatcher : IQueryDispatcher
{
	private readonly IServiceProvider provider;

	/// <summary>
	/// Create object
	/// </summary>
	/// <param name="provider">IServiceProvider</param>
	public QueryDispatcher(IServiceProvider provider) =>
		this.provider = provider;

	/// <inheritdoc/>
	public Task<Maybe<TResult>> DispatchAsync<TQuery, TResult>(TQuery query, CancellationToken cancellationToken)
	{
		var handler = provider.GetRequiredService<IQueryHandler<TQuery, TResult>>();
		return handler.HandleAsync(query, cancellationToken);
	}
}
