﻿// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using Jeebs;

namespace F
{
	public static partial class OptionF
	{
		/// <summary>
		/// Create a <see cref="Jeebs.None{T}"/> Option with a Reason message
		/// </summary>
		/// <typeparam name="T">Option value type</typeparam>
		/// <param name="reason">Reason message</param>
		public static None<T> None<T>(IMsg reason) =>
			new(reason);

		/// <summary>
		/// Create a <see cref="Jeebs.None{T}"/> Option with a Reason message by type
		/// </summary>
		/// <typeparam name="T">Option value type</typeparam>
		/// <typeparam name="TMsg">Reason message type</typeparam>
		public static None<T> None<T, TMsg>()
			where TMsg : IMsg, new() =>
			new(new TMsg());

		internal static None<T> None<T>(bool areYouSure) =>
			areYouSure switch
			{
				true =>
					new(new Msg.NoReasonGivenMsg()),

				false =>
					new(new Msg.IfYouArentSureDontMakeItMsg())
			};

		/// <summary>Messages</summary>
		public static partial class Msg
		{
			/// <summary>No reason given</summary>
			public sealed record NoReasonGivenMsg : IMsg { }

			/// <summary>If you aren't sure you want to make a <see cref="Jeebs.None{T}"/> without a reason, don't do it!</summary>
			public sealed record IfYouArentSureDontMakeItMsg : IMsg { }
		}
	}
}