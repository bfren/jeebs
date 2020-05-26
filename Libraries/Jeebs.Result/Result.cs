using System;
using System.Collections.Generic;

namespace Jeebs
{
	/// <summary>
	/// Basic result, also used to begin the result chain
	/// </summary>
	public abstract class R : R<object>
	{
		/// <summary>
		/// Start the result chain with an <see cref="Ok"/> result
		/// </summary>
		public static Ok Chain { get => new Ok(); }
	}

	/// <summary>
	/// Main result class, used for linking functions and auditing values
	/// </summary>
	/// <typeparam name="T">Result value type (there is only an actual value in OkV objects)</typeparam>
	public abstract partial class R<T>
	{
		/// <summary>
		/// Message List - used to pass information down the chain to subsequent functions
		/// </summary>
		public MessageList Messages { get; private set; } = new MessageList();

		/// <summary>
		/// Execute the next link in the chain.
		/// <para>Action <paramref name="f"/> receives no input and gives no output</para>
		/// <para>Any exceptions will be caught and passed down the pipeline as a <see cref="Jm.Exception"/> message</para>
		/// </summary>
		/// <param name="f">The action will be executed if the current object is an Ok result</param>
		public R<T> Link(Action f) => this switch
		{
			Ok<T> _ => Catch(() => { f(); return Ok(); }),
			_ => SkipAhead()
		};

		/// <summary>
		/// Execute the next link in the chain
		/// <para>Action <paramref name="f"/> receives the current object as an input and gives no output</para>
		/// <para>Any exceptions will be caught and passed down the pipeline as a <see cref="Jm.Exception"/> message</para>
		/// </summary>
		/// <param name="f">The action will be executed if the current object is an Ok result</param>
		public R<T> Link(Action<Ok<T>> f) => this switch
		{
			Ok<T> ok => Catch(() => { f(ok); return Ok(); }),
			_ => SkipAhead()
		};

		/// <summary>
		/// Execute the next link in the chain
		/// <para>Action <paramref name="f"/> receives the current object as an input and gives no output</para>
		/// <para>Any exceptions will be caught and passed down the pipeline as a <see cref="Jm.Exception"/> message</para>
		/// </summary>
		/// <param name="f">The action will be executed if the current object is an OkV result</param>
		public R<T> Link(Action<OkV<T>> f) => this switch
		{
			OkV<T> ok => Catch(() => { f(ok); return Ok(); }),
			_ => SkipAhead()
		};

		/// <summary>
		/// Execute the next link in the chain
		/// <para>Action <paramref name="f"/> receives no input and returns a new value as output, which is wrapped in an OkV object</para>
		/// <para>Any exceptions will be caught and passed down the pipeline as a <see cref="Jm.Exception"/> message</para>
		/// </summary>
		/// <typeparam name="TNext">Next Result value type</typeparam>
		/// <param name="f">The function will be executed if the current object is an Ok result</param>
		public R<TNext> Link<TNext>(Func<TNext> f) => this switch
		{
			Ok<T> _ => Catch(() => { var v = f(); return OkV(v); }),
			_ => SkipAhead<TNext>()
		};

		/// <summary>
		/// Execute the next link in the chain
		/// <para>Action <paramref name="f"/> receives no input and returns an Ok result with no value</para>
		/// <para>Any exceptions will be caught and passed down the pipeline as a <see cref="Jm.Exception"/> message</para>
		/// </summary>
		/// <typeparam name="TNext">Next Result value type</typeparam>
		/// <param name="f">The function will be executed if the current object is an Ok result</param>
		public R<TNext> Link<TNext>(Func<Ok<TNext>> f) => this switch
		{
			Ok<T> _ => Catch(f),
			_ => SkipAhead<TNext>()
		};

		/// <summary>
		/// Execute the next link in the chain
		/// <para>Action <paramref name="f"/> receives no input and returns an Ok result with a value of type <typeparamref name="TNext"/></para>
		/// <para>Any exceptions will be caught and passed down the pipeline as a <see cref="Jm.Exception"/> message</para>
		/// </summary>
		/// <typeparam name="TNext">Next Result value type</typeparam>
		/// <param name="f">The function will be executed if the current object is an Ok result</param>
		public R<TNext> Link<TNext>(Func<OkV<TNext>> f) => this switch
		{
			Ok<T> _ => Catch(f),
			_ => SkipAhead<TNext>()
		};

		/// <summary>
		/// Execute the next link in the chain
		/// <para>Action <paramref name="f"/> receives the current object as an input and returns an OK result with a value of type <typeparamref name="TNext"/></para>
		/// <para>Any exceptions will be caught and passed down the pipeline as a <see cref="Jm.Exception"/> message</para>
		/// </summary>
		/// <typeparam name="TNext">Next Result value type</typeparam>
		/// <param name="f">The function will be executed if the current object is an OkV result</param>
		public R<TNext> Link<TNext>(Func<OkV<T>, R<TNext>> f) => this switch
		{
			OkV<T> s => Catch(() => f(s)),
			_ => SkipAhead<TNext>()
		};

		/// <summary>
		/// Skip ahead by returning the current object as an Error
		/// </summary>
		private Error<T> SkipAhead() => (Error<T>)this;

		/// <summary>
		/// Skip ahead by returning the current object as an Error
		/// </summary>
		/// <typeparam name="TNext">Next Result value type</typeparam>
		private Error<TNext> SkipAhead<TNext>() => new Error<TNext> { Messages = Messages };

		/// <summary>
		/// Execute a function, catching any exceptions and skipping head with an error message
		/// </summary>
		/// <typeparam name="TNext">Next Result value type</typeparam>
		/// <param name="f">Function to execute</param>
		private R<TNext> Catch<TNext>(Func<R<TNext>> f)
		{
			try
			{
				return f();
			}
			catch (Exception ex)
			{
				Messages.Add(new Jm.Exception(ex));
				return SkipAhead<TNext>();
			}
		}
	}
}
