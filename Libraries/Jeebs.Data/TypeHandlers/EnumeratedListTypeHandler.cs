using System;
using System.Collections.Generic;
using System.Text;

namespace Jeebs.Data.TypeHandlers
{
	/// <summary>
	/// EnumeratedList TypeHandler
	/// </summary>
	/// <typeparam name="T">Enumerated type</typeparam>
	public sealed class EnumeratedListTypeHandler<T> : JsonTypeHandler<EnumeratedList<T>>
		where T : Enumerated
	{
		/// <summary>
		/// Convert to list of string values
		/// </summary>
		/// <param name="value">EnumeratedList</param>
		protected override string Format(EnumeratedList<T> value)
			=> value.Serialise();

		/// <summary>
		/// Parse from list of string values and convert
		/// </summary>
		/// <param name="json">JSON string</param>
		protected override EnumeratedList<T> Parse(string json)
			=> EnumeratedList<T>.Deserialise(json);
	}
}
