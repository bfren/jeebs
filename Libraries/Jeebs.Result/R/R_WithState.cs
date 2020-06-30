using System;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace Jeebs
{
	/// <inheritdoc cref="IR{TResult,TState}"/>
	public abstract partial class R<TResult, TState> : IR<TResult, TState>
	{
		#region Static - Start Chain

		/// <summary>
		/// Start a synchronous result chain with an <see cref="Ok{TResult, TState}"/> result
		/// </summary>
		public static IR<TResult, TState> Chain(TState state) => new Ok<TResult, TState>(state);

		/// <summary>
		/// Start an asynchronous result chain with an <see cref="Ok"/> result
		/// </summary>
		public static Task<IR<TResult, TState>> ChainAsync(TState state) => Chain(state).LinkAsync(() => Task.CompletedTask);

		/// <summary>
		/// Start a synchronous result chain with an <see cref="Ok"/> result
		/// </summary>
		public static IR<TResult, TState> ChainV(TResult value, TState state) => new OkV<TResult, TState>(value, state);

		/// <summary>
		/// Start an asynchronous result chain with an <see cref="Ok"/> result
		/// </summary>
		public static Task<IR<TResult, TState>> ChainVAsync(TResult value, TState state) => ChainV(value, state).LinkAsync(() => Task.CompletedTask);

		#endregion

		/// <inheritdoc/>
		public TState State { get; }

		/// <inheritdoc/>
		public abstract bool Val { get; }

		/// <inheritdoc/>
		public MessageList Messages { get; internal set; } = new MessageList();

		protected R(TState state) => State = state;

		/// <summary>
		/// Clear MessageList
		/// </summary>
		public virtual void Dispose()
		{
			Messages.Clear();
			Messages = new MessageList();
		}

		/// <inheritdoc/>
		public IR<TResult, TNext> ChangeState<TNext>(TNext state) => this switch
		{
			IOkV<TResult, TState> okV => new OkV<TResult, TNext>(okV.Val, state) { Messages = Messages },
			IOk<TResult, TState> _ => new Ok<TResult, TNext>(state) { Messages = Messages },
			IError<TResult, TState> _ => new Error<TResult, TNext>(state) { Messages = Messages },
			_ => throw new InvalidOperationException($"Unknown R<> subtype: '{GetType()}'.")
		};

		/// <inheritdoc/>
		public IR<TResult> RemoveState() => this switch
		{
			IOkV<TResult, TState> okV => okV.RemoveState(),
			IOk<TResult, TState> ok => ok.RemoveState(),
			IError<TResult, TState> error => error.RemoveState(),
			_ => throw new InvalidOperationException($"Unknown R<> subtype: '{GetType()}'.")
		};

		#region Private Methods

		/// <summary>
		/// Skip ahead by returning the current object as an <see cref="Error{T}"/>
		/// </summary>
		private IError<TResult, TState> SkipAhead() => this switch
		{
			IError<TResult, TState> e => e,
			_ => SkipAhead<TResult>()
		};

		/// <summary>
		/// Skip ahead by returning the current object as an <see cref="Error{TNext}"/>
		/// </summary>
		/// <typeparam name="TNext">Next Result value type</typeparam>
		private IError<TNext, TState> SkipAhead<TNext>() => new Error<TNext, TState>(State) { Messages = Messages };

		/// <summary>
		/// Execute a function, catching any exceptions and skipping head with an error message
		/// </summary>
		/// <typeparam name="TNext">Next Result value type</typeparam>
		/// <param name="f">Function to execute</param>
		private IR<TNext, TState> Catch<TNext>(Func<IR<TNext, TState>> f)
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
