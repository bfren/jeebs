// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Jeebs.Data.Common;

namespace Jeebs.Data;

public interface IAdapter
{
	ITypeMapper Mapper { get; }

	Task<Result<int>> ExecuteAsync(IDbTransaction transaction, string query, object? param, CommandType type);

	Task<Result<T>> ExecuteAsync<T>(IDbTransaction transaction, string query, object? param, CommandType type);

	Task<Result<IEnumerable<T>>> QueryAsync<T>(IDbTransaction transaction, string query, object? param, CommandType type);

	Task<Result<T>> QuerySingleAsync<T>(IDbTransaction transaction, string query, object? param, CommandType type);
}
