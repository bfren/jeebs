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
		public static async Task<Option<T>> AuditAsyncPrivate<T>(Task<Option<T>> @this, Func<Option<T>, Task> audit)
		{
			// Await option type
			var awaited = await @this;

			// Perform the audit
			try
			{
				await audit(awaited);
			}
			catch (Exception e)
			{
				Console.WriteLine("Audit Error: {0}", e);
			}

			// Return the original object
			return awaited;
		}

		/// <inheritdoc cref="AuditAsyncPrivate{T}(Task{Option{T}}, Func{Option{T}, Task})"/>
		public static Task<Option<T>> AuditAsync<T>(this Task<Option<T>> @this, Action<Option<T>> audit) =>
			AuditAsyncPrivate(@this, x => { audit(x); return Task.CompletedTask; });

		/// <inheritdoc cref="AuditAsyncPrivate{T}(Task{Option{T}}, Func{Option{T}, Task})"/>
		public static Task<Option<T>> AuditAsync<T>(this Task<Option<T>> @this, Func<Option<T>, Task> audit) =>
			AuditAsyncPrivate(@this, audit);
	}
}
