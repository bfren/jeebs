// Jeebs Rapid Application Development
// Copyright (c) bfren.uk - licensed under https://mit.bfren.uk/2013

using System.Text.Json;
using System.Text.Json.Serialization;
using Jeebs;

namespace F.Internals
{
	/// <summary>
	/// <see cref="IStrongId"/> Converter Factory
	/// </summary>
	public sealed class StrongIdConverterFactory : JsonConverterFactory
	{
		/// <summary>
		/// Returns true if <paramref name="typeToConvert"/> inherits from <see cref="IStrongId"/>
		/// </summary>
		/// <param name="typeToConvert"><see cref="IStrongId"/> type</param>
		public override bool CanConvert(Type typeToConvert) =>
			typeof(IStrongId).IsAssignableFrom(typeToConvert);

		/// <summary>
		/// Creates JsonConverter using <see cref="IStrongId"/> type as generic argument
		/// </summary>
		/// <param name="typeToConvert"><see cref="IStrongId"/> type</param>
		/// <param name="options">JsonSerializerOptions</param>
		public override JsonConverter CreateConverter(Type typeToConvert, JsonSerializerOptions options)
		{
			var converterType = typeof(StrongIdConverter<>).MakeGenericType(typeToConvert);
			return Activator.CreateInstance(converterType) switch
			{
				JsonConverter x =>
					x,

				_ =>
					throw new JsonException($"Unable to create {typeof(StrongIdConverter<>)} for type {typeToConvert}.")
			};
		}
	}
}
