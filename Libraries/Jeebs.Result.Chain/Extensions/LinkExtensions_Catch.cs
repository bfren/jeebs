// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System;
using Jeebs.Fluent;
using Jm.Audit;

namespace Jeebs
{
	/// <summary>
	/// Extension methods for <see cref="ILink{TValue}"/> and <see cref="ILink{TValue, TState}"/>: Catch
	/// </summary>
	public static class LinkExtensions_Catch
	{
		/// <summary>
		/// Fluently add a generic exception handler
		/// </summary>
		/// <param name="this">ILink</param>
		public static Catch<TValue> Catch<TValue>(this ILink<TValue> @this) =>
			new(@this);

		/// <inheritdoc cref="Catch{TValue}(ILink{TValue})"/>
		public static Catch<TValue, TState> Catch<TValue, TState>(this ILink<TValue, TState> @this) =>
			new(@this);
	}
}
