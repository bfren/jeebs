using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Jeebs
{
	/// <summary>
	/// Extension methods for IR interface: AuditAsync & AuditSwitchAsync
	/// </summary>
	public static class RExtensions_AuditAsync
	{
		/// <summary>
		/// Audit the current result state and return unmodified
		/// <para>Any exceptions will be caught and passed down the pipeline as a <see cref="Jm.AuditExceptionMsg"/> message</para>
		/// </summary>
		/// <typeparam name="TResult">Result value type</typeparam>
		/// <param name="this">Result task - will be awaited before executing <paramref name="audit"/></param>
		/// <param name="audit">Audit action</param>
		public static async Task<TResult> AuditAsync<TResult>(this Task<TResult> @this, Func<TResult, Task> audit)
			where TResult : IR
		{
			var result = await @this.ConfigureAwait(false);

			try
			{
				await audit(result).ConfigureAwait(false);
			}
			catch (Exception ex)
			{
				result.AddMsg(new Jm.AuditAsyncExceptionMsg(ex));
			}

			return result;
		}

		/// <summary>
		/// Audit the current result state and return unmodified
		/// <para>Any exceptions will be caught and passed down the pipeline as a <see cref="Jm.AuditExceptionMsg"/> message</para>
		/// </summary>
		/// <typeparam name="TValue">Result value type</typeparam>
		/// <param name="this">Result</param>
		/// <param name="audit">Audit action</param>
		public static Task<IR<TValue>> AuditAsync<TValue>(this IR<TValue> @this, Func<IR<TValue>, Task> audit)
			=> AuditAsync(Task.FromResult(@this), audit);

		/// <summary>
		/// Audit the current result state and return unmodified
		/// <para>Any exceptions will be caught and passed down the pipeline as a <see cref="Jm.AuditExceptionMsg"/> message</para>
		/// </summary>
		/// <typeparam name="TValue">Result value type</typeparam>
		/// <typeparam name="TState">Result state type</typeparam>
		/// <param name="this">Result</param>
		/// <param name="audit">Audit action</param>
		public static Task<IR<TValue, TState>> AuditAsync<TValue, TState>(this IR<TValue, TState> @this, Func<IR<TValue, TState>, Task> audit)
			=> AuditAsync(Task.FromResult(@this), audit);

		/// <summary>
		/// Audit the current result state and return unmodified
		/// <para>Any exceptions will be caught and passed down the pipeline as a <see cref="Jm.AuditExceptionMsg"/> message</para>
		/// </summary>
		/// <typeparam name="TValue">Result value type</typeparam>
		/// <param name="this">Result task - will be awaited before executing audit functions</param>
		/// <param name="isOk">[Optional] Action to run if the current result is <see cref="IOk{TValue}"/></param>
		/// <param name="isOkV">[Optional] Action to run if the current result is <see cref="IOkV{TValue}"/></param>
		/// <param name="isError">[Optional] Action to run if the current result is <see cref="IError{TValue}"/></param>
		public static async Task<IR<TValue>> AuditSwitchAsync<TValue>(this Task<IR<TValue>> @this, Func<IOk<TValue>, Task>? isOk = null, Func<IOkV<TValue>, Task>? isOkV = null, Func<IError<TValue>, Task>? isError = null)
			=> await SwitchAsync(@this, isOk, isOkV, isError);

		/// <summary>
		/// Audit the current result state and return unmodified
		/// <para>Any exceptions will be caught and passed down the pipeline as a <see cref="Jm.AuditExceptionMsg"/> message</para>
		/// </summary>
		/// <typeparam name="TValue">Result value type</typeparam>
		/// <param name="this">Result</param>
		/// <param name="isOk">[Optional] Action to run if the current result is <see cref="IOk{TValue}"/></param>
		/// <param name="isOkV">[Optional] Action to run if the current result is <see cref="IOkV{TValue}"/></param>
		/// <param name="isError">[Optional] Action to run if the current result is <see cref="IError{TValue}"/></param>
		public static Task<IR<TValue>> AuditSwitchAsync<TValue>(this IR<TValue> @this, Func<IOk<TValue>, Task>? isOk = null, Func<IOkV<TValue>, Task>? isOkV = null, Func<IError<TValue>, Task>? isError = null)
			=> SwitchAsync(Task.FromResult(@this), isOk, isOkV, isError);

		/// <summary>
		/// Audit the current result state and return unmodified
		/// <para>Any exceptions will be caught and passed down the pipeline as a <see cref="Jm.AuditExceptionMsg"/> message</para>
		/// </summary>
		/// <typeparam name="TValue">Result value type</typeparam>
		/// <typeparam name="TState">Chain state type</typeparam>
		/// <param name="this">Result task - will be awaited before executing audit functions</param>
		/// <param name="isOk">[Optional] Action to run if the current result is <see cref="IOk{TValue, TState}"/></param>
		/// <param name="isOkV">[Optional] Action to run if the current result is <see cref="IOkV{TValue, TState}"/></param>
		/// <param name="isError">[Optional] Action to run if the current result is <see cref="IError{TValue, TState}"/></param>
		public static async Task<IR<TValue, TState>> AuditSwitchAsync<TValue, TState>(this Task<IR<TValue, TState>> @this, Func<IOk<TValue, TState>, Task>? isOk = null, Func<IOkV<TValue, TState>, Task>? isOkV = null, Func<IError<TValue, TState>, Task>? isError = null)
			=> await SwitchAsync(@this, isOk, isOkV, isError);

		/// <summary>
		/// Audit the current result state and return unmodified
		/// <para>Any exceptions will be caught and passed down the pipeline as a <see cref="Jm.AuditExceptionMsg"/> message</para>
		/// </summary>
		/// <typeparam name="TValue">Result value type</typeparam>
		/// <typeparam name="TState">Chain state type</typeparam>
		/// <param name="this">Result</param>
		/// <param name="isOk">[Optional] Action to run if the current result is <see cref="IOk{TValue, TState}"/></param>
		/// <param name="isOkV">[Optional] Action to run if the current result is <see cref="IOkV{TValue, TState}"/></param>
		/// <param name="isError">[Optional] Action to run if the current result is <see cref="IError{TValue, TState}"/></param>
		public static Task<IR<TValue, TState>> AuditSwitchAsync<TValue, TState>(this IR<TValue, TState> @this, Func<IOk<TValue, TState>, Task>? isOk = null, Func<IOkV<TValue, TState>, Task>? isOkV = null, Func<IError<TValue, TState>, Task>? isError = null)
			=> SwitchAsync(Task.FromResult(@this), isOk, isOkV, isError);

		private static async Task<TResult> SwitchAsync<TResult, TOk, TOkV, TError>(Task<TResult> @this, Func<TOk, Task>? isOk, Func<TOkV, Task>? isOkV, Func<TError, Task>? isError)
			where TResult : IR
		{
			var result = await @this.ConfigureAwait(false);

			if (isOk == null && isOkV == null && isError == null)
			{
				return result;
			}

			Func<Task> audit = result switch
			{
				TOkV okV => () => isOkV?.Invoke(okV),
				TOk ok => () => isOk?.Invoke(ok),
				TError error => () => isError?.Invoke(error),
				_ => () => throw new InvalidOperationException($"Unknown IR<> subtype: '{result.GetType()}'.")
			};

			try
			{
				await audit().ConfigureAwait(false);
			}
			catch (Exception ex)
			{
				result.AddMsg(new Jm.AuditAsyncExceptionMsg(ex));
			}

			return result;
		}
	}
}
