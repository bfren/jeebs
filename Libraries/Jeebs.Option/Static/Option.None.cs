// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

namespace Jeebs
{
	public static partial class Option
	{
		/// <summary>
		/// Create a None option
		/// </summary>
		/// <typeparam name="T">Option value type</typeparam>
		/// <param name="areYouSure">You should normally create <see cref="Jeebs.None{T}"/> objects with a Reason</param>
		public static None<T> None<T>(bool areYouSure) =>
			areYouSure switch
			{
				true =>
					new(null),

				false =>
					new(new Jm.Option.IfYouArentSureDontMakeItMsg())
			};

		/// <summary>
		/// Create a None option with a Reason message
		/// </summary>
		/// <typeparam name="T">Option value type</typeparam>
		/// <param name="reason">Reason message</param>
		public static None<T> None<T>(IMsg reason) =>
			new(reason);
	}
}
