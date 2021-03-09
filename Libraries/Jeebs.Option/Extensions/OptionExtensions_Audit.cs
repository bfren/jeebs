// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System;

namespace Jeebs
{
	/// <summary>
	/// <see cref="Option{T}"/> Extensions: Audit
	/// </summary>
	public static class OptionExtensions_Audit
	{
		/// <summary>
		/// Audit the current Option state and return unmodified
		/// Errors will not be returned as they affect the state of the object, but will be written to the console
		/// </summary>
		/// <typeparam name="T">Option value type</typeparam>
		/// <param name="this">Option value (awaitable)</param>
		/// <param name="audit">Audit function</param>
		public static Option<T> Audit<T>(this Option<T> @this, Action<Option<T>> audit)
		{
			// Perform the audit
			try
			{
				audit(@this);
			}
			catch (Exception e)
			{
				Console.WriteLine("Audit Error: {0}", e);
			}

			// Return the original object
			return @this;
		}

		/// <summary>
		/// Audit the current Option state and return unmodified
		/// Errors will not be returned as they affect the state of the object, but will be written to the console
		/// </summary>
		/// <typeparam name="T">Option value type</typeparam>
		/// <param name="this">Option value (awaitable)</param>
		/// <param name="some">[Optional] Action to run if the current Option is <see cref="Some{T}"/></param>
		/// <param name="none">[Optional] Action to run if the current Option is <see cref="None{T}"/></param>
		public static Option<T> AuditSwitch<T>(this Option<T> @this, Action<T>? some = null, Action<IMsg?>? none = null)
		{
			// Do nothing if the user gave us nothing to do!
			if (some == null && none == null)
			{
				return @this;
			}

			// Work out which audit function to use
			Action audit = @this switch
			{
				Some<T> x =>
					() => some?.Invoke(x.Value),

				None<T> x =>
					() => none?.Invoke(x.Reason),

				_ =>
					() => throw new Jx.Option.UnknownOptionException()
			};

			// Perform the audit
			try
			{
				audit();
			}
			catch (Exception e)
			{
				Console.WriteLine("Audit Error: {0}", e);
			}

			// Return the original object
			return @this;
		}
	}
}
