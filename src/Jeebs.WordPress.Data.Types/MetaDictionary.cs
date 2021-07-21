﻿// Jeebs Rapid Application Development
// Copyright (c) bfren.uk - licensed under https://mit.bfren.uk/2013

using System.Collections.Generic;

namespace Jeebs.WordPress.Data
{
	/// <summary>
	/// Meta Dictionary that handles when WordPress doesn't preserve unique keys in the meta collection
	/// </summary>
	public sealed class MetaDictionary : Dictionary<string, string>
	{
		/// <summary>
		/// Create blank meta dictionary
		/// </summary>
		public MetaDictionary() { }

		/// <summary>
		/// Safely parse meta collection as a dictionary
		/// </summary>
		/// <param name="collection">Meta collection</param>
		public MetaDictionary(IEnumerable<KeyValuePair<string, string>> collection)
		{
			// Sometimes WordPress isn't very good at preserving unique keys in the meta collection
			foreach (var item in collection)
			{
				// Extend or add the item
				if (ContainsKey(item.Key))
				{
					this[item.Key] = string.Concat(this[item.Key], ";", item.Value);
				}
				else
				{
					Add(item.Key, item.Value);
				}
			}
		}
	}
}
