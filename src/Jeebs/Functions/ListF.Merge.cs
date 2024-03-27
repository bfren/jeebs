// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System.Linq;
using Jeebs.Collections;

namespace Jeebs.Functions;

public static partial class ListF
{
	/// <summary>
	/// Merge multiple <see cref="ImmutableList{T}"/> objects into one.
	/// </summary>
	/// <typeparam name="T">List Item type.</typeparam>
	/// <param name="lists">Lists to merge.</param>
	/// <returns>Merged lists.</returns>
	public static ImmutableList<T> Merge<T>(params IImmutableList<T>[] lists) =>
		new(lists.SelectMany(x => x));
}
