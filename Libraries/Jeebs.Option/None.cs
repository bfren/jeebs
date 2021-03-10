﻿// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

namespace Jeebs
{
	/// <summary>
	/// 'None' option - used to replace null returns (see <seealso cref="Some{T}"/>)
	/// </summary>
	/// <typeparam name="T">Option value type</typeparam>
	public sealed record None<T> : Option<T>
	{
		/// <summary>
		/// Sometimes a reason for the 'None' value may be set
		/// </summary>
		public IMsg? Reason { get; private init; }

		internal None() { }

		internal None(IMsg? reason) =>
			Reason = reason;

		/// <summary>
		/// Add a reason why this option is returning <see cref="None{T}"/>
		/// </summary>
		/// <typeparam name="TMsg">Reason message type</typeparam>
		public None<T> AddReason<TMsg>()
			where TMsg : IMsg, new() =>
			this with { Reason = new TMsg() };

		/// <summary>
		/// Add a reason why this option is returning <see cref="None{T}"/>
		/// </summary>
		/// <typeparam name="TMsg">Reason message type</typeparam>
		/// <param name="ex">Exception</param>
		public None<T> AddReason<TMsg>(System.Exception ex)
			where TMsg : IExceptionMsg, new() =>
			this with { Reason = new TMsg { Exception = ex } };

		/// <summary>
		/// Return <see cref="Reason"/>.ToString(), or the name of <typeparamref name="T"/>
		/// </summary>
		public override string ToString() =>
			Reason?.ToString() switch
			{
				string reason =>
					reason,

				_ =>
					"None: " + typeof(T).ToString()
			};
	}
}