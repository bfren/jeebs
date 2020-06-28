using System;
using System.Collections.Generic;
using System.Text;

namespace Jeebs
{
	/// <summary>
	/// Result interface
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public interface IR<T> : IDisposable
	{
		/// <summary>
		/// Result value
		/// </summary>
		bool Val { get; }

		/// <summary>
		/// Message List - used to pass information down the chain to subsequent functions
		/// </summary>
		MessageList Messages { get; }

		#region Link & LinkMap

		/// <summary>
		/// Execute the next link in the chain
		/// <para>Action <paramref name="a"/> receives no input and returns <see cref="Ok()"/> as output</para>
		/// <para>Any exceptions will be caught and passed down the pipeline as a <see cref="Jm.Exception"/> message</para>
		/// </summary>
		/// <param name="a">The action will be executed if the current object is an <see cref="Jeebs.Ok{T}"/> result</param>
		IR<T> Link(Action a);

		/// <summary>
		/// Execute the next link in the chain
		/// <para>Action <paramref name="a"/> receives the current object as an input and returns <see cref="Ok()"/> as output</para>
		/// <para>Any exceptions will be caught and passed down the pipeline as a <see cref="Jm.Exception"/> message</para>
		/// </summary>
		/// <param name="a">The action will be executed if the current object is an <see cref="Jeebs.Ok{T}"/> result</param>
		IR<T> Link(Action<IOk<T>> a);

		/// <summary>
		/// Execute the next link in the chain
		/// <para>Action <paramref name="a"/> receives the current object as an input and returns <see cref="Ok()"/> as output</para>
		/// <para>Any exceptions will be caught and passed down the pipeline as a <see cref="Jm.Exception"/> message</para>
		/// </summary>
		/// <param name="a">The action will be executed if the current object is an <see cref="Jeebs.OkV{T}"/> result</param>
		IR<T> Link(Action<IOkV<T>> a);

		/// <summary>
		/// Execute the next link in the chain and map to a new result type
		/// <para>Func <paramref name="f"/> receives no input and returns a value of type <typeparamref name="TNext"/>, which is then wrapped in an <see cref="Jeebs.OkV{TNext}"/> object</para>
		/// <para>Any exceptions will be caught and passed down the pipeline as a <see cref="Jm.Exception"/> message</para>
		/// </summary>
		/// <typeparam name="TNext">Next Result value type</typeparam>
		/// <param name="f">The function will be executed if the current object is an <see cref="Jeebs.Ok{T}"/> result</param>
		IR<TNext> LinkMap<TNext>(Func<TNext> f);

		/// <summary>
		/// Execute the next link in the chain and map to a new result type
		/// <para>Func <paramref name="f"/> receives the current object as an input and returns a new result of type <see cref="R{TNext}"/></para>
		/// <para>Any exceptions will be caught and passed down the pipeline as a <see cref="Jm.Exception"/> message</para>
		/// </summary>
		/// <typeparam name="TNext">Next Result value type</typeparam>
		/// <param name="f">The function will be executed if the current object is an <see cref="Jeebs.Ok{T}"/> result</param>
		IR<TNext> LinkMap<TNext>(Func<IOk<T>, IR<TNext>> f);

		/// <summary>
		/// Execute the next link in the chain and map to a new result type
		/// <para>Func <paramref name="f"/> receives the current object as an input and returns a new result of type <see cref="R{TNext}"/></para>
		/// <para>Any exceptions will be caught and passed down the pipeline as a <see cref="Jm.Exception"/> message</para>
		/// </summary>
		/// <typeparam name="TNext">Next Result value type</typeparam>
		/// <param name="f">The function will be executed if the current object is an <see cref="Jeebs.OkV{T}"/> result</param>
		IR<TNext> LinkMap<TNext>(Func<IOkV<T>, IR<TNext>> f);

		#endregion

		#region Ok: Keep Result Type

		/// <summary>
		/// Return Ok result with optional messages
		/// </summary>
		/// <param name="messages">IMessage array</param>
		IOk<T> Ok(params IMessage[] messages);

		/// <summary>
		/// Return Ok result with a single message of type <typeparamref name="TMessage"/>
		/// </summary>
		/// <typeparam name="TMessage">IMessage type</typeparam>
		IOk<T> Ok<TMessage>() where TMessage : IMessage, new();

		/// <summary>
		/// Return Ok result with a single message of type <typeparamref name="TMessage"/>
		/// </summary>
		/// <typeparam name="TMessage">IMessage type</typeparam>
		IOk<T> Ok<TMessage>(TMessage message) where TMessage : IMessage;

		#endregion

		#region Ok: Change Result Type

		/// <summary>
		/// Return Ok result with optional messages
		/// </summary>
		/// <typeparam name="TNext">Next Result value type</typeparam>
		/// <param name="messages">IMessage array</param>
		IOk<TNext> OkNew<TNext>(params IMessage[] messages);

		/// <summary>
		/// Return Ok result with a single message of type <typeparamref name="TMessage"/>
		/// </summary>
		/// <typeparam name="TNext">Next Result value type</typeparam>
		/// <typeparam name="TMessage">IMessage type</typeparam>
		IOk<TNext> OkNew<TNext, TMessage>() where TMessage : IMessage, new();

		/// <summary>
		/// Return Ok result with a single message of type <typeparamref name="TMessage"/>
		/// </summary>
		/// <typeparam name="TNext">Next Result value type</typeparam>
		/// <typeparam name="TMessage">IMessage type</typeparam>
		/// <param name="message">Message value</param>
		IOk<TNext> OkNew<TNext, TMessage>(TMessage message) where TMessage : IMessage, new();

		#endregion

		#region Ok: Change Result Type and Add Value

		/// <summary>
		/// Return Ok result with optional messages
		/// </summary>
		/// <typeparam name="TNext">Next Result value type</typeparam>
		/// <param name="value">Ok result value</param>
		/// <param name="messages">IMessage array</param>
		IOkV<TNext> OkV<TNext>(TNext value, params IMessage[] messages);

		/// <summary>
		/// Return Ok result with a single message of type <typeparamref name="TMessage"/>
		/// </summary>
		/// <typeparam name="TNext">Next Result value type</typeparam>
		/// <typeparam name="TMessage">IMessage type</typeparam>
		/// <param name="value">Ok result value</param>
		IOkV<TNext> OkV<TNext, TMessage>(TNext value) where TMessage : IMessage, new();

		/// <summary>
		/// Return Ok result with a single message of type <typeparamref name="TMessage"/>
		/// </summary>
		/// <typeparam name="TNext">Next Result value type</typeparam>
		/// <typeparam name="TMessage">IMessage type</typeparam>
		/// <param name="value">Ok result value</param>
		/// <param name="message">Message value</param>
		IOkV<TNext> OkV<TNext, TMessage>(TNext value, TMessage message) where TMessage : IMessage, new();

		#endregion

		#region Error: Keep Result Type

		/// <summary>
		/// Return Error result with optional messages
		/// </summary>
		/// <param name="messages">IMessage array</param>
		public IError<T> Error(params IMessage[] messages);

		/// <summary>
		/// Return Error result with a single message of type <typeparamref name="TMessage"/>
		/// </summary>
		/// <typeparam name="TMessage">IMessage type</typeparam>
		public IError<T> Error<TMessage>() where TMessage : IMessage, new();

		/// <summary>
		/// Return Error result with a single message of type <typeparamref name="TMessage"/>
		/// </summary>
		/// <typeparam name="TMessage">IMessage type</typeparam>
		public IError<T> Error<TMessage>(TMessage message) where TMessage : IMessage;

		#endregion

		#region Error: Change Result Type

		/// <summary>
		/// Return Error result with optional messages
		/// </summary>
		/// <typeparam name="TNext">Next Result value type</typeparam>
		/// <param name="messages">IMessage array</param>
		public IError<TNext> ErrorNew<TNext>(params IMessage[] messages);

		/// <summary>
		/// Return Error result with a single message of type <typeparamref name="TMessage"/>
		/// </summary>
		/// <typeparam name="TNext">Next Result value type</typeparam>
		/// <typeparam name="TMessage">IMessage type</typeparam>
		public IError<TNext> ErrorNew<TNext, TMessage>() where TMessage : IMessage, new();

		/// <summary>
		/// Return Error result with a single message of type <typeparamref name="TMessage"/>
		/// </summary>
		/// <typeparam name="TNext">Next Result value type</typeparam>
		/// <typeparam name="TMessage">IMessage type</typeparam>
		/// <param name="message">Message value</param>
		public IError<TNext> ErrorNew<TNext, TMessage>(TMessage message) where TMessage : IMessage, new();

		#endregion

		#region Simple

		/// <summary>
		/// Return Simple Ok result with optional messages
		/// </summary>
		/// <param name="messages">IMessage array</param>
		public IOk OkSimple(params IMessage[] messages);

		/// <summary>
		/// Return Simple Ok result with a single message of type <typeparamref name="TMessage"/>
		/// </summary>
		/// <typeparam name="TMessage">IMessage type</typeparam>
		public IOk OkSimple<TMessage>() where TMessage : IMessage, new();

		/// <summary>
		/// Return Simple Ok result with a single message of type <typeparamref name="TMessage"/>
		/// </summary>
		/// <typeparam name="TMessage">IMessage type</typeparam>
		public IOk OkSimple<TMessage>(TMessage message) where TMessage : IMessage;

		/// <summary>
		/// Return Simple Error result with optional messages
		/// </summary>
		/// <param name="messages">IMessage array</param>
		public IError ErrorSimple(params IMessage[] messages);

		/// <summary>
		/// Return Simple Error result with a single message of type <typeparamref name="TMessage"/>
		/// </summary>
		/// <typeparam name="TMessage">IMessage type</typeparam>
		public IError ErrorSimple<TMessage>() where TMessage : IMessage, new();

		/// <summary>
		/// Return Simple Error result with a single message of type <typeparamref name="TMessage"/>
		/// </summary>
		/// <typeparam name="TMessage">IMessage type</typeparam>
		public IError ErrorSimple<TMessage>(TMessage message) where TMessage : IMessage;

		#endregion

		#region Audit

		/// <summary>
		/// Audit the current result state and return unmodified
		/// <para>Any exceptions will be caught and passed down the pipeline as a <see cref="Jm.AuditException"/> message</para>
		/// </summary>
		/// <param name="audit">Audit action</param>
		public IR<T> Audit(Action<IR<T>> audit);

		/// <summary>
		/// Audit the current result state and return unmodified
		/// <para>If no arguments  <see cref="Jm.AuditException"/> message</para>
		/// <para>Any exceptions will be caught and passed down the pipeline as a <see cref="Jm.AuditException"/> message</para>
		/// </summary>
		/// <param name="isOk">[Optional] Action to run if the current result is <see cref="Jeebs.Ok{T}"/></param>
		/// <param name="isOkV">[Optional] Action to run if the current result is <see cref="Jeebs.OkV{T}"/></param>
		/// <param name="isError">[Optional] Action to run if the current result is <see cref="Jeebs.Error{T}"/></param>
		/// <param name="isUnknown">[Optional] Action to run if the current result is unknown</param>
		public IR<T> AuditSwitch(Action<IOk<T>>? isOk = null, Action<IOkV<T>>? isOkV = null, Action<IError<T>>? isError = null, Action? isUnknown = null);

		#endregion
	}

	/// <summary>
	/// Result interface with State object
	/// </summary>
	/// <typeparam name="TResult"></typeparam>
	/// <typeparam name="TState"></typeparam>
	public interface IR<TResult, TState> : IR<TResult>
	{
		/// <summary>
		/// State value
		/// </summary>
		TState State { get; }
	}
}
