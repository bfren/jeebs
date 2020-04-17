using System;
using System.Collections.Generic;
using System.Text;

namespace Jeebs.Data.TypeHandlers
{
	/// <summary>
	/// EnumList TypeHandler
	/// </summary>
	/// <typeparam name="T">Enum type</typeparam>
	public sealed class EnumListTypeHandler<T> : JsonTypeHandler<EnumList<T>>
		where T : Enum
	{
		/// <summary>
		/// Convert to list of string values
		/// </summary>
		/// <param name="value">Enum list</param>
		/// <returns>JSON</returns>
		protected override string Format(EnumList<T> value)
		{
			var list = new List<string>();
			foreach (var item in value)
			{
				list.Add(item.ToString());
			}

			return Util.Json.Serialise(list);
		}

		/// <summary>
		/// Parse from list of string values and convert
		/// </summary>
		/// <param name="json">JSON string</param>
		protected override EnumList<T> Parse(string json)
		{
			var strings = Util.Json.Deserialise<List<string>>(json);
			return new EnumList<T>(strings);
		}
	}
}
