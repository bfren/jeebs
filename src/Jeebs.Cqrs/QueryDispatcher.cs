// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;
using System.Threading;
using System.Threading.Tasks;
using Jeebs.Cqrs.Internals;
using Jeebs.Cqrs.Messages;
using Jeebs.Logging;

namespace Jeebs.Cqrs;

/// <inheritdoc cref="IQueryDispatcher"/>
public sealed class QueryDispatcher : IQueryDispatcher
{
	private ILog<QueryDispatcher> Log { get; init; }

	private IServiceProvider Provider { get; init; }

	/// <summary>
	/// Create object
	/// </summary>
	/// <param name="provider"></param>
	/// <param name="log"></param>
	public QueryDispatcher(IServiceProvider provider, ILog<QueryDispatcher> log) =>
		(Provider, Log) = (provider, log);

	/// <inheritdoc/>
	public Task<Maybe<TResult>> DispatchAsync<TResult>(IQuery<TResult> query) =>
		DispatchAsync(query, CancellationToken.None);

	/// <inheritdoc/>
	public Task<Maybe<TResult>> DispatchAsync<TResult>(IQuery<TResult> query, CancellationToken cancellationToken)
	{
		// Make generic handler type
		var handlerType = typeof(QueryHandler<,>).MakeGenericType(query.GetType(), typeof(TResult));
		Log.Vrb("Query handler type: {Type}", handlerType);

		// Get service and handle query
		var service = Provider.GetService(handlerType);
		return service switch
		{
			IQueryHandler<TResult> handler =>
				handler.HandleAsync(query, cancellationToken),

			_ =>
				F.None<TResult>(new UnableToGetQueryHandlerMsg(query.GetType())).AsTask
		};
	}
}
