// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs;

namespace F;

public static partial class MaybeF
{
	/// <summary>
	/// Special case for boolean - returns Some{bool}(true)
	/// </summary>
	public static Maybe<bool> True =>
		true;

	/// <summary>
	/// Special case for boolean - returns Some{bool}(false)
	/// </summary>
	public static Maybe<bool> False =>
		false;
}
