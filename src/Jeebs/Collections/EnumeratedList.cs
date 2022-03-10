// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;
using System.Collections.Generic;
using Jeebs.Functions;
using MaybeF;

namespace Jeebs.Collections;

/// <inheritdoc cref="EnumeratedList{T}"/>
public static class EnumeratedList
{
	/// <summary>
	/// Deserialise list from JSON
	/// </summary>
	/// <typeparam name="T">Enumerated value type</typeparam>
	/// <param name="json">JSON serialised list</param>
	public static EnumeratedList<T> Deserialise<T>(string json)
		where T : Enumerated
	{
		var strings = JsonF.Deserialise<List<string>>(json).Unwrap(() => new List<string>());
		return new EnumeratedList<T>(strings);
	}
}

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
	public Maybe<string> Serialise()
	{
		var list = new List<string>();
		ForEach(e => list.Add(e));

		return (list.Count > 0) switch
		{
			true =>
				JsonF.Serialise(list),

			false =>
				JsonF.Empty
		};
	}
}
