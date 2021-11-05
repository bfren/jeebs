﻿// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Dapper;
using Jeebs.Data.TypeHandlers;

namespace Jeebs.Data;

/// <summary>
/// DbMapper extension methods
/// </summary>
public static class DbMapperExtensions
{
	/// <summary>
	/// Persist an EnumList to the database by encoding it as JSON
	/// </summary>
	/// <typeparam name="T">Type to handle</typeparam>
	public static void AddEnumeratedListTypeHandler<T>()
		where T : Enumerated =>
		SqlMapper.AddTypeHandler(new JsonbEnumeratedListTypeHandler<T>());

	/// <summary>
	/// Persist a type to the database by encoding it as JSON
	/// </summary>
	/// <typeparam name="T">Type to handle</typeparam>
	public static void AddJsonbTypeHandler<T>() =>
		SqlMapper.AddTypeHandler(new JsonbTypeHandler<T>());
}
