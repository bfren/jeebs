// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;
using Dapper;
using Jeebs.Cryptography;
using Jeebs.Data.TypeHandlers;
using Jeebs.Functions;
using StrongId;
using static Jeebs.Data.IDbTypeMapper;

namespace Jeebs.Data;

/// <summary>
/// Default DbType mapper
/// </summary>
public class DbTypeMapper : IDbTypeMapper
{
	#region Static

	/// <summary>
	/// Default (global) instance
	/// </summary>
	internal static IDbTypeMapper Instance =>
		LazyInstance.Value;

	/// <summary>
	/// Lazily create a <see cref="DbTypeMapper"/>
	/// </summary>
	private static Lazy<IDbTypeMapper> LazyInstance { get; } = new(() => new DbTypeMapper(), true);

	#endregion Static

	/// <summary>
	/// Only allow creation from <see cref="Db"/> or implementing classes
	/// </summary>
	protected internal DbTypeMapper() { }

	/// <summary>
	/// Reset type handlers
	/// </summary>
	public virtual void ResetTypeHandlers() =>
		SqlMapper.ResetTypeHandlers();

	/// <summary>
	/// Add a type handler
	/// </summary>
	/// <typeparam name="T">Type to handle</typeparam>
	/// <param name="typeHandler">Type handler</param>
	public virtual void AddTypeHandler<T>(SqlMapper.TypeHandler<T> typeHandler) =>
		SqlMapper.AddTypeHandler(typeHandler);

	/// <summary>
	/// Persist an <see cref="Collections.EnumeratedList{T}"/> to the database by encoding it as JSON
	/// </summary>
	/// <typeparam name="T">Type to handle</typeparam>
	public virtual void AddEnumeratedListTypeHandler<T>()
		where T : Enumerated =>
		SqlMapper.AddTypeHandler(new EnumeratedListJsonTypeHandler<T>());

	/// <summary>
	/// Persist a nullable Guid type to the database
	/// </summary>
	public virtual void AddGuidTypeHandler() =>
		SqlMapper.AddTypeHandler(new GuidTypeHandler());

	/// <summary>
	/// Persist an <see cref="Collections.ImmutableList{T}"/> to the database by encoding it as JSON
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public virtual void AddImmutableListTypeHandler<T>() =>
		SqlMapper.AddTypeHandler(new ImmutableListJsonTypeHandler<T>());

	/// <summary>
	/// Persist a type to the database by encoding it as JSON
	/// </summary>
	/// <typeparam name="T">Type to handle</typeparam>
	public virtual void AddJsonTypeHandler<T>() =>
		SqlMapper.AddTypeHandler(new JsonTypeHandler<T>());

	/// <summary>
	/// Persist <see cref="IStrongId"/> properties to the database
	/// </summary>
	public virtual void AddStrongIdTypeHandlers() =>
		AddGenericTypeHandlers<IStrongId>(typeof(StrongIdTypeHandler<>), SqlMapper.AddTypeHandler);

	/// <summary>
	/// Persist <see cref="Locked{T}"/> properties to the database
	/// </summary>
	public virtual void AddLockedTypeHandlers() =>
		AddGenericTypeHandlers<Locked>(typeof(JsonTypeHandler<>), SqlMapper.AddTypeHandler);

	/// <summary>
	/// Add generic type handlers
	/// </summary>
	/// <typeparam name="T">Base (abstract or interface) type to map</typeparam>
	/// <param name="handlerType">Handler type (with generic argument)</param>
	/// <param name="addTypeHandler">Function to add a type handler</param>
	public virtual void AddGenericTypeHandlers<T>(Type handlerType, AddGenericTypeHandler addTypeHandler)
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
}
