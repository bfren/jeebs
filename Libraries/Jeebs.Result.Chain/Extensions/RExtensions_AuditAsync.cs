using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Jm.AuditAsync;

namespace Jeebs
{
	/// <summary>
	/// Extension methods for <see cref="IR"/>: AuditAsync &amp; AuditSwitchAsync
	/// </summary>
	public static class RExtensions_AuditAsync
	{
		private static async Task<TResult> PrivateAuditSwitchAsync<TResult, TOk, TOkV, TError>(TResult result, Func<TOk, Task>? isOk, Func<TOkV, Task>? isOkV, Func<TError, Task>? isError)
			where TResult : IR
		{
			if (isOk == null && isOkV == null && isError == null)
			{
				return result;
			}

			Func<Task> audit = result switch
			{
				TOkV okV => () => isOkV?.Invoke(okV),
				TOk ok => () => isOk?.Invoke(ok),
				TError error => () => isError?.Invoke(error),
				_ => () => throw new Jx.Result.UnknownImplementationException()
			};

			try
			{
				await audit().ConfigureAwait(false);
			}
			catch (Exception ex) when (!(result.Messages is null))
			{
				result.AddMsg(new AuditSwitchAsyncExceptionMsg(ex));
			}

			return result;
		}

		/// <summary>
		/// Audit the current result state and return unmodified
		/// <para>Any exceptions will be caught and passed down the pipeline as a <see cref="AuditAsyncExceptionMsg"/> message</para>
		/// </summary>
		/// <typeparam name="TResult">Result type</typeparam>
		/// <param name="this">Result</param>
		/// <param name="audit">Audit action</param>
		public static async Task<TResult> AuditAsync<TResult>(this TResult @this, Func<TResult, Task> audit)
			where TResult : IR
		{
			try
			{
				await audit(@this).ConfigureAwait(false);
			}
			catch (Exception ex)
			{
				@this.AddMsg(new AuditAsyncExceptionMsg(ex));
			}

			return @this;
		}

		/// <summary>
		/// Audit the current result state and return unmodified
		/// <para>Any exceptions will be caught and passed down the pipeline as a <see cref="AuditSwitchAsyncExceptionMsg"/> message</para>
		/// </summary>
		/// <typeparam name="TValue">Result value type</typeparam>
		/// <param name="this">Result</param>
		/// <param name="isOk">[Optional] Action to run if the current result is <see cref="IOk{TValue}"/></param>
		/// <param name="isOkV">[Optional] Action to run if the current result is <see cref="IOkV{TValue}"/></param>
		/// <param name="isError">[Optional] Action to run if the current result is <see cref="IError{TValue}"/></param>
		public static Task<IR<TValue>> AuditSwitchAsync<TValue>(this IR<TValue> @this, Func<IOk<TValue>, Task>? isOk = null, Func<IOkV<TValue>, Task>? isOkV = null, Func<IError<TValue>, Task>? isError = null)
			=> PrivateAuditSwitchAsync(@this, isOk, isOkV, isError);

		/// <summary>
		/// Audit the current result state and return unmodified
		/// <para>Any exceptions will be caught and passed down the pipeline as a <see cref="AuditSwitchAsyncExceptionMsg"/> message</para>
		/// </summary>
		/// <typeparam name="TValue">Result value type</typeparam>
		/// <typeparam name="TState">Chain state type</typeparam>
		/// <param name="this">Result</param>
		/// <param name="isOk">[Optional] Action to run if the current result is <see cref="IOk{TValue, TState}"/></param>
		/// <param name="isOkV">[Optional] Action to run if the current result is <see cref="IOkV{TValue, TState}"/></param>
		/// <param name="isError">[Optional] Action to run if the current result is <see cref="IError{TValue, TState}"/></param>
		public static Task<IR<TValue, TState>> AuditSwitchAsync<TValue, TState>(this IR<TValue, TState> @this, Func<IOk<TValue, TState>, Task>? isOk = null, Func<IOkV<TValue, TState>, Task>? isOkV = null, Func<IError<TValue, TState>, Task>? isError = null)
			=> PrivateAuditSwitchAsync(@this, isOk, isOkV, isError);
	}
}
