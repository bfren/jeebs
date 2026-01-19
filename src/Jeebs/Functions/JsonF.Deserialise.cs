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
		static Result<T> fail(string message) =>
			R.Fail(nameof(JsonF), nameof(Deserialise), message);

		// Check for null string
		if (string.IsNullOrWhiteSpace(str) || string.Equals(str, Empty, StringComparison.Ordinal))
		{
			return fail("Cannot deserialise a null or empty string to JSON.");
		}

		// Attempt to deserialise JSON
		try
		{
			return JsonSerializer.Deserialize<T>(str, options) switch
			{
				T x =>
					x,

				_ =>
					fail("Json deserialise returned a null object.") // should never get here
			};
		}
		catch (Exception ex)
		{
			return R.Fail(nameof(JsonF), nameof(Deserialise), ex);
		}
	}

	/// <inheritdoc cref="Deserialise{T}(string, JsonSerializerOptions)"/>
	public static Result<T> Deserialise<T>(string? str) =>
		Deserialise<T>(str, Options);
}
