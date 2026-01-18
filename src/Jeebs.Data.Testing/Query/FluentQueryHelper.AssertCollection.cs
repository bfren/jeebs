// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Data.Testing.Exceptions;
using Xunit.Sdk;

namespace Jeebs.Data.Testing.Query;

public static partial class FluentQueryHelper
{
	/// <summary>
	/// Provides alternative to <see cref="Assert.Collection{T}(IEnumerable{T}, Action{T}[])"/>
	/// that doesn't wrap thrown <see cref="FluentQueryHelperException"/> objects
	/// </summary>
	/// <typeparam name="T">Value type</typeparam>
	/// <param name="collection">Calls.</param>
	/// <param name="inspectors">Element Inspectors.</param>
	/// <exception cref="CollectionException"></exception>
	internal static void AssertCollection<T>(T[] collection, params Action<T>[] inspectors)
	{
		// number of items does not match
		var expected = inspectors.Length;
		var actual = collection.Length;
		if (expected != actual)
		{
			throw CollectionException.ForMismatchedItemCount(expected, actual, string.Join(", ", collection));
		}

		//
		for (var i = 0; i < actual; i++)
		{
			try
			{
				inspectors[i].Invoke(collection[i]);
			}
			catch (Exception ex) when (ex is not FluentQueryHelperException)
			{
				throw CollectionException.ForMismatchedItem(ex, i, null, string.Join(", ", collection));
			}
		}
	}
}
