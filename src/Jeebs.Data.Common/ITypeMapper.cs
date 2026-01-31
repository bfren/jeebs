// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;

namespace Jeebs.Data.Common;

/// <summary>
/// Add custom type maps.
/// </summary>
public interface ITypeMapper
{
	/// <summary>
	/// Reset type handlers.
	/// </summary>
	void ResetTypeHandlers();

	/// <summary>
	/// Persist an <see cref="Collections.EnumeratedList{T}"/> to the database by encoding it as JSON.
	/// </summary>
	/// <typeparam name="T">Type to handle.</typeparam>
	void AddEnumeratedListTypeHandler<T>()
	   where T : Enumerated;

	/// <summary>
	/// Persist a nullable Guid type to the database.
	/// </summary>
	void AddGuidTypeHandler();

	/// <summary>
	/// Persist <see cref="Id{TId, TValue}"/> properties to the database.
	/// </summary>
	void AddIdTypeHandlers();

	/// <summary>
	/// Persist a type to the database by encoding it as JSON.
	/// </summary>
	/// <typeparam name="T">Type to handle.</typeparam>
	void AddJsonTypeHandler<T>();

	/// <summary>
	/// Persist list types to the database by encoding as JSON:<br/>
	///  - <see cref="Collections.ImmutableList{T}"/><br/>
	///  - <see cref="System.Collections.Generic.List{T}"/>
	/// </summary>
	/// <typeparam name="T"></typeparam>
	void AddListTypeHandlers<T>();

	/// <summary>
	/// Persist <see cref="Cryptography.Locked{T}"/> properties to the database.
	/// </summary>
	void AddLockedTypeHandlers();

	/// <summary>
	/// Add generic type handlers.
	/// </summary>
	/// <typeparam name="T">Base (abstract or interface) type to map.</typeparam>
	/// <param name="handlerType">Handler type (with generic argument).</param>
	/// <param name="addTypeHandler">Function to add a type handler.</param>
	void AddGenericTypeHandlers<T>(Type handlerType);

	/// <summary>
	/// Whether or not the specified type has a custom type handler registered.
	/// </summary>
	/// <typeparam name="T">The type to check.</typeparam>
	bool HasTypeHandler<T>();
}
