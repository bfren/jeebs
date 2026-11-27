// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;
using System.Text.Json;

namespace Jeebs.Functions;

public static partial class JsonF
{
	/// <summary>
	/// Use JsonSerializer to deserialise a given string into a given object type.
	/// </summary>
	/// <typeparam name="T">The type of the object to return.</typeparam>
	/// <param name="str">The string to deserialise.</param>
	/// <param name="options">JsonSerializerOptions.</param>
	/// <returns>Deserialised object.</returns>
	public static Result<T> Deserialise<T>(string? str, JsonSerializerOptions options)
	{
		// Check for null string
		if (string.IsNullOrWhiteSpace(str) || string.Equals(str, Empty, StringComparison.Ordinal))
		{
			return NullOrEmptyString();
		}

		// Attempt to deserialise JSON
		try
		{
			return JsonSerializer.Deserialize<T>(str, options) switch
			{
				T x =>
					x,

				_ =>
					NullObjectReturned() // should never get here
			};
		}
		catch (Exception ex)
		{
			return R.Fail(ex);
		}
	}

	/// <inheritdoc cref="Deserialise{T}(string, JsonSerializerOptions)"/>
	public static Result<T> Deserialise<T>(string? str) =>
		Deserialise<T>(str, Options);

	[Failure("Cannot deserialise a null or empty string.")]
	private static partial Failure NullOrEmptyString();

	[Failure("Json deserialise returned a null object.")]
	private static partial Failure NullObjectReturned();
}
