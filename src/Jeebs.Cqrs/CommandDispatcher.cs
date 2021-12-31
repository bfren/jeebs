// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace Jeebs.Cqrs;

/// <inheritdoc cref="ICommandDispatcher"/>
public sealed class CommandDispatcher : ICommandDispatcher
{
	private readonly IServiceProvider provider;

	/// <summary>
	/// Create object
	/// </summary>
	/// <param name="provider">IServiceProvider</param>
	public CommandDispatcher(IServiceProvider provider) =>
		this.provider = provider;

	/// <inheritdoc/>
	public ValueTask<Option<TResult>> DispatchAsync<TCommand, TResult>(TCommand query, CancellationToken cancellationToken)
	{
		var handler = provider.GetRequiredService<ICommandHandler<TCommand, TResult>>();
		return handler.HandleAsync(query, cancellationToken);
	}
}

