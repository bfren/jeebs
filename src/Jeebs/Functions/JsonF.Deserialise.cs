// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;
using System.Text.Json;
using Jeebs.Messages;

namespace Jeebs.Functions;

public static partial class JsonF
{
	/// <summary>
	/// Use JsonSerializer to deserialise a given string into a given object type
	/// </summary>
	/// <typeparam name="T">The type of the object to return</typeparam>
	/// <param name="str">The string to deserialise</param>
	/// <param name="options">JsonSerializerOptions</param>
	public static Maybe<T> Deserialise<T>(string str, JsonSerializerOptions options)
	{
		// Check for null string
		if (str is null || string.IsNullOrWhiteSpace(str))
		{
			return F.None<T, M.DeserialisingNullOrEmptyStringMsg>();
		}

		// Attempt to deserialise JSON
		try
		{
			return JsonSerializer.Deserialize<T>(str, options) switch
			{
				T x =>
					x,

				_ =>
					F.None<T, M.DeserialisingReturnedNullMsg>() // should never get here
			};
		}
		catch (Exception ex)
		{
			return F.None<T>(new M.DeserialiseExceptionMsg(ex));
		}
	}

	/// <inheritdoc cref="Deserialise{T}(string, JsonSerializerOptions)"/>
	public static Maybe<T> Deserialise<T>(string str) =>
		Deserialise<T>(str, Options);

	public static partial class M
	{
		/// <summary>Exception caught during <see cref="JsonSerializer.Serialize{TValue}(TValue, JsonSerializerOptions?)"/></summary>
		/// <param name="Value">Exception object</param>
		public sealed record class SerialiseExceptionMsg(Exception Value) : ExceptionMsg;
	}
}
