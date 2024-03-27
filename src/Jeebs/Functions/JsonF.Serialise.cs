// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System.Text.Json;

namespace Jeebs.Functions;

public static partial class JsonF
{
	/// <summary>
	/// Use JsonSerializer to serialise a given object.
	/// </summary>
	/// <typeparam name="T">Object Type to be serialised.</typeparam>
	/// <param name="obj">The object to serialise.</param>
	/// <param name="options">JsonSerializerOptions.</param>
	/// <returns>Serialised string.</returns>
	public static Result<string> Serialise<T>(T obj, JsonSerializerOptions options) =>
		obj switch
		{
			T x =>
				R.Try(() => JsonSerializer.Serialize(x, options)),

			_ =>
				Empty
		};

	/// <inheritdoc cref="Serialise{T}(T, JsonSerializerOptions)"/>
	public static Result<string> Serialise<T>(T obj) =>
		Serialise(obj, Options);
}
