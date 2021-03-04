using System;
using System.Collections.Generic;
using System.Text;

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
		/// <param name="reason">Reason message</param>
		public None<T> AddReason<TMsg>(TMsg reason)
			where TMsg : IMsg =>
			this with { Reason = reason };

		/// <summary>
		/// Add a reason why this option is returning <see cref="None{T}"/>
		/// </summary>
		/// <typeparam name="TMsg">Reason message type</typeparam>
		public None<T> AddReason<TMsg>()
			where TMsg : IMsg, new() =>
			AddReason(new TMsg());

		/// <summary>
		/// Add a reason why this option is returning <see cref="None{T}"/>
		/// </summary>
		/// <typeparam name="TMsg">Reason message type</typeparam>
		/// <param name="ex">Exception</param>
		public None<T> AddReason<TMsg>(Exception ex)
			where TMsg : IExceptionMsg, new()
		{
			var msg = new TMsg();
			msg.Set(ex);
			return AddReason(msg);
		}
	}
}