using System;
using System.Collections.Generic;
using System.Text;

namespace Jeebs
{
	/// <summary>
	/// Enumerated List
	/// </summary>
	/// <typeparam name="TEnum">Enumerated value type</typeparam>
	public sealed class EnumeratedList<TEnum> : List<TEnum>
		where TEnum : Enumerated
	{
		/// <summary>
		/// Empty constructor
		/// </summary>
		public EnumeratedList() { }

		/// <summary>
		/// Construct object from list of string values
		/// </summary>
		/// <param name="list">List of values</param>
		public EnumeratedList(List<string> list)
		{
			if (list is null)
			{
				return;
			}

			foreach (var item in list)
			{
				Add((TEnum)Activator.CreateInstance(typeof(TEnum), item));
			}
		}

		/// <summary>
		/// Serialise list as JSON
		/// </summary>
		/// <returns>JSON</returns>
		public string Serialise()
		{
			var list = new List<string>();
			foreach (var item in this)
			{
				list.Add(item.ToString());
			}

			return Util.Json.Serialise(list);
		}

		/// <summary>
		/// Deserialise list from JSON
		/// </summary>
		/// <param name="json">JSON</param>
		/// <returns>EnumList</returns>
		public static EnumeratedList<TEnum> Deserialise(string json)
		{
			var strings = Util.Json.Deserialise<List<string>>(json) switch
			{
				Some<List<string>> x => x.Value,
				_ => new List<string>()
			};

			return new EnumeratedList<TEnum>(strings);
		}
	}
}
