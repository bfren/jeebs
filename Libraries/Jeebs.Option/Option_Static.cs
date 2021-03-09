// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System;
using System.Threading.Tasks;

namespace Jeebs
{
	/// <summary>
	/// Create option types
	/// </summary>
	public static class Option
	{
		/// <summary>
		/// Create a None option
		/// </summary>
		/// <typeparam name="T">Option value type</typeparam>
		public static None<T> None<T>() =>
			new();

		/// <summary>
		/// Create a None option with a Reason message
		/// </summary>
		/// <typeparam name="T">Option value type</typeparam>
		/// <param name="reason">Reason message</param>
		public static None<T> None<T>(IMsg? reason) =>
			new(reason);

		/// <summary>
		/// Create a Some option, containing a value
		/// <para>If <paramref name="value"/> is null, <see cref="Jeebs.None{T}"/> will be returned instead</para>
		/// </summary>
		/// <typeparam name="T">Option value type</typeparam>
		/// <param name="value">Some value</param>
		public static Option<T> Wrap<T>(T value, bool allowNull = false) =>
			value switch
			{
				T x =>
					new Some<T>(x),

				_ =>
					allowNull switch
					{
						true =>
							new Some<T>(value),

						false =>
							None<T>().AddReason<Jm.Option.SomeValueWasNullMsg>()
					}

			};

		/// <summary>
		/// Wrap <paramref name="value"/> in <see cref="Wrap{T}(T)"/> if <paramref name="predicate"/> is true
		/// <para>Otherwise, will return <see cref="Jeebs.None{T}"/></para>
		/// </summary>
		/// <typeparam name="T">Option value type</typeparam>
		/// <param name="predicate">Predicate to evaluate</param>
		/// <param name="value">Function to return value</param>
		public static Option<T> WrapIf<T>(Func<bool> predicate, Func<T> value) =>
			predicate() switch
			{
				true =>
					Wrap(value()),

				false =>
					None<T>()
			};

		/// <summary>
		/// Catch any unhandled exceptions in the chain
		/// </summary>
		/// <typeparam name="T">Option value type</typeparam>
		/// <param name="chain">The chain to execute</param>
		/// <param name="handle">[Optional] Unhandled exception handler</param>
		public static Option<T> Catch<T>(Func<Option<T>> chain, Func<Exception, IExceptionMsg>? handle = null)
		{
			try
			{
				return chain();
			}
			catch (Exception e) when (handle != null)
			{
				return None<T>().AddReason(handle(e));
			}
			catch (Exception e)
			{
				return None<T>().AddReason(new Jm.Option.UnhandledExceptionMsg(e));
			}
		}

		/// <summary>
		/// Catch any unhandled exceptions in the asynchronous chain
		/// </summary>
		/// <typeparam name="T">Option value type</typeparam>
		/// <param name="chain">The asynchronous chain to execute</param>
		/// <param name="handle">[Optional] Unhandled exception handler</param>
		public static async Task<Option<T>> CatchAsync<T>(Func<Task<Option<T>>> chain, Func<Exception, IExceptionMsg>? handle = null)
		{
			try
			{
				return await chain();
			}
			catch (Exception e) when (handle != null)
			{
				return None<T>().AddReason(handle(e));
			}
			catch (Exception e)
			{
				return None<T>().AddReason(new Jm.Option.UnhandledExceptionMsg(e));
			}
		}
	}
}
