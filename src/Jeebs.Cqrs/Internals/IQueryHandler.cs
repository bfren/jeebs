// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System.Threading.Tasks;

namespace Jeebs.Cqrs.Internals;

/// <summary>
/// Query handler interface which allows generic dispatching - see
/// <see cref="Dispatcher.DispatchAsync{TResult}(IQuery{TResult})"/>
/// </summary>
/// <typeparam name="TResult">Query result value type</typeparam>
internal interface IQueryHandler<TResult>
{
	/// <summary>
	/// Handle a query
	/// </summary>
	/// <param name="query">Query object</param>
	Task<Maybe<TResult>> HandleAsync(IQuery<TResult> query);
}
