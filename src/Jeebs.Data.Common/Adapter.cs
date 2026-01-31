// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace Jeebs.Data.Common;

/// <inheritdoc/>
public abstract class Adapter : IAdapter
{
	/// <inheritdoc/>
	public abstract ITypeMapper Mapper { get; }

	/// <inheritdoc/>
	public abstract Task<Result<int>> ExecuteAsync(IDbTransaction transaction, string query, object? param, CommandType type);

	/// <inheritdoc/>
	public abstract Task<Result<T>> ExecuteAsync<T>(IDbTransaction transaction, string query, object? param, CommandType type);

	/// <inheritdoc/>
	public abstract Task<Result<IEnumerable<T>>> QueryAsync<T>(IDbTransaction transaction, string query, object? param, CommandType type);

	/// <inheritdoc/>
	public abstract Task<Result<T>> QuerySingleAsync<T>(IDbTransaction transaction, string query, object? param, CommandType type);
}
