using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Jeebs
{
	public static partial class RExtensions_WithState
	{
		#region Start Async

		/// <summary>
		/// Audit the current result state asynchronously and return unmodified
		/// <para>Any exceptions will be caught and passed down the pipeline as a <see cref="Jm.AuditAsyncException"/> message</para>
		/// </summary>
		/// <typeparam name="TResult">Result value type</typeparam>
		/// <typeparam name="TState">State value type</typeparam>
		/// <param name="this">Current result task (will be awaited before executing <paramref name="audit"/>)</param>
		/// <param name="audit">Audit action</param>
		public static async Task<IR<TResult, TState>> AuditAsync<TResult, TState>(this Task<IR<TResult, TState>> @this, Func<IR<TResult, TState>, Task> audit)
		{
			var result = await @this.ConfigureAwait(false);

			try
			{
				await audit(result).ConfigureAwait(false);
			}
			catch (Exception ex)
			{
				result.Messages.Add(new Jm.AuditAsyncException(ex));
			}

			return result;
		}

		/// <summary>
		/// Audit the current result state asynchronously and return unmodified
		/// <para>Any exceptions will be caught and passed down the pipeline as a <see cref="Jm.AuditAsyncException"/> message</para>
		/// </summary>
		/// <typeparam name="TResult">Result value type</typeparam>
		/// <typeparam name="TState">State value type</typeparam>
		/// <param name="this">Current result task (will be awaited before executing appropriate action)</param>
		/// <param name="isOk">[Optional] Action to run if the current result is <see cref="IOk{TResult, TState}"/></param>
		/// <param name="isOkV">[Optional] Action to run if the current result is <see cref="IOkV{TResult, TState}"/></param>
		/// <param name="isError">[Optional] Action to run if the current result is <see cref="IError{TResult, TState}"/></param>
		/// <param name="isUnknown">[Optional] Action to run if the current result is unknown</param>
		public static async Task<IR<TResult, TState>> AuditSwitchAsync<TResult, TState>(
			this Task<IR<TResult, TState>> @this,
			Func<IOk<TResult, TState>, Task>? isOk = null,
			Func<IOkV<TResult, TState>, Task>? isOkV = null,
			Func<IError<TResult, TState>, Task>? isError = null,
			Func<Task>? isUnknown = null
		)
		{
			var result = await @this.ConfigureAwait(false);

			if (isOk == null && isOkV == null && isError == null && isUnknown == null)
			{
				return result;
			}

			Func<Task> audit = result switch
			{
				IError<TResult, TState> error => () => isError == null ? Task.CompletedTask : isError(error),
				IOkV<TResult, TState> okV => () => isOkV == null ? Task.CompletedTask : isOkV(okV),
				IOk<TResult, TState> ok => () => isOk == null ? Task.CompletedTask : isOk(ok),
				_ => isUnknown ?? new Func<Task>(() => throw new InvalidOperationException($"Unknown R<> subtype: '{result.GetType()}'."))
			};

			try
			{
				await audit().ConfigureAwait(false);
			}
			catch (Exception ex)
			{
				result.Messages.Add(new Jm.AuditAsyncException(ex));
			}

			return result;
		}

		#endregion

		#region Start Sync

		/// <summary>
		/// Audit the current result state asynchronously and return unmodified
		/// <para>Any exceptions will be caught and passed down the pipeline as a <see cref="Jm.AuditAsyncException"/> message</para>
		/// </summary>
		/// <typeparam name="TResult">Result value type</typeparam>
		/// <typeparam name="TState">State value type</typeparam>
		/// <param name="this">Current result</param>
		/// <param name="audit">Audit action</param>
		public static Task<IR<TResult, TState>> AuditAsync<TResult, TState>(this IR<TResult, TState> @this, Func<IR<TResult, TState>, Task> audit) => AuditAsync(Task.Run(() => @this), audit);

		/// <summary>
		/// Audit the current result state asynchronously and return unmodified
		/// <para>Any exceptions will be caught and passed down the pipeline as a <see cref="Jm.AuditAsyncException"/> message</para>
		/// </summary>
		/// <typeparam name="TResult">Result value type</typeparam>
		/// <typeparam name="TState">State value type</typeparam>
		/// <param name="this">Current result</param>
		/// <param name="isOk">[Optional] Action to run if the current result is <see cref="IOk{TResult, TState}"/></param>
		/// <param name="isOkV">[Optional] Action to run if the current result is <see cref="IOkV{TResult, TState}"/></param>
		/// <param name="isError">[Optional] Action to run if the current result is <see cref="IError{TResult, TState}"/></param>
		/// <param name="isUnknown">[Optional] Action to run if the current result is unknown</param>
		public static Task<IR<TResult, TState>> AuditSwitchAsync<TResult, TState>(
			this IR<TResult, TState> @this,
			Func<IOk<TResult, TState>, Task>? isOk = null,
			Func<IOkV<TResult, TState>, Task>? isOkV = null,
			Func<IError<TResult, TState>, Task>? isError = null,
			Func<Task>? isUnknown = null
		) => AuditSwitchAsync(Task.Run(() => @this), isOk, isOkV, isError, isUnknown);

		#endregion
	}
}
