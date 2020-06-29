using System;
using System.Collections.Generic;
using System.Text;

namespace Jeebs
{
	/// <summary>
	/// Represents a result with state.
	/// </summary>
	/// <typeparam name="TResult">Result type</typeparam>
	/// <typeparam name="TState">State type</typeparam>
	public interface IR<TResult, TState> : IR
	{
		/// <summary>
		/// State value.
		/// </summary>
		TState State { get; }

		#region Link & LinkMap

		/// <summary>
		/// Execute the next link in the chain
		/// <para>Action <paramref name="a"/> receives no input and returns <see cref="IOk{TResult, TState}"/> as output</para>
		/// <para>Any exceptions will be caught and passed down the pipeline as a <see cref="Jm.Exception"/> message</para>
		/// </summary>
		/// <param name="a">The action will be executed if the current object is an <see cref="IOk{TResult, TState}"/> result</param>
		IR<TResult, TState> Link(Action a);

		/// <summary>
		/// Execute the next link in the chain
		/// <para>Action <paramref name="a"/> receives the current object as an input and returns <see cref="IOk{TResult, TState}"/> as output</para>
		/// <para>Any exceptions will be caught and passed down the pipeline as a <see cref="Jm.Exception"/> message</para>
		/// </summary>
		/// <param name="a">The action will be executed if the current object is an <see cref="IOk{TResult, TState}"/> result</param>
		IR<TResult, TState> Link(Action<IOk<TResult, TState>> a);

		/// <summary>
		/// Execute the next link in the chain
		/// <para>Action <paramref name="a"/> receives the current object as an input and returns <see cref="IOkV{TResult, TState}"/> as output</para>
		/// <para>Any exceptions will be caught and passed down the pipeline as a <see cref="Jm.Exception"/> message</para>
		/// </summary>
		/// <param name="a">The action will be executed if the current object is an <see cref="IOkV{TResult, TState}"/> result</param>
		IR<TResult, TState> Link(Action<IOkV<TResult, TState>> a);

		/// <summary>
		/// Execute the next link in the chain and map to a new result type
		/// <para>Func <paramref name="f"/> receives no input and returns a value of type <typeparamref name="TNext"/>, which is then wrapped in an <see cref="IOkV{TNext, TState}"/> object</para>
		/// <para>Any exceptions will be caught and passed down the pipeline as a <see cref="Jm.Exception"/> message</para>
		/// </summary>
		/// <typeparam name="TNext">Next Result value type</typeparam>
		/// <param name="f">The function will be executed if the current object is an <see cref="IOk{TResult, TState"/> result</param>
		IR<TNext, TState> LinkMap<TNext>(Func<TNext> f);

		/// <summary>
		/// Execute the next link in the chain and map to a new result type
		/// <para>Func <paramref name="f"/> receives the current object as an input and returns a new result of type <see cref="IR{TNext, TState}"/></para>
		/// <para>Any exceptions will be caught and passed down the pipeline as a <see cref="Jm.Exception"/> message</para>
		/// </summary>
		/// <typeparam name="TNext">Next Result value type</typeparam>
		/// <param name="f">The function will be executed if the current object is an <see cref="IOk{TResult, TState}"/> result</param>
		IR<TNext, TState> LinkMap<TNext>(Func<IOk<TResult, TState>, IR<TNext, TState>> f);

		/// <summary>
		/// Execute the next link in the chain and map to a new result type
		/// <para>Func <paramref name="f"/> receives the current object as an input and returns a new result of type <see cref="IR{TNext, TState}"/></para>
		/// <para>Any exceptions will be caught and passed down the pipeline as a <see cref="Jm.Exception"/> message</para>
		/// </summary>
		/// <typeparam name="TNext">Next Result value type</typeparam>
		/// <param name="f">The function will be executed if the current object is an <see cref="IOkV{TResult, TState}"/> result</param>
		IR<TNext, TState> LinkMap<TNext>(Func<IOkV<TResult, TState>, IR<TNext, TState>> f);

		#endregion

		#region Ok: Keep Result Type

		/// <summary>
		/// Return Ok result with optional messages
		/// </summary>
		/// <param name="messages">IMessage array</param>
		IOk<TResult, TState> Ok(params IMessage[] messages);

		/// <summary>
		/// Return Ok result with a single message of type <typeparamref name="TMessage"/>
		/// </summary>
		/// <typeparam name="TMessage">IMessage type</typeparam>
		IOk<TResult, TState> Ok<TMessage>() where TMessage : IMessage, new();

		/// <summary>
		/// Return Ok result with a single message of type <typeparamref name="TMessage"/>
		/// </summary>
		/// <typeparam name="TMessage">IMessage type</typeparam>
		IOk<TResult, TState> Ok<TMessage>(TMessage message) where TMessage : IMessage;

		#endregion

		#region Ok: Change Result Type

		/// <summary>
		/// Return Ok result with optional messages
		/// </summary>
		/// <typeparam name="TNext">Next Result value type</typeparam>
		/// <param name="messages">IMessage array</param>
		IOk<TNext, TState> OkNew<TNext>(params IMessage[] messages);

