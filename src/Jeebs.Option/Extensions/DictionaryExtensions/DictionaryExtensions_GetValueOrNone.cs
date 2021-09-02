// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

namespace Jeebs.Linq;

/// <summary>
/// Dictionary Extensions : GetValueOrNone
/// </summary>
public static class DictionaryExtensions_GetValueOrNone
{
	/// <inheritdoc cref="F.OptionF.Dictionary.GetValueOrNone{TKey, TValue}(IDictionary{TKey, TValue}, TKey)"/>
	public static Option<TValue> GetValueOrNone<TKey, TValue>(this IDictionary<TKey, TValue> @this, TKey key) =>
		F.OptionF.Dictionary.GetValueOrNone(@this, key);
}
