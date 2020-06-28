using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Jeebs
{
	/// <summary>
	/// Basic result, can also used to begin the result chain
	/// </summary>
	public abstract class R : R<bool>
	{
		/// <summary>
		/// Start a synchronous result chain with an <see cref="Ok"/> result
		/// </summary>
		new public static IR<bool> Chain { get => new Ok(); }

		/// <summary>
		/// Start an asynchronous result chain with an <see cref="Ok"/> result
		/// </summary>
		new public static Task<IR<bool>> ChainAsync { get => Chain.LinkAsync(() => Task.CompletedTask); }
	}

	public abstract class R<TResult, TState> : R<TResult>
	{
		public TState State { get; }

		protected R(TState state) => State = state;
	}

	/// <summary>
	/// Main result class, used for linking functions and auditing values
	/// </summary>
	/// <typeparam name="T">Result value type (there is only an actual value in OkV objects)</typeparam>
	public abstract partial class R<T> : IR<T>
	{
		#region Static - Start Chain

		/// <summary>
		/// Start a synchronous result chain with an <see cref="Ok"/> result
		/// </summary>
		public static IR<T> Chain { get => new Ok<T>(); }

		/// <summary>
		/// Start an asynchronous result chain with an <see cref="Ok"/> result
		/// </summary>
		public static Task<IR<T>> ChainAsync { get => Chain.LinkAsync(() => Task.CompletedTask); }

		/// <summary>
		/// Start a synchronous result chain with an <see cref="Ok"/> result
		/// </summary>
		public static IR<T> ChainV(T value) => new OkV<T>(value);

		/// <summary>
		/// Start an asynchronous result chain with an <see cref="Ok"/> result
		/// </summary>
		public static Task<IR<T>> ChainVAsync(T value) => ChainV(value).LinkAsync(() => Task.CompletedTask);

		#endregion

		/// <summary>
		/// Result value.
		/// </summary>
		public abstract bool Val { get; }

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
		public IR<T> Link(Action a) => this switch
		{
			IOk<T> ok => Catch(() => { a(); return ok; }),
			_ => SkipAhead()
		};

		/// <summary>
		/// Execute the next link in the chain
		/// <para>Action <paramref name="a"/> receives the current object as an input and returns <see cref="Ok()"/> as output</para>
		/// <para>Any exceptions will be caught and passed down the pipeline as a <see cref="Jm.Exception"/> message</para>
		/// </summary>
		/// <param name="a">The action will be executed if the current object is an <see cref="Jeebs.Ok{T}"/> result</param>
		public IR<T> Link(Action<IOk<T>> a) => this switch
		{
			IOk<T> ok => Catch(() => { a(ok); return ok; }),
			_ => SkipAhead()
		};

		/// <summary>
		/// Execute the next link in the chain
		/// <para>Action <paramref name="a"/> receives the current object as an input and returns <see cref="Ok()"/> as output</para>
		/// <para>Any exceptions will be caught and passed down the pipeline as a <see cref="Jm.Exception"/> message</para>
		/// </summary>
		/// <param name="a">The action will be executed if the current object is an <see cref="Jeebs.OkV{T}"/> result</param>
		public IR<T> Link(Action<IOkV<T>> a) => this switch
		{
			IOkV<T> ok => Catch(() => { a(ok); return ok; }),
			_ => SkipAhead()
		};

		/// <summary>
		/// Execute the next link in the chain and map to a new result type
		/// <para>Func <paramref name="f"/> receives no input and returns a value of type <typeparamref name="TNext"/>, which is then wrapped in an <see cref="Jeebs.OkV{TNext}"/> object</para>
		/// <para>Any exceptions will be caught and passed down the pipeline as a <see cref="Jm.Exception"/> message</para>
		/// </summary>
		/// <typeparam name="TNext">Next Result value type</typeparam>
		/// <param name="f">The function will be executed if the current object is an <see cref="Jeebs.Ok{T}"/> result</param>
		public IR<TNext> LinkMap<TNext>(Func<TNext> f) => this switch
		{
			IOk<T> ok => Catch(() => { var v = f(); return ok.OkV(v); }),
			_ => SkipAhead<TNext>()
		};

		/// <summary>
		/// Execute the next link in the chain and map to a new result type
		/// <para>Func <paramref name="f"/> receives the current object as an input and returns a new result of type <see cref="R{TNext}"/></para>
		/// <para>Any exceptions will be caught and passed down the pipeline as a <see cref="Jm.Exception"/> message</para>
		/// </summary>
		/// <typeparam name="TNext">Next Result value type</typeparam>
		/// <param name="f">The function will be executed if the current object is an <see cref="Jeebs.Ok{T}"/> result</param>
		public IR<TNext> LinkMap<TNext>(Func<IOk<T>, IR<TNext>> f) => this switch
		{
			IOk<T> s => Catch(() => f(s)),
			_ => SkipAhead<TNext>()
		};

		/// <summary>
		/// Execute the next link in the chain and map to a new result type
		/// <para>Func <paramref name="f"/> receives the current object as an input and returns a new result of type <see cref="R{TNext}"/></para>
		/// <para>Any exceptions will be caught and passed down the pipeline as a <see cref="Jm.Exception"/> message</para>
		/// </summary>
		/// <typeparam name="TNext">Next Result value type</typeparam>
		/// <param name="f">The function will be executed if the current object is an <see cref="Jeebs.OkV{T}"/> result</param>
		public IR<TNext> LinkMap<TNext>(Func<IOkV<T>, IR<TNext>> f) => this switch
		{
			IOkV<T> s => Catch(() => f(s)),
			_ => SkipAhead<TNext>()
		};

		/// <summary>
		/// Skip ahead by returning the current object as an <see cref="Error{T}"/>
		/// </summary>
		private IError<T> SkipAhead() => this switch
		{
			IError<T> e => e,
			_ => SkipAhead<T>()
		};

		/// <summary>
		/// Skip ahead by returning the current object as an <see cref="Error{TNext}"/>
		/// </summary>
		/// <typeparam name="TNext">Next Result value type</typeparam>
		private IError<TNext> SkipAhead<TNext>() => new Error<TNext> { Messages = Messages };

		/// <summary>
		/// Execute a function, catching any exceptions and skipping head with an error message
		/// </summary>
		/// <typeparam name="TNext">Next Result value type</typeparam>
		/// <param name="f">Function to execute</param>
		private IR<TNext> Catch<TNext>(Func<IR<TNext>> f)
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
		/// Clear MessageList
		/// </summary>
		public virtual void Dispose()
		{
			Messages.Clear();
			Messages = new MessageList();
		}
	}
}
