// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;
using System.Collections.Generic;
using Dapper;
using Jeebs.Cryptography;
using Jeebs.Data.TypeHandlers;
using Jeebs.Functions;
using Wrap.Dapper;
using static Jeebs.Data.IDbTypeMapper;

namespace Jeebs.Data;

/// <summary>
/// Default DbType mapper.
/// </summary>
public class DbTypeMapper : IDbTypeMapper
{
	#region Static

	/// <summary>
	/// Default (global) instance.
	/// </summary>
	internal static IDbTypeMapper Instance =>
		LazyInstance.Value;

	/// <summary>
	/// Lazily create a <see cref="DbTypeMapper"/>.
	/// </summary>
	private static Lazy<IDbTypeMapper> LazyInstance { get; } = new(() => new DbTypeMapper(), true);

	#endregion Static

	/// <summary>
	/// Only allow creation from <see cref="Db"/> or implementing classes.
	/// </summary>
	protected internal DbTypeMapper() { }

	/// <inheritdoc/>
	public virtual void ResetTypeHandlers() =>
		SqlMapper.ResetTypeHandlers();

	/// <inheritdoc/>
	public virtual void AddTypeHandler<T>(SqlMapper.TypeHandler<T> typeHandler) =>
		SqlMapper.AddTypeHandler(typeHandler);

	/// <inheritdoc/>
	public virtual void AddEnumeratedListTypeHandler<T>()
		where T : Enumerated =>
		AddTypeHandler(new EnumeratedListJsonTypeHandler<T>());

	/// <inheritdoc/>
	public virtual void AddGuidTypeHandler() =>
		AddTypeHandler(new GuidTypeHandler());

	/// <inheritdoc/>
	public virtual void AddListTypeHandlers<T>()
	{
		AddTypeHandler(new JsonTypeHandler<T[]>());
		AddTypeHandler(new JsonTypeHandler<List<T>>());
		AddTypeHandler(new ImmutableListJsonTypeHandler<T>());
	}

	/// <inheritdoc/>
	public virtual void AddJsonTypeHandler<T>() =>
		AddTypeHandler(new JsonTypeHandler<T>());

	/// <inheritdoc/>
	public virtual void AddStrongIdTypeHandlers()
	{
		AddGenericTypeHandlers<IGuidId>(typeof(GuidIdTypeHandler<>), SqlMapper.AddTypeHandler);
		AddGenericTypeHandlers<IIntId>(typeof(IntIdTypeHandler<>), SqlMapper.AddTypeHandler);
		AddGenericTypeHandlers<ILongId>(typeof(LongIdTypeHandler<>), SqlMapper.AddTypeHandler);
		AddGenericTypeHandlers<IUintId>(typeof(UIntIdTypeHandler<>), SqlMapper.AddTypeHandler);
		AddGenericTypeHandlers<IULongId>(typeof(ULongIdTypeHandler<>), SqlMapper.AddTypeHandler);
	}

	/// <inheritdoc/>
	public virtual void AddLockedTypeHandlers() =>
		AddGenericTypeHandlers<Locked>(typeof(JsonTypeHandler<>), SqlMapper.AddTypeHandler);

	/// <inheritdoc/>
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

	/// <inheritdoc/>
	public virtual bool HasTypeHandler<T>() =>
		SqlMapper.HasTypeHandler(typeof(T));
}
