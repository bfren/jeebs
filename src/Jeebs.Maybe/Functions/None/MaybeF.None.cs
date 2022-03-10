// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;
using Jeebs;
using Jeebs.Internals;

namespace F;

public static partial class MaybeF
{
	/// <summary>
	/// Create a <see cref="Jeebs.Internals.None{T}"/> Maybe with a Reason message
	/// </summary>
	/// <typeparam name="T">Maybe value type</typeparam>
	/// <param name="reason">Reason message</param>
	public static None<T> None<T>(Msg reason) =>
		new(reason);

	/// <summary>
	/// Create a <see cref="Jeebs.Internals.None{T}"/> Maybe with a Reason message by type
	/// </summary>
	/// <typeparam name="T">Maybe value type</typeparam>
	/// <typeparam name="TMsg">Reason message type</typeparam>
	public static None<T> None<T, TMsg>()
		where TMsg : Msg, new() =>
		new(new TMsg());

	/// <summary>
	/// Create a <see cref="Jeebs.Internals.None{T}"/> Maybe with a Reason exception message by type<br/>
	/// NB: <typeparamref name="TExceptionMsg"/> must have a constructor with precisely one argument to
	/// receive <paramref name="ex"/> as the value, or creation will fail
	/// </summary>
	/// <typeparam name="T">Maybe value type</typeparam>
	/// <typeparam name="TExceptionMsg">Reason exception message type</typeparam>
	/// <param name="ex">Exception object</param>
	public static None<T> None<T, TExceptionMsg>(Exception ex)
		where TExceptionMsg : ExceptionMsg
	{
		var none = () => None<T>(new M.GeneralExceptionMsg<TExceptionMsg>(ex));

		try
		{
			return Activator.CreateInstance(typeof(TExceptionMsg), ex) switch
			{
				TExceptionMsg msg =>
					None<T>(msg),

				_ =>
					none()
			};
		}
		catch (Exception)
		{
			return none();
		}
	}

	public static partial class M
	{
		/// <summary>Unable to create exception message</summary>
		/// <typeparam name="TExceptionMsg">ExceptionMsg type</typeparam>
		/// <param name="Value">Exception value</param>
		public sealed record class GeneralExceptionMsg<TExceptionMsg>(Exception Value) : ExceptionMsg where TExceptionMsg : ExceptionMsg;
	}
}
