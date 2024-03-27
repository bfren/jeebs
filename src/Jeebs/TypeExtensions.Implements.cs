// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;

namespace Jeebs;

public static partial class TypeExtensions
{
	/// <summary>
	/// True if <paramref name="this"/> implements <typeparamref name="T"/>.
	/// </summary>
	/// <typeparam name="T">Implementation type.</typeparam>
	/// <param name="this">Base type.</param>
	/// <returns>Whether or not <paramref name="this"/> implements <typeparamref name="T"/>.</returns>
	public static bool Implements<T>(this Type @this) =>
		@this.Implements(typeof(T));

	/// <summary>
	/// True if <paramref name="this"/> implements <paramref name="type"/>.
	/// </summary>
	/// <param name="this">Base type.</param>
	/// <param name="type">Implementation type.</param>
	/// <returns>Whether or not <paramref name="this"/> implements <paramref name="type"/>.</returns>
	public static bool Implements(this Type @this, Type type)
	{
		// Handle base object
		if (@this == typeof(object))
		{
			return false;
		}

		// Handle identical types
		if (@this == type)
		{
			return false;
		}

		// Handle value types
		if (@this.IsValueType)
		{
			return false;
		}

		// Simple checks
		if (@this.IsSubclassOf(type) || type.IsAssignableFrom(@this))
		{
			return true;
		}

		// Handle generic types
		if (type.IsGenericType)
		{
			return type.IsInterface switch
			{
				true =>
					@this.ImplementsGenericInterface(type),

				false =>
					@this.ImplementsGenericClass(type)
			};
		}

		return false;
	}
}
