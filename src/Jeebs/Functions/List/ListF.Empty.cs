// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Collections;

namespace Jeebs.Functions;

public static partial class ListF
{
	/// <summary>
	/// Create an empty <see cref="ImmutableList{T}"/>.
	/// </summary>
	/// <typeparam name="T">List Item type.</typeparam>
	/// <returns>Empty <see cref="ImmutableList{T}"/>.</returns>
	public static ImmutableList<T> Empty<T>() =>
		new();
}