		/// <summary>
		/// Return Ok result with a single message of type <typeparamref name="TMessage"/>
		/// </summary>
		/// <typeparam name="TNext">Next Result value type</typeparam>
		/// <typeparam name="TMessage">IMessage type</typeparam>
		IOk<TNext, TState> OkNew<TNext, TMessage>() where TMessage : IMessage, new();

		/// <summary>
		/// Return Ok result with a single message of type <typeparamref name="TMessage"/>
		/// </summary>
		/// <typeparam name="TNext">Next Result value type</typeparam>
		/// <typeparam name="TMessage">IMessage type</typeparam>
		/// <param name="message">Message value</param>
		IOk<TNext, TState> OkNew<TNext, TMessage>(TMessage message) where TMessage : IMessage, new();

		#endregion

		#region Ok: Change Result Type and Add Value

		/// <summary>
		/// Return Ok result with optional messages
		/// </summary>
		/// <typeparam name="TNext">Next Result value type</typeparam>
		/// <param name="value">Ok result value</param>
		/// <param name="messages">IMessage array</param>
		IOkV<TNext, TState> OkV<TNext>(TNext value, params IMessage[] messages);

		/// <summary>
		/// Return Ok result with a single message of type <typeparamref name="TMessage"/>
		/// </summary>
		/// <typeparam name="TNext">Next Result value type</typeparam>
		/// <typeparam name="TMessage">IMessage type</typeparam>
		/// <param name="value">Ok result value</param>
		IOkV<TNext, TState> OkV<TNext, TMessage>(TNext value) where TMessage : IMessage, new();

		/// <summary>
		/// Return Ok result with a single message of type <typeparamref name="TMessage"/>
		/// </summary>
		/// <typeparam name="TNext">Next Result value type</typeparam>
		/// <typeparam name="TMessage">IMessage type</typeparam>
		/// <param name="value">Ok result value</param>
		/// <param name="message">Message value</param>
		IOkV<TNext, TState> OkV<TNext, TMessage>(TNext value, TMessage message) where TMessage : IMessage, new();

		#endregion

		#region Error: Keep Result Type

		/// <summary>
		/// Return Error result with optional messages
		/// </summary>
		/// <param name="messages">IMessage array</param>
		IError<TResult, TState> Error(params IMessage[] messages);

		/// <summary>
		/// Return Error result with a single message of type <typeparamref name="TMessage"/>
		/// </summary>
		/// <typeparam name="TMessage">IMessage type</typeparam>
		IError<TResult, TState> Error<TMessage>() where TMessage : IMessage, new();

		/// <summary>
		/// Return Error result with a single message of type <typeparamref name="TMessage"/>
		/// </summary>
		/// <typeparam name="TMessage">IMessage type</typeparam>
		IError<TResult, TState> Error<TMessage>(TMessage message) where TMessage : IMessage;

		#endregion

		#region Error: Change Result Type

		/// <summary>
		/// Return Error result with optional messages
		/// </summary>
		/// <typeparam name="TNext">Next Result value type</typeparam>
		/// <param name="messages">IMessage array</param>
		IError<TNext, TState> ErrorNew<TNext>(params IMessage[] messages);

		/// <summary>
		/// Return Error result with a single message of type <typeparamref name="TMessage"/>
		/// </summary>
		/// <typeparam name="TNext">Next Result value type</typeparam>
		/// <typeparam name="TMessage">IMessage type</typeparam>
		IError<TNext, TState> ErrorNew<TNext, TMessage>() where TMessage : IMessage, new();

		/// <summary>
		/// Return Error result with a single message of type <typeparamref name="TMessage"/>
		/// </summary>
		/// <typeparam name="TNext">Next Result value type</typeparam>
		/// <typeparam name="TMessage">IMessage type</typeparam>
		/// <param name="message">Message value</param>
		IError<TNext, TState> ErrorNew<TNext, TMessage>(TMessage message) where TMessage : IMessage, new();

		#endregion

		#region Audit

		/// <summary>
		/// Audit the current result state and return unmodified
		/// <para>Any exceptions will be caught and passed down the pipeline as a <see cref="Jm.AuditException"/> message</para>
		/// </summary>
		/// <param name="audit">Audit action</param>
		IR<TResult, TState> Audit(Action<IR<TResult, TState>> audit);

		/// <summary>
		/// Audit the current result state and return unmodified
		/// <para>If no arguments  <see cref="Jm.AuditException"/> message</para>
		/// <para>Any exceptions will be caught and passed down the pipeline as a <see cref="Jm.AuditException"/> message</para>
		/// </summary>
		/// <param name="isOk">[Optional] Action to run if the current result is <see cref="IOk{TResult, TState}"/></param>
		/// <param name="isOkV">[Optional] Action to run if the current result is <see cref="IOkV{TResult, TState}"/></param>
		/// <param name="isError">[Optional] Action to run if the current result is <see cref="IError{TResult, TState}"/></param>
		/// <param name="isUnknown">[Optional] Action to run if the current result is unknown</param>
		IR<TResult, TState> AuditSwitch(
			Action<IOk<TResult, TState>>? isOk = null,
			Action<IOkV<TResult, TState>>? isOkV = null,
			Action<IError<TResult, TState>>? isError = null,
			Action? isUnknown = null
		);

		#endregion
	}
}
