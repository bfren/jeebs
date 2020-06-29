using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Jeebs
{
	/// <summary>
	/// Used to begin the result chain.
	/// </summary>
	public static class R
	{
		/// <summary>
		/// Start a synchronous result chain with an <see cref="Ok"/> result
		/// </summary>
		public static IR<bool> Chain { get => new Ok(); }

		/// <summary>
		/// Start an asynchronous result chain with an <see cref="Ok"/> result
		/// </summary>
		public static Task<IR<bool>> ChainAsync { get => Chain.LinkAsync(() => Task.CompletedTask); }
	}

	/// <inheritdoc cref="IR{TResult}"/>
	public abstract partial class R<TResult> : IR<TResult>
	{
		#region Static - Start Chain

		/// <summary>
		/// Start a synchronous result chain with an <see cref="Jeebs.Ok{TResult}"/> result
		/// </summary>
		public static IR<TResult> Chain { get => new Ok<TResult>(); }

		/// <summary>
		/// Start an asynchronous result chain with an <see cref="Jeebs.Ok{TResult}"/> result
		/// </summary>
		public static Task<IR<TResult>> ChainAsync { get => Chain.LinkAsync(() => Task.CompletedTask); }

		/// <summary>
		/// Start a synchronous result chain with an <see cref="Jeebs.OkV{TResult}"/> result
		/// </summary>
		public static IR<TResult> ChainV(TResult value) => new OkV<TResult>(value);

		/// <summary>
		/// Start an asynchronous result chain with an <see cref="Jeebs.OkV{TResult}"/> result
		/// </summary>
		public static Task<IR<TResult>> ChainVAsync(TResult value) => ChainV(value).LinkAsync(() => Task.CompletedTask);

		#endregion

		/// <inheritdoc/>
		public abstract bool Val { get; }

		/// <inheritdoc/>
		public MessageList Messages { get; internal set; } = new MessageList();

		/// <summary>
		/// Add state to the current result object.
		/// </summary>
		/// <typeparam name="TState">State value type</typeparam>
		/// <param name="state">State value</param>
		public IR<TResult, TState> WithState<TState>(TState state) => this switch
		{
			IOkV<TResult> okV => new OkV<TResult, TState>(okV.Val, state) { Messages = Messages },
			IOk<TResult> _ => new Ok<TResult, TState>(state) { Messages = Messages },
			IError _ => new Error<TResult, TState>(state) { Messages = Messages },
			_ => throw new InvalidOperationException($"Unknown R<> subtype: '{GetType()}'.")
		};

		/// <summary>
		/// Clear MessageList
		/// </summary>
		public virtual void Dispose()
		{
			Messages.Clear();
			Messages = new MessageList();
		}

		#region Private Methods

		/// <summary>
		/// Skip ahead by returning the current object as an <see cref="Error{T}"/>
		/// </summary>
		private IError<TResult> SkipAhead() => this switch
		{
			IError<TResult> e => e,
			_ => SkipAhead<TResult>()
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

		#endregion
	}
}
