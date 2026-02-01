// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;
using System.Collections.Generic;
using Dapper;
using Jeebs.Cryptography;
using Jeebs.Data.Adapters.Dapper.TypeHandlers;
using Jeebs.Data.Common;
using Jeebs.Functions;
using Wrap.Dapper;
using Wrap.Ids;

namespace Jeebs.Data.Adapters.Dapper;

/// <summary>
/// Default DbType mapper.
/// </summary>
public class DapperTypeMapper : ITypeMapper
{
	#region Static

	/// <summary>
	/// Default (global) instance.
	/// </summary>
	internal static DapperTypeMapper Instance =>
		LazyInstance.Value;

	/// <summary>
	/// Lazily create a <see cref="DapperTypeMapper"/>.
	/// </summary>
	private static Lazy<DapperTypeMapper> LazyInstance { get; } = new(() => new DapperTypeMapper(), true);

	#endregion Static

	/// <summary>
	/// Only allow creation by Lazy instance and implementing classes.
	/// </summary>
	protected DapperTypeMapper() { }

	/// <inheritdoc/>
	public virtual void ResetTypeHandlers() =>
		SqlMapper.ResetTypeHandlers();

	/// <inheritdoc/>
	public virtual void AddEnumeratedListTypeHandler<T>()
		where T : Enumerated =>
		SqlMapper.AddTypeHandler(new EnumeratedListJsonTypeHandler<T>());

	/// <inheritdoc/>
	public virtual void AddGuidTypeHandler() =>
		SqlMapper.AddTypeHandler(new GuidTypeHandler());

	/// <inheritdoc/>
	public virtual void AddListTypeHandlers<T>()
	{
		SqlMapper.AddTypeHandler(new JsonTypeHandler<T[]>());
		SqlMapper.AddTypeHandler(new JsonTypeHandler<List<T>>());
		SqlMapper.AddTypeHandler(new ImmutableListJsonTypeHandler<T>());
	}

	/// <inheritdoc/>
	public virtual void AddJsonTypeHandler<T>() =>
		SqlMapper.AddTypeHandler(new JsonTypeHandler<T>());

	/// <inheritdoc/>
	public virtual void AddIdTypeHandlers()
	{
		AddGenericTypeHandlers<IGuidId>(typeof(GuidIdTypeHandler<>));
		AddGenericTypeHandlers<IIntId>(typeof(IntIdTypeHandler<>));
		AddGenericTypeHandlers<ILongId>(typeof(LongIdTypeHandler<>));
		AddGenericTypeHandlers<IUintId>(typeof(UIntIdTypeHandler<>));
		AddGenericTypeHandlers<IULongId>(typeof(ULongIdTypeHandler<>));
	}

	/// <inheritdoc/>
	public virtual void AddLockedTypeHandlers() =>
		AddGenericTypeHandlers<Locked>(typeof(JsonTypeHandler<>));

	/// <inheritdoc/>
	public virtual void AddGenericTypeHandlers<T>(Type handlerType)
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
				SqlMapper.AddTypeHandler(t, handler);
			}
		});
	}

	/// <inheritdoc/>
	public bool HasTypeHandler<T>() =>
		SqlMapper.HasTypeHandler(typeof(T));
}
