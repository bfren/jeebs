// Jeebs Rapid Application Development
// Copyright (c) bfren.uk - licensed under https://mit.bfren.uk/2013

using Jeebs;

namespace F
{
	public static partial class OptionF
	{
		/// <summary>
		/// If <paramref name="option"/> is <see cref="Jeebs.None{T}"/> and the reason is <see cref="Msg.NullValueMsg"/>,
		/// or <paramref name="option"/> is <see cref="Some{T}"/> and <see cref="Some{T}.Value"/> is null,
		/// runs <paramref name="ifNull"/> - which gives you the opportunity to return a more useful 'Not Found' message
		/// </summary>
		/// <typeparam name="T">Option value type</typeparam>
		/// <param name="option">Input option</param>
		/// <param name="ifNull">Runs if a null value was found</param>
		public static Option<T> IfNull<T>(Option<T> option, Func<Option<T>> ifNull) =>
			Catch(() =>
				option switch
				{
					Some<T> x when x.Value is null =>
						ifNull(),

					None<T> x when x.Reason is Msg.NullValueMsg =>
						ifNull(),

					_ =>
						option
				},
				DefaultHandler
			);

		/// <inheritdoc cref="IfNull{T}(Option{T}, Func{Option{T}})"/>
		/// <typeparam name="T">Option value type</typeparam>
		/// <typeparam name="TMsg">Reason type</typeparam>
		public static Option<T> IfNull<T, TMsg>(Option<T> option, Func<TMsg> ifNull)
			where TMsg : IMsg =>
			IfNull(option, () => None<T>(ifNull()));
	}
}
