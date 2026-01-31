// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Dapper;
using Jeebs.Data.Common;

namespace Jeebs.Data.Adapters.Dapper;

public static class TypeMapperExtensions
{
	/// <summary>
	/// Register a type handler with Dapper.
	/// </summary>
	/// <typeparam name="T">CLR type.</typeparam>
	/// <param name="this">ITypeMapper.</param>
	/// <param name="handler">Type handler to map <typeparamref name="T"/> to / from the database.</param>
	public static void AddTypeHandler<T>(this ITypeMapper _, SqlMapper.TypeHandler<T> handler) =>
		SqlMapper.AddTypeHandler(handler);
}
