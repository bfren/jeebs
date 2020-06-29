using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Jeebs
{
	public static partial class RExtensions
	{
		#region Start Async

		/// <summary>
		/// Audit the current result state asynchronously and return unmodified
		/// <para>Any exceptions will be caught and passed down the pipeline as a <see cref="Jm.AuditAsyncException"/> message</para>
		/// </summary>
		/// <typeparam name="TResult">Result value type</typeparam>
		/// <param name="this">Current result task (will be awaited before executing <paramref name="audit"/>)</param>
		/// <param name="audit">Audit action</param>
		public static async Task<IR<TResult>> AuditAsync<TResult>(this Task<IR<TResult>> @this, Func<IR<TResult>, Task> audit)
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
		/// <param name="this">Current result task (will be awaited before executing appropriate action)</param>
		/// <param name="isOk">[Optional] Action to run if the current result is <see cref="IOk{TResult}"/></param>
		/// <param name="isOkV">[Optional] Action to run if the current result is <see cref="IOkV{TResult}"/></param>
		/// <param name="isError">[Optional] Action to run if the current result is <see cref="IError{TResult}"/></param>
		/// <param name="isUnknown">[Optional] Action to run if the current result is unknown</param>
		public static async Task<IR<TResult>> AuditSwitchAsync<TResult>(
			this Task<IR<TResult>> @this,
			Func<IOk<TResult>, Task>? isOk = null,
			Func<IOkV<TResult>, Task>? isOkV = null,
			Func<IError<TResult>, Task>? isError = null,
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
				IError<TResult> error => () => isError == null ? Task.CompletedTask : isError(error),
				IOkV<TResult> okV => () => isOkV == null ? Task.CompletedTask : isOkV(okV),
				IOk<TResult> ok => () => isOk == null ? Task.CompletedTask : isOk(ok),
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
		/// <param name="this">Current result</param>
		/// <param name="audit">Audit action</param>
		public static Task<IR<TResult>> AuditAsync<TResult>(this IR<TResult> @this, Func<IR<TResult>, Task> audit) => AuditAsync(Task.Run(() => @this), audit);

		/// <summary>
		/// Audit the current result state asynchronously and return unmodified
		/// <para>Any exceptions will be caught and passed down the pipeline as a <see cref="Jm.AuditAsyncException"/> message</para>
		/// </summary>
		/// <typeparam name="TResult">Result value type</typeparam>
		/// <param name="this">Current result</param>
		/// <param name="isOk">[Optional] Action to run if the current result is <see cref="IOk{TResult}"/></param>
		/// <param name="isOkV">[Optional] Action to run if the current result is <see cref="IOkV{TResult}"/></param>
		/// <param name="isError">[Optional] Action to run if the current result is <see cref="IError{TResult}"/></param>
		/// <param name="isUnknown">[Optional] Action to run if the current result is unknown</param>
		public static Task<IR<TResult>> AuditSwitchAsync<TResult>(
			this IR<TResult> @this,
			Func<IOk<TResult>, Task>? isOk = null,
			Func<IOkV<TResult>, Task>? isOkV = null,
			Func<IError<TResult>, Task>? isError = null,
			Func<Task>? isUnknown = null
		) => AuditSwitchAsync(Task.Run(() => @this), isOk, isOkV, isError, isUnknown);

		#endregion
	}
}
