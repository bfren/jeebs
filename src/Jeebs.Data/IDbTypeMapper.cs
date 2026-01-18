// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;
using Dapper;

namespace Jeebs.Data;

/// <summary>
/// Add custom type maps.
/// </summary>
public interface IDbTypeMapper
{
	/// <summary>
	/// Reset type handlers.
	/// </summary>
	void ResetTypeHandlers();

	/// <summary>
	/// Add a type handler.
	/// </summary>
	/// <typeparam name="T">Type to handle</typeparam>
	/// <param name="typeHandler">Type handler.</param>
	void AddTypeHandler<T>(SqlMapper.TypeHandler<T> typeHandler);

	/// <summary>
	/// Persist an <see cref="Collections.EnumeratedList{T}"/> to the database by encoding it as JSON.
	/// </summary>
	/// <typeparam name="T">Type to handle</typeparam>
	void AddEnumeratedListTypeHandler<T>()
	   where T : Enumerated;

	/// <summary>
	/// Persist a nullable Guid type to the database.
	/// </summary>
	void AddGuidTypeHandler();

	/// <summary>
	/// Persist list types to the database by encoding as JSON:<br/>
	///  - <see cref="Collections.ImmutableList{T}"/><br/>
	///  - <see cref="System.Collections.Generic.List{T}"/>
	/// </summary>
	/// <typeparam name="T"></typeparam>
	void AddListTypeHandlers<T>();

	/// <summary>
	/// Persist a type to the database by encoding it as JSON.
	/// </summary>
	/// <typeparam name="T">Type to handle</typeparam>
	void AddJsonTypeHandler<T>();

	/// <summary>
	/// Persist <see cref="StrongId.IStrongId"/> properties to the database.
	/// </summary>
	void AddStrongIdTypeHandlers();

	/// <summary>
	/// Persist <see cref="Cryptography.Locked{T}"/> properties to the database.
	/// </summary>
	void AddLockedTypeHandlers();

	/// <summary>
	/// Add generic type handlers.
	/// </summary>
	/// <typeparam name="T">Base (abstract or interface) type to map</typeparam>
	/// <param name="handlerType">Handler type (with generic argument).</param>
	/// <param name="addTypeHandler">Function to add a type handler.</param>
	void AddGenericTypeHandlers<T>(Type handlerType, AddGenericTypeHandler addTypeHandler);

	/// <summary>
	/// For testing - used to register a type handler.
	/// </summary>
	/// <param name="type">Base (abstract or interface) type to map.</param>
	/// <param name="handler">Handler type (with generic argument).</param>
	delegate void AddGenericTypeHandler(Type type, SqlMapper.ITypeHandler handler);

	/// <summary>
	/// Whether or not the specified type has a custom type handler registered.
	/// </summary>
	/// <typeparam name="T">The type to check</typeparam>
	bool HasTypeHandler<T>();
}
