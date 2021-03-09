// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System;
using System.Threading.Tasks;

namespace Jeebs
{
	/// <summary>
	/// <see cref="Option{T}"/> Extensions: AuditAsync
	/// </summary>
	public static class OptionExtensions_AuditAsync
	{
		/// <summary>
		/// Audit the current Option state and return unmodified
		/// Errors will not be returned as they affect the state of the object, but will be written to the console
		/// </summary>
		/// <typeparam name="T">Option value type</typeparam>
		/// <param name="this">Option value (awaitable)</param>
		/// <param name="audit">Audit function</param>
		public static async Task<Option<T>> AuditAsync<T>(this Task<Option<T>> @this, Action<Option<T>> audit) =>
			OptionExtensions_Audit.Audit(await @this, audit);

		/// <summary>
		/// Audit the current Option state and return unmodified
		/// Errors will not be returned as they affect the state of the object, but will be written to the console
		/// </summary>
		/// <typeparam name="T">Option value type</typeparam>
		/// <param name="this">Option value (awaitable)</param>
		/// <param name="audit">Audit function</param>
		public static async Task<Option<T>> AuditAsync<T>(this Option<T> @this, Func<Option<T>, Task> audit)
		{
			// Perform the audit
			try
			{
				await audit(@this);
			}
			catch (Exception e)
			{
				Console.WriteLine("Audit Error: {0}", e);
			}

			// Return the original object
			return @this;
		}

		/// <inheritdoc cref="AuditAsync{T}(Option{T}, Func{Option{T}, Task})"/>
		public static async Task<Option<T>> AuditAsync<T>(this Task<Option<T>> @this, Func<Option<T>, Task> audit) =>
			await AuditAsync(await @this, audit);

		/// <summary>
		/// Audit the current Option state and return unmodified
		/// Errors will not be returned as they affect the state of the object, but will be written to the console
		/// </summary>
		/// <typeparam name="T">Option value type</typeparam>
		/// <param name="this">Option value (awaitable)</param>
		/// <param name="some">[Optional] Action to run if the current Option is <see cref="Some{T}"/></param>
		/// <param name="none">[Optional] Action to run if the current Option is <see cref="None{T}"/></param>
		public static async Task<Option<T>> AuditSwitch<T>(this Task<Option<T>> @this, Action<T>? some = null, Action<IMsg?>? none = null) =>
			OptionExtensions_Audit.AuditSwitch(await @this, some, none);

		/// <summary>
		/// Audit the current Option state and return unmodified
		/// Errors will not be returned as they affect the state of the object, but will be written to the console
		/// </summary>
		/// <typeparam name="T">Option value type</typeparam>
		/// <param name="this">Option value (awaitable)</param>
		/// <param name="some">[Optional] Action to run if the current Option is <see cref="Some{T}"/></param>
		/// <param name="none">[Optional] Action to run if the current Option is <see cref="None{T}"/></param>
		public static async Task<Option<T>> AuditSwitchAsync<T>(this Option<T> @this, Func<T, Task>? some = null, Func<IMsg?, Task>? none = null)
		{
			// Do nothing if the user gave us nothing to do!
			if (some == null && none == null)
			{
				return @this;
			}

			// Work out which audit function to use
			Func<Task> audit = @this switch
			{
				Some<T> x =>
					() => some?.Invoke(x.Value) ?? Task.CompletedTask,

				None<T> x =>
					() => none?.Invoke(x.Reason) ?? Task.CompletedTask,

				_ =>
					() => throw new Jx.Option.UnknownOptionException()
			};

			// Perform the audit
			try
			{
				await audit();
			}
			catch (Exception e)
			{
				Console.WriteLine("Audit Error: {0}", e);
			}

			// Return the original object
			return @this;
		}

		/// <inheritdoc cref="AuditSwitchAsync{T}(Option{T}, Func{T, Task}?, Func{IMsg?, Task}?)"/>
		public static async Task<Option<T>> AuditSwitchAsync<T>(this Task<Option<T>> @this, Func<T, Task>? some = null, Func<IMsg?, Task>? none = null) =>
			await AuditSwitchAsync(await @this, some, none);
	}
}
