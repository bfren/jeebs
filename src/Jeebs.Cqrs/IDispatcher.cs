// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System.Threading.Tasks;

namespace Jeebs.Cqrs;

/// <summary>
/// CQRS dispatcher.
/// </summary>
public interface IDispatcher
{
	/// <summary>
	/// Call <see cref="CommandHandler{T}.HandleAsync(T)"/> for <paramref name="command"/>.
	/// </summary>
	/// <param name="command">Command object.</param>
	Task<Result<bool>> SendAsync(Command command);

	/// <summary>
	/// Call <see cref="CommandHandler{T}.HandleAsync(T)"/> for <typeparamref name="T"/>.
	/// </summary>
	/// <typeparam name="T">Command type.</typeparam>
	Task<Result<bool>> SendAsync<T>()
		where T : Command, new();

	/// <summary>
	/// Call <see cref="QueryHandler{TQuery, TResult}.HandleAsync(TQuery)"/> for <paramref name="query"/>.
	/// </summary>
	/// <typeparam name="T">Query result value type.</typeparam>
	/// <param name="query">Query object.</param>
	Task<Result<T>> SendAsync<T>(Query<T> query);
}
