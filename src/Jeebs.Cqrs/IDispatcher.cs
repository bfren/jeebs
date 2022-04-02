// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System.Threading.Tasks;

namespace Jeebs.Cqrs;

/// <summary>
/// CQRS dispatcher
/// </summary>
public interface IDispatcher
{
	/// <summary>
	/// Call <see cref="CommandHandler{TCommand}.HandleAsync(TCommand)"/> for <paramref name="command"/>
	/// </summary>
	/// <param name="command">Command object</param>
	Task<Maybe<bool>> DispatchAsync(ICommand command);

	/// <summary>
	/// Call <see cref="QueryHandler{TQuery, TResult}.HandleAsync(TQuery)"/> for <paramref name="query"/>
	/// </summary>
	/// <typeparam name="TResult">Query result value type</typeparam>
	/// <param name="query">Query object</param>
	Task<Maybe<TResult>> DispatchAsync<TResult>(IQuery<TResult> query);
}
