// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System;
using System.Threading.Tasks;

namespace Jeebs
{
	/// <summary>
	/// <see cref="Option{T}"/> Extensions: AuditSwitchAsync
	/// </summary>
	public static class OptionExtensions_AuditSwitchAsync
	{
		/// <summary>
		/// Audit the current Option state and return unmodified
		/// Errors will not be returned as they affect the state of the object, but will be written to the console
		/// </summary>
		/// <typeparam name="T">Option value type</typeparam>
		/// <param name="this">Option value (awaitable)</param>
		/// <param name="some">[Optional] Action to run if the current Option is <see cref="Some{T}"/></param>
		/// <param name="none">[Optional] Action to run if the current Option is <see cref="None{T}"/></param>
		private static async Task<Option<T>> AuditSwitchAsyncPrivate<T>(
			Task<Option<T>> @this,
			Func<T, Task>? some = null,
			Func<IMsg?, Task>? none = null
		)
		{
			// Await option
			var awaited = await @this;

			// Do nothing if the user gave us nothing to do!
			if (some == null && none == null)
			{
				return awaited;
			}

			// Work out which audit function to use
			Func<Task> audit = awaited switch
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
			return awaited;
		}

		/// <inheritdoc cref="AuditSwitchAsyncPrivate{T}(Task{Option{T}}, Func{T, Task}?, Func{IMsg?, Task}?)"/>
		public static Task<Option<T>> AuditSwitchAsync<T>(
			this Task<Option<T>> @this,
			Func<T, Task>? some = null,
			Action<IMsg?>? none = null
		) =>
			AuditSwitchAsyncPrivate(
				@this,
				some: some,
				none: x => { none?.Invoke(x); return Task.CompletedTask; }
			);

		/// <inheritdoc cref="AuditSwitchAsyncPrivate{T}(Task{Option{T}}, Func{T, Task}?, Func{IMsg?, Task}?)"/>
		public static Task<Option<T>> AuditSwitchAsync<T>(
			this Task<Option<T>> @this,
			Action<T>? some = null,
			Func<IMsg?, Task>? none = null
		) =>
			AuditSwitchAsyncPrivate(
				@this,
				some: x => { some?.Invoke(x); return Task.CompletedTask; },
				none: none
			);

		/// <inheritdoc cref="AuditSwitchAsyncPrivate{T}(Task{Option{T}}, Func{T, Task}?, Func{IMsg?, Task}?)"/>
		public static Task<Option<T>> AuditSwitchAsync<T>(
			this Task<Option<T>> @this,
			Func<T, Task>? some = null,
			Func<IMsg?, Task>? none = null
		) =>
			AuditSwitchAsyncPrivate(
				@this,
				some: some,
				none: none
			);
	}
}
