// Copyright (c) bcg|design.
// Licensed under https://mit.bcgdesign.com/2013.

using System.Linq;

namespace Jeebs
{
	/// <summary>
	/// Extension methods for <see cref="IR"/> interface: UseLog
	/// </summary>
	public static class RExtensions_ToOption
	{
		/// <summary>
		/// Convert a result to an Option type
		/// </summary>
		/// <typeparam name="TValue">Value type</typeparam>
		/// <param name="this">Result</param>
		public static Option<TValue> ToOption<TValue>(this IR<TValue> @this) =>
			@this switch
			{
				IOkV<TValue> ok =>
					ok.Value,

				{ } e when e.HasMessages =>
					Option.None<TValue>().AddReason(e.Messages.GetEnumerable().Last()),

				_ =>
					Option.None<TValue>()
			};
	}
}
