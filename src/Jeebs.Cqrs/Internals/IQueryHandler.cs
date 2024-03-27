// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System.Threading.Tasks;

namespace Jeebs.Cqrs.Internals;

/// <summary>
/// Query handler interface which allows generic dispatching - see
/// <see cref="Dispatcher.SendAsync{TResult}(Query{TResult})"/>.
/// </summary>
/// <typeparam name="T">Query result value type.</typeparam>
internal interface IQueryHandler<T>
{
	/// <summary>
	/// Handle a query.
	/// </summary>
	/// <param name="query">Query object.</param>
	/// <returns>Query result.</returns>
	Task<Result<T>> HandleAsync(Query<T> query);
}
