using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Jeebs
{
	/// <summary>
	/// Extension methods for <see cref="IR"/>: Audit &amp; AuditSwitch
	/// </summary>
	public static class RExtensions_Audit
	{
		private static TResult PrivateAuditSwitch<TResult, TOk, TOkV, TError>(TResult @this, Action<TOk>? isOk, Action<TOkV>? isOkV, Action<TError>? isError)
			where TResult : IR
		{
			if (isOk == null && isOkV == null && isError == null)
			{
				return @this;
			}

			Action audit = @this switch
			{
				TOkV okV => () => isOkV?.Invoke(okV),
				TOk ok => () => isOk?.Invoke(ok),
				TError error => () => isError?.Invoke(error),
				_ => () => throw new Jx.Result.UnknownImplementationException()
			};

			try
			{
				audit();
			}
			catch (Exception ex) when (!(@this.Messages is null))
			{
				@this.AddMsg(new Jm.AuditExceptionMsg(ex));
			}

			return @this;
		}

		/// <summary>
		/// Audit the current result state and return unmodified
		/// <para>Any exceptions will be caught and passed down the pipeline as a <see cref="Jm.AuditExceptionMsg"/> message</para>
		/// </summary>
		/// <typeparam name="TResult">Result type</typeparam>
		/// <param name="this">Result</param>
		/// <param name="audit">Audit action</param>
		public static TResult Audit<TResult>(this TResult @this, Action<TResult> audit)
			where TResult : IR
		{
			try
			{
				audit(@this);
			}
			catch (Exception ex)
			{
				@this.AddMsg(new Jm.AuditExceptionMsg(ex));
			}

			return @this;
		}

		/// <summary>
		/// Audit the current result state and return unmodified
		/// <para>Any exceptions will be caught and passed down the pipeline as a <see cref="Jm.AuditExceptionMsg"/> message</para>
		/// </summary>
		/// <typeparam name="TValue">Result value type</typeparam>
		/// <param name="this">Result</param>
		/// <param name="isOk">[Optional] Action to run if the current result is <see cref="IOk{TValue}"/></param>
		/// <param name="isOkV">[Optional] Action to run if the current result is <see cref="IOkV{TValue}"/></param>
		/// <param name="isError">[Optional] Action to run if the current result is <see cref="IError{TValue}"/></param>
		public static IR<TValue> AuditSwitch<TValue>(this IR<TValue> @this, Action<IOk<TValue>>? isOk = null, Action<IOkV<TValue>>? isOkV = null, Action<IError<TValue>>? isError = null)
			=> PrivateAuditSwitch(@this, isOk, isOkV, isError);

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
		public static IR<TValue, TState> AuditSwitch<TValue, TState>(this IR<TValue, TState> @this, Action<IOk<TValue, TState>>? isOk = null, Action<IOkV<TValue, TState>>? isOkV = null, Action<IError<TValue, TState>>? isError = null)
			=> PrivateAuditSwitch(@this, isOk, isOkV, isError);
	}
}
