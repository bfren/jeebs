using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Jeebs
{
	/// <summary>
	/// Extension methods for IR interface: Audit & AuditSwitch
	/// </summary>
	public static class RExtensions_Audit
	{
		/// <summary>
		/// Audit the current result state and return unmodified
		/// <para>Any exceptions will be caught and passed down the pipeline as a <see cref="Jm.AuditExceptionMsg"/> message</para>
		/// </summary>
		/// <typeparam name="TResult">Result type</typeparam>
		/// <param name="result">Result</param>
		/// <param name="audit">Audit action</param>
		public static TResult Audit<TResult>(this TResult result, Action<TResult> audit)
			where TResult : IR
		{
			try
			{
				audit(result);
			}
			catch (Exception ex)
			{
				result.Messages.Add(new Jm.AuditExceptionMsg(ex));
			}

			return result;
		}

		/// <summary>
		/// Audit the current result state and return unmodified
		/// <para>Any exceptions will be caught and passed down the pipeline as a <see cref="Jm.AuditExceptionMsg"/> message</para>
		/// </summary>
		/// <typeparam name="TResult">Result type</typeparam>
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
				result.Messages.Add(new Jm.AuditAsyncExceptionMsg(ex));
			}

			return result;
		}

		/// <summary>
		/// Audit the current result state and return unmodified
		/// <para>Any exceptions will be caught and passed down the pipeline as a <see cref="Jm.AuditExceptionMsg"/> message</para>
		/// </summary>
		/// <typeparam name="TResult">Result type</typeparam>
		/// <typeparam name="TValue">Result value type</typeparam>
		/// <param name="isOk">[Optional] Action to run if the current result is <see cref="IOk{TValue}"/></param>
		/// <param name="isOkV">[Optional] Action to run if the current result is <see cref="IOkV{TValue}"/></param>
		/// <param name="isError">[Optional] Action to run if the current result is <see cref="IError{TValue}"/></param>
		public static TResult AuditSwitch<TResult, TValue>(this TResult result, Action<IOk<TValue>>? isOk = null, Action<IOkV<TValue>>? isOkV = null, Action<IError<TValue>>? isError = null)
			where TResult : IR<TValue> => Switch(result, isOk, isOkV, isError);

		/// <summary>
		/// Audit the current result state and return unmodified
		/// <para>Any exceptions will be caught and passed down the pipeline as a <see cref="Jm.AuditExceptionMsg"/> message</para>
		/// </summary>
		/// <typeparam name="TResult">Result type</typeparam>
		/// <typeparam name="TValue">Result value type</typeparam>
		/// <param name="result">Result task - will be awaited before executing audit functions</param>
		/// <param name="isOk">[Optional] Action to run if the current result is <see cref="IOk{TValue}"/></param>
		/// <param name="isOkV">[Optional] Action to run if the current result is <see cref="IOkV{TValue}"/></param>
		/// <param name="isError">[Optional] Action to run if the current result is <see cref="IError{TValue}"/></param>
		public static async Task<TResult> AuditSwitchAsync<TResult, TValue>(this Task<TResult> result, Func<IOk<TValue>, Task>? isOk = null, Func<IOkV<TValue>, Task>? isOkV = null, Func<IError<TValue>, Task>? isError = null)
			where TResult : IR<TValue> => await SwitchAsync(result, isOk, isOkV, isError);

		/// <summary>
		/// Audit the current result state and return unmodified
		/// <para>Any exceptions will be caught and passed down the pipeline as a <see cref="Jm.AuditExceptionMsg"/> message</para>
		/// </summary>
		/// <typeparam name="TResult">Result type</typeparam>
		/// <typeparam name="TValue">Result value type</typeparam>
		/// <typeparam name="TState">Chain state type</typeparam>
		/// <param name="isOk">[Optional] Action to run if the current result is <see cref="IOk{TValue, TState}"/></param>
		/// <param name="isOkV">[Optional] Action to run if the current result is <see cref="IOkV{TValue, TState}"/></param>
		/// <param name="isError">[Optional] Action to run if the current result is <see cref="IError{TValue, TState}"/></param>
		public static TResult AuditSwitch<TResult, TValue, TState>(this TResult result, Action<IOk<TValue, TState>>? isOk = null, Action<IOkV<TValue, TState>>? isOkV = null, Action<IError<TValue, TState>>? isError = null)
			where TResult : IR<TValue, TState> => Switch(result, isOk, isOkV, isError);

		/// <summary>
		/// Audit the current result state and return unmodified
		/// <para>Any exceptions will be caught and passed down the pipeline as a <see cref="Jm.AuditExceptionMsg"/> message</para>
		/// </summary>
		/// <typeparam name="TResult">Result type</typeparam>
		/// <typeparam name="TValue">Result value type</typeparam>
		/// <typeparam name="TState">Chain state type</typeparam>
		/// <param name="result">Result task - will be awaited before executing audit functions</param>
		/// <param name="isOk">[Optional] Action to run if the current result is <see cref="IOk{TValue, TState}"/></param>
		/// <param name="isOkV">[Optional] Action to run if the current result is <see cref="IOkV{TValue, TState}"/></param>
		/// <param name="isError">[Optional] Action to run if the current result is <see cref="IError{TValue, TState}"/></param>
		public static async Task<TResult> AuditSwitchAsync<TResult, TValue, TState>(this Task<TResult> result, Func<IOk<TValue, TState>, Task>? isOk = null, Func<IOkV<TValue, TState>, Task>? isOkV = null, Func<IError<TValue, TState>, Task>? isError = null)
			where TResult : IR<TValue, TState> => await SwitchAsync(result, isOk, isOkV, isError);

		private static TResult Switch<TResult, TOk, TOkV, TError>(TResult result, Action<TOk>? isOk, Action<TOkV>? isOkV, Action<TError>? isError)
			where TResult : IR
		{
			if (isOk == null && isOkV == null && isError == null)
			{
				return result;
			}

			Action audit = result switch
			{
				TOkV okV => () => isOkV?.Invoke(okV),
				TOk ok => () => isOk?.Invoke(ok),
				TError error => () => isError?.Invoke(error),
				_ => () => throw new InvalidOperationException($"Unknown IR<> subtype: '{result.GetType()}'.")
			};

			try
			{
				audit();
			}
			catch (Exception ex)
			{
				result.Messages.Add(new Jm.AuditExceptionMsg(ex));
			}

			return result;
		}

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
				result.Messages.Add(new Jm.AuditAsyncExceptionMsg(ex));
			}

			return result;
		}
	}
}
