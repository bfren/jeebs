using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Jeebs
{
	/// <summary>
	/// Basic result, also used to begin the result chain
	/// </summary>
	public abstract class R : R<object>
	{
		/// <summary>
		/// Start a synchronous result chain with an <see cref="Ok"/> result
		/// </summary>
		public static R<object> Chain { get => new Ok(); }

		/// <summary>
		/// Start an asynchronous result chain with an <see cref="Ok"/> result
		/// </summary>
		public static Task<R<object>> ChainAsync { get => Chain.LinkAsync(() => Task.CompletedTask); }
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
		public MessageList Messages { get; internal set; } = new MessageList();

		/// <summary>
		/// Execute the next link in the chain
		/// <para>Action <paramref name="a"/> receives no input and returns <see cref="Ok()"/> as output</para>
		/// <para>Any exceptions will be caught and passed down the pipeline as a <see cref="Jm.Exception"/> message</para>
		/// </summary>
		/// <param name="a">The action will be executed if the current object is an <see cref="Jeebs.Ok{T}"/> result</param>
		public R<T> Link(Action a) => this switch
		{
			Ok<T> _ => Catch(() => { a(); return Ok(); }),
			_ => SkipAhead()
		};

		/// <summary>
		/// Execute the next link in the chain
		/// <para>Action <paramref name="a"/> receives the current object as an input and returns <see cref="Ok()"/> as output</para>
		/// <para>Any exceptions will be caught and passed down the pipeline as a <see cref="Jm.Exception"/> message</para>
		/// </summary>
		/// <param name="a">The action will be executed if the current object is an <see cref="Jeebs.Ok{T}"/> result</param>
		public R<T> Link(Action<Ok<T>> a) => this switch
		{
			Ok<T> ok => Catch(() => { a(ok); return Ok(); }),
			_ => SkipAhead()
		};

		/// <summary>
		/// Execute the next link in the chain
		/// <para>Action <paramref name="a"/> receives the current object as an input and returns <see cref="Ok()"/> as output</para>
		/// <para>Any exceptions will be caught and passed down the pipeline as a <see cref="Jm.Exception"/> message</para>
		/// </summary>
		/// <param name="a">The action will be executed if the current object is an <see cref="Jeebs.OkV{T}"/> result</param>
		public R<T> Link(Action<OkV<T>> a) => this switch
		{
			OkV<T> ok => Catch(() => { a(ok); return Ok(); }),
			_ => SkipAhead()
		};

		/// <summary>
		/// Execute the next link in the chain and map to a new result type
		/// <para>Func <paramref name="f"/> receives no input and returns a value of type <typeparamref name="TNext"/>, which is then wrapped in an <see cref="Jeebs.OkV{TNext}"/> object</para>
		/// <para>Any exceptions will be caught and passed down the pipeline as a <see cref="Jm.Exception"/> message</para>
		/// </summary>
		/// <typeparam name="TNext">Next Result value type</typeparam>
		/// <param name="f">The function will be executed if the current object is an <see cref="Jeebs.Ok{T}"/> result</param>
		public R<TNext> LinkMap<TNext>(Func<TNext> f) => this switch
		{
			Ok<T> _ => Catch(() => { var v = f(); return OkV(v); }),
			_ => SkipAhead<TNext>()
		};

		/// <summary>
		/// Execute the next link in the chain and map to a new result type
		/// <para>Func <paramref name="f"/> receives the current object as an input and returns a new result of type <see cref="R{TNext}"/></para>
		/// <para>Any exceptions will be caught and passed down the pipeline as a <see cref="Jm.Exception"/> message</para>
		/// </summary>
		/// <typeparam name="TNext">Next Result value type</typeparam>
		/// <param name="f">The function will be executed if the current object is an <see cref="Jeebs.Ok{T}"/> result</param>
		public R<TNext> LinkMap<TNext>(Func<Ok<T>, R<TNext>> f) => this switch
		{
			Ok<T> s => Catch(() => f(s)),
			_ => SkipAhead<TNext>()
		};

		/// <summary>
		/// Execute the next link in the chain and map to a new result type
		/// <para>Func <paramref name="f"/> receives the current object as an input and returns a new result of type <see cref="R{TNext}"/></para>
		/// <para>Any exceptions will be caught and passed down the pipeline as a <see cref="Jm.Exception"/> message</para>
		/// </summary>
		/// <typeparam name="TNext">Next Result value type</typeparam>
		/// <param name="f">The function will be executed if the current object is an <see cref="Jeebs.OkV{T}"/> result</param>
		public R<TNext> LinkMap<TNext>(Func<OkV<T>, R<TNext>> f) => this switch
		{
			OkV<T> s => Catch(() => f(s)),
			_ => SkipAhead<TNext>()
		};

		/// <summary>
		/// Skip ahead by returning the current object as an <see cref="Error{T}"/>
		/// </summary>
		private Error<T> SkipAhead() => (Error<T>)this;

		/// <summary>
		/// Skip ahead by returning the current object as an <see cref="Error{TNext}"/>
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

		/// <summary>
		/// Execute the next link in the chain and map to a new result type
		/// <para>Action <paramref name="f"/> receives no input and returns an Ok result with no value</para>
		/// <para>Any exceptions will be caught and passed down the pipeline as a <see cref="Jm.Exception"/> message</para>
		/// </summary>
		/// <typeparam name="TNext">Next Result value type</typeparam>
		/// <param name="f">The function will be executed if the current object is an Ok result</param>
		//public R<TNext> LinkMap<TNext>(Func<Ok<TNext>> f) => this switch
		//{
		//	Ok<T> _ => Catch(f),
		//	_ => SkipAhead<TNext>()
		//};

		/// <summary>
		/// Execute the next link in the chain and map to a new result type
		/// <para>Action <paramref name="f"/> receives no input and returns an Ok result with a value of type <typeparamref name="TNext"/></para>
		/// <para>Any exceptions will be caught and passed down the pipeline as a <see cref="Jm.Exception"/> message</para>
		/// </summary>
		/// <typeparam name="TNext">Next Result value type</typeparam>
		/// <param name="f">The function will be executed if the current object is an Ok result</param>
		//public R<TNext> LinkMap<TNext>(Func<OkV<TNext>> f) => this switch
		//{
		//	Ok<T> _ => Catch(f),
		//	_ => SkipAhead<TNext>()
		//};
	}
}
