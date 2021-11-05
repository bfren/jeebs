// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

namespace Jeebs.Data.TypeHandlers;

/// <summary>
/// EnumeratedList TypeHandler
/// </summary>
/// <typeparam name="T">Enumerated type</typeparam>
public sealed class JsonbEnumeratedListTypeHandler<T> : JsonbTypeHandler<EnumeratedList<T>>
	where T : Enumerated
{
	/// <summary>
	/// Parse from list of string values and convert
	/// </summary>
	/// <param name="value">Database value</param>
	public override EnumeratedList<T> Parse(object value) =>
		value switch
		{
			string json =>
				EnumeratedList<T>.Deserialise(json),

			_ =>
				new()
		};
}
