// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;
using System.Data.Common;
using Jeebs.Data.Map;
using Jeebs.Messages;

namespace Jeebs.Data;

/// <inheritdoc cref="IDbClient"/>
public abstract partial class DbClient : IDbClient
{
	/// <summary>
	/// IEntityMapper
	/// </summary>
	public IEntityMapper Entities { get; private init; }

	/// <summary>
	/// IDbTypeMapper
	/// </summary>
	public IDbTypeMapper Types { get; private init; }

	/// <summary>
	/// Create using default instances
	/// </summary>
	protected DbClient() : this(EntityMapper.Instance, DbTypeMapper.Instance) { }

	/// <summary>
	/// Inject dependencies
	/// </summary>
	/// <param name="types"></param>
	protected DbClient(IDbTypeMapper types) : this(EntityMapper.Instance, types) { }

	/// <summary>
	/// Inject dependencies
	/// </summary>
	/// <param name="entities"></param>
	/// <param name="types"></param>
	protected DbClient(IEntityMapper entities, IDbTypeMapper types) =>
		(Entities, Types) = (entities, types);

	/// <inheritdoc/>
	public abstract DbConnection GetConnection(string connectionString);

	/// <summary>Messages</summary>
	public static class M
	{
		/// <summary>Error getting General Retrieve query</summary>
		/// <param name="Value">Exception object</param>
		public sealed record class ErrorGettingGeneralRetrieveQueryExceptionMsg(Exception Value) : ExceptionMsg;

		/// <summary>Error getting CRUD Create query</summary>
		/// <param name="Value">Exception object</param>
		public sealed record class ErrorGettingCrudCreateQueryExceptionMsg(Exception Value) : ExceptionMsg;

		/// <summary>Error getting CRUD Retrieve query</summary>
		/// <param name="Value">Exception object</param>
		public sealed record class ErrorGettingCrudRetrieveQueryExceptionMsg(Exception Value) : ExceptionMsg;

		/// <summary>Error getting CRUD Update query</summary>
		/// <param name="Value">Exception object</param>
		public sealed record class ErrorGettingCrudUpdateQueryExceptionMsg(Exception Value) : ExceptionMsg;

		/// <summary>Error getting CRUD Delete query</summary>
		/// <param name="Value">Exception object</param>
		public sealed record class ErrorGettingCrudDeleteQueryExceptionMsg(Exception Value) : ExceptionMsg;
	}
}
