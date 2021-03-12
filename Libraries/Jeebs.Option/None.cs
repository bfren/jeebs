// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using Jeebs;

namespace JeebsF
{
	/// <summary>
	/// 'None' option - used to replace null returns (see <seealso cref="Some{T}"/>)
	/// </summary>
	/// <typeparam name="T">Option value type</typeparam>
	public sealed class None<T> : Option<T>
	{
		/// <summary>
		/// Sometimes a reason for the 'None' value may be set
		/// </summary>
		public IMsg? Reason { get; private init; }

		/// <summary>
		/// Return generic <see cref="Option{T}"/> - useful when wrapping in Task.FromResult() or similar,
		/// when implicit casting doesn't work
		/// </summary>
		public Option<T> AsOption =>
			this;

		internal None(IMsg? reason) =>
			Reason = reason;

		/// <summary>
		/// Add a reason why this option is returning <see cref="None{T}"/>
		/// </summary>
		/// <typeparam name="TMsg">Reason message type</typeparam>
		public None<T> AddReason<TMsg>()
			where TMsg : IMsg, new() =>
			new(new TMsg());

		/// <summary>
		/// Add a reason why this option is returning <see cref="None{T}"/>
		/// </summary>
		/// <typeparam name="TMsg">Reason message type</typeparam>
		/// <param name="ex">Exception</param>
		public None<T> AddReason<TMsg>(System.Exception ex)
			where TMsg : IExceptionMsg, new() =>
			new(new TMsg { Exception = ex });

		/// <summary>
		/// Return:
		///    Reason (if one is set)
		/// </summary>
		public override string ToString() =>
			base.ToString();
	}
}
