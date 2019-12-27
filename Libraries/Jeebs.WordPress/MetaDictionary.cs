using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Jeebs.WordPress
{
	/// <summary>
	/// Meta Dictionary
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
					this[item.Key] += $"; {item.Value}";
				}
				else
				{
					Add(item.Key, item.Value);
				}
			}
		}
	}
}
