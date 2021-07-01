// Jeebs Rapid Application Development
// Copyright (c) bfren.uk - licensed under https://mit.bfren.uk/2013

using System;
using System.Collections.Generic;

namespace Jeebs
{
	/// <summary>
	/// Enumerated List
	/// </summary>
	/// <typeparam name="T">Enumerated value type</typeparam>
	public sealed class EnumeratedList<T> : List<T>
		where T : Enumerated
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
				if (Activator.CreateInstance(typeof(T), item) is T obj)
				{
					Add(obj);
				}
			}
		}

		/// <summary>
		/// Serialise list as JSON
		/// </summary>
		public Option<string> Serialise()
		{
			var list = new List<string>();
			ForEach(e => list.Add(e));

			return (list.Count > 0) switch
			{
				true =>
					F.JsonF.Serialise(list),

				false =>
					F.JsonF.Empty
			};
		}

		/// <summary>
		/// Deserialise list from JSON
		/// </summary>
		/// <param name="json">JSON serialised list</param>
		public static EnumeratedList<T> Deserialise(string json)
		{
			var strings = F.JsonF.Deserialise<List<string>>(json).Unwrap(() => new List<string>());
			return new EnumeratedList<T>(strings);
		}
	}
}
