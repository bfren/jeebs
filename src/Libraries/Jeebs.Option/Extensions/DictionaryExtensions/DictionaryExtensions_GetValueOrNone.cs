// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System.Collections.Generic;

namespace Jeebs.Linq
{
	/// <summary>
	/// Dictionary Extensions : GetValueOrNone
	/// </summary>
	public static class DictionaryExtensions_GetValueOrNone
	{
		/// <inheritdoc cref="F.OptionF.Dictionary.GetValueOrNone{TKey, TValue}(IDictionary{TKey, TValue}, TKey)"/>
		public static Option<TValue> GetValueOrNone<TKey, TValue>(this IDictionary<TKey, TValue> @this, TKey key) =>
			F.OptionF.Dictionary.GetValueOrNone(@this, key);
	}
}
