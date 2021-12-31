﻿// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;
using System.Data;
using Jeebs.Data.Mapping;

namespace Jeebs.Data;

/// <inheritdoc cref="IDbClient"/>
public abstract partial class DbClient : IDbClient
{
	/// <summary>
	/// IMapper
	/// </summary>
	protected IMapper Mapper { get; private init; }

	internal IMapper MapperTest =>
		Mapper;

	/// <summary>
	/// Create using default Mapper instance
	/// </summary>
	protected DbClient() : this(Mapping.Mapper.Instance) { }

	/// <summary>
	/// Inject a Mapper instance
	/// </summary>
	/// <param name="mapper">IMapper</param>
	protected DbClient(IMapper mapper) =>
		Mapper = mapper;

	/// <inheritdoc/>
	public abstract IDbConnection Connect(string connectionString);

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
