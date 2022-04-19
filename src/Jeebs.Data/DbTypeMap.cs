// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;
using Dapper;
using Jeebs.Cryptography;
using Jeebs.Data.TypeHandlers;
using Jeebs.Functions;
using StrongId;

namespace Jeebs.Data;

/// <summary>
/// Add custom type mapping
/// </summary>
public sealed class DbTypeMap
{
	/// <summary>
	/// Only allow creation from <see cref="Db"/>
	/// </summary>
	internal DbTypeMap() { }

	/// <summary>
	/// Reset type handlers
	/// </summary>
	public void ResetTypeHandlers() =>
		SqlMapper.ResetTypeHandlers();

	/// <summary>
	/// Add a type handler
	/// </summary>
	/// <typeparam name="T">Type to handle</typeparam>
	/// <param name="typeHandler">Type handler</param>
	public void AddTypeHandler<T>(SqlMapper.TypeHandler<T> typeHandler) =>
		SqlMapper.AddTypeHandler(typeHandler);

	/// <summary>
	/// Persist an <see cref="Collections.EnumeratedList{T}"/> to the database by encoding it as JSON
	/// </summary>
	/// <typeparam name="T">Type to handle</typeparam>
	public void AddEnumeratedListJsonTypeHandler<T>()
		where T : Enumerated =>
		SqlMapper.AddTypeHandler(new EnumeratedListJsonTypeHandler<T>());

	/// <summary>
	/// Persist a nullable Guid type to the database
	/// </summary>
	public void AddGuidTypeHandler() =>
		SqlMapper.AddTypeHandler(new GuidTypeHandler());

	/// <summary>
	/// Persist an <see cref="Collections.ImmutableList{T}"/> to the database by encoding it as JSON
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public void AddImmutableListJsonTypeHandler<T>() =>
		SqlMapper.AddTypeHandler(new ImmutableListJsonTypeHandler<T>());

	/// <summary>
	/// Persist a type to the database by encoding it as JSON
	/// </summary>
	/// <typeparam name="T">Type to handle</typeparam>
	public void AddJsonTypeHandler<T>() =>
		SqlMapper.AddTypeHandler(new JsonTypeHandler<T>());

	/// <summary>
	/// Persist <see cref="IStrongId"/> properties to the database
	/// </summary>
	public void AddStrongIdTypeHandlers() =>
		AddGenericTypeHandlers<IStrongId>(typeof(StrongIdTypeHandler<>), SqlMapper.AddTypeHandler);

	/// <summary>
	/// Persist <see cref="Locked{T}"/> properties to the database
	/// </summary>
	public void AddLockedJsonTypeHandlers() =>
		AddGenericTypeHandlers<Locked>(typeof(JsonTypeHandler<>), SqlMapper.AddTypeHandler);

	/// <summary>
	/// Add generic type handlers
	/// </summary>
	/// <typeparam name="T">Base (abstract or interface) type to map</typeparam>
	/// <param name="handlerType">Handler type (with generic argument)</param>
	/// <param name="addTypeHandler">Function to add a type handler</param>
	public void AddGenericTypeHandlers<T>(Type handlerType, AddGenericTypeHandler addTypeHandler)
	{
		if (!handlerType.ContainsGenericParameters)
		{
			return;
		}

		if (!typeof(SqlMapper.ITypeHandler).IsAssignableFrom(handlerType))
		{
			return;
		}

		TypeF.GetPropertyTypesImplementing<T>().ForEach(t =>
		{
			var genericHandlerType = handlerType.MakeGenericType(t);
			if (Activator.CreateInstance(genericHandlerType) is SqlMapper.ITypeHandler handler)
			{
				addTypeHandler(t, handler);
			}
		});
	}

	/// <summary>
	/// For testing - used to register a type handler
	/// </summary>
	/// <param name="type">Base (abstract or interface) type to map</param>
	/// <param name="handler">Handler type (with generic argument)</param>
	public delegate void AddGenericTypeHandler(Type type, SqlMapper.ITypeHandler handler);
}
