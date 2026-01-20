// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Data.Map;

namespace Jeebs.Data;

/// <summary>
/// Interface wrapper for column extraction function.
/// </summary>
public interface IExtract
{
	/// <summary>
	/// Extract columns from the list of <paramref name="tables"/> that match properties on <typeparamref name="TModel"/>.
	/// </summary>
	/// <typeparam name="TModel">Model type</typeparam>
	/// <param name="tables">Tables.</param>
	Result<IColumnList> From<TModel>(params ITable[] tables);
}
