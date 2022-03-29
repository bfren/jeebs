// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;
using System.Text.Json;
using Jeebs.Messages;

namespace Jeebs.Functions;

public static partial class JsonF
{
	/// <summary>
	/// Use JsonSerializer to serialise a given object
	/// </summary>
	/// <typeparam name="T">Object Type to be serialised</typeparam>
	/// <param name="obj">The object to serialise</param>
	/// <param name="options">JsonSerializerOptions</param>
	public static Maybe<string> Serialise<T>(T obj, JsonSerializerOptions options) =>
		obj switch
		{
			T x =>
				F.Some(
					() => JsonSerializer.Serialize(x, options),
					e => new M.SerialiseExceptionMsg(e)
				),

			_ =>
				Empty
		};

	/// <inheritdoc cref="Serialise{T}(T, JsonSerializerOptions)"/>
	public static Maybe<string> Serialise<T>(T obj) =>
		Serialise(obj, Options);

	public static partial class M
	{
		/// <summary>Exception caught during <see cref="JsonSerializer.Deserialize{TValue}(string, JsonSerializerOptions?)"/></summary>
		/// <param name="Value">Exception object</param>
		public sealed record class DeserialiseExceptionMsg(Exception Value) : ExceptionMsg;

		/// <summary>A null or empty string cannot be deserialised</summary>
		public sealed record class DeserialisingNullOrEmptyStringMsg : Msg;

		/// <summary>The object was deserialised but returned null</summary>
		public sealed record class DeserialisingReturnedNullMsg : Msg;
	}
}
