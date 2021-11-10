﻿// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

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
	/// <param name="this">DbMapper</param>
	public static void AddJsonbEnumeratedListTypeHandler<T>(this DbMapper @this)
		where T : Enumerated =>
		@this.AddTypeHandler(new JsonbEnumeratedListTypeHandler<T>());

	/// <summary>
	/// Persist a type to the database by encoding it as JSON
	/// </summary>
	/// <typeparam name="T">Type to handle</typeparam>
	/// <param name="this">DbMapper</param>
	public static void AddJsonbTypeHandler<T>(this DbMapper @this) =>
		@this.AddTypeHandler(new JsonbTypeHandler<T>());
}
