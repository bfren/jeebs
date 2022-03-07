// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System.Collections.Generic;

namespace Jeebs.Linq;

/// <summary>
/// Dictionary Extensions : GetValueOrNone
/// </summary>
public static class DictionaryExtensionsGetValueOrNone
{
	/// <inheritdoc cref="F.MaybeF.Dictionary.GetValueOrNone{TKey, TValue}(IDictionary{TKey, TValue}, TKey)"/>
	public static Maybe<TValue> GetValueOrNone<TKey, TValue>(this IDictionary<TKey, TValue> @this, TKey key) =>
		F.MaybeF.Dictionary.GetValueOrNone(@this, key);
}
