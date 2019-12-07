using System;
using System.Collections.Generic;
using System.Text;

namespace Jeebs
{
	/// <summary>
	/// Enum List
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public sealed class EnumList<T> : List<T> 
		where T : Enum
	{
		/// <summary>
		/// Empty constructor
		/// </summary>
		public EnumList() { }

		/// <summary>
		/// Construct object from list of string values
		/// </summary>
		/// <param name="list">List of values</param>
		public EnumList(in List<string> list)
		{
			if (list == null)
			{
				return;
			}

			foreach (var item in list)
			{
				Add((T)Activator.CreateInstance(typeof(T), item));
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
		public static EnumList<T> Deserialise(in string json)
		{
			var strings = Util.Json.Deserialise<List<string>>(json);
			return new EnumList<T>(strings);
		}
	}
}
