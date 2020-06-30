using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Jeebs
{
	public static partial class RExtensions_WithState
	{
		#region Change / Remove State

		/// <summary>
		/// Change state on the current result object.
		/// </summary>
		/// <typeparam name="TResult">Result value type</typeparam>
		/// <typeparam name="TState">State value type</typeparam>
		/// <typeparam name="TNext">Next state value type</typeparam>
		/// <param name="this">Current result task (will be executed before changing state)</param>
		/// <param name="state">State value</param>
		public static async Task<IR<TResult, TNext>> ChangeState<TResult, TState, TNext>(this Task<IR<TResult, TState>> @this, TNext state) => await @this.ConfigureAwait(false) switch
		{
			IOkV<TResult, TState> okV => new OkV<TResult, TNext>(okV.Val, state) { Messages = okV.Messages },
			IOk<TResult, TState> ok => new Ok<TResult, TNext>(state) { Messages = ok.Messages },
			IError<TResult, TState> error => new Error<TResult, TNext>(state) { Messages = error.Messages },
			IR<TResult, TState> r => throw new InvalidOperationException($"Unknown R<> subtype: '{r.GetType()}'.")
		};

		/// <summary>
		/// Change state on the current result object.
		/// </summary>
		/// <typeparam name="TResult">Result value type</typeparam>
		/// <typeparam name="TState">State value type</typeparam>
		/// <typeparam name="TNext">Next state value type</typeparam>
		/// <param name="this">Current result task (will be executed before changing state)</param>
		/// <param name="state">State value</param>
		public static async Task<IR<TResult>> RemoveState<TResult, TState>(this Task<IR<TResult, TState>> @this) => await @this.ConfigureAwait(false) switch
		{
			IOkV<TResult, TState> okV => okV.RemoveState(),
			IOk<TResult, TState> ok => ok.RemoveState(),
			IError<TResult, TState> error => error.RemoveState(),
			IR<TResult, TState> r => throw new InvalidOperationException($"Unknown R<> subtype: '{r.GetType()}'.")
		};

		/// <summary>
		/// Remove state from the current result object.
		/// </summary>
		/// <typeparam name="TResult">Result value type</typeparam>
		/// <typeparam name="TState">State value type</typeparam>
		/// <param name="this">Current result task (will be executed before removing state)</param>
		public static async Task<IOk<TResult>> RemoveState<TResult, TState>(this Task<IOk<TResult, TState>> @this)
		{
			var r = await @this.ConfigureAwait(false);
			return new Ok<TResult> { Messages = r.Messages };
		}

		/// <summary>
		/// Remove state from the current result object.
		/// </summary>
		/// <typeparam name="TResult">Result value type</typeparam>
		/// <typeparam name="TState">State value type</typeparam>
		/// <param name="this">Current result task (will be executed before removing state)</param>
		public static async Task<IOkV<TResult>> RemoveState<TResult, TState>(this Task<IOkV<TResult, TState>> @this)
		{
			var r = await @this.ConfigureAwait(false);
			return new OkV<TResult>(r.Val) { Messages = r.Messages };
		}

		/// <summary>
		/// Remove state from the current result object.
		/// </summary>
		/// <typeparam name="TResult">Result value type</typeparam>
		/// <typeparam name="TState">State value type</typeparam>
		/// <param name="this">Current result task (will be executed before removing state)</param>
		public static async Task<IError<TResult>> RemoveState<TResult, TState>(this Task<IError<TResult, TState>> @this)
		{
			var r = await @this.ConfigureAwait(false);
			return new Error<TResult> { Messages = r.Messages };
		}

		#endregion

		/// <summary>
		/// Skip ahead by returning the current object as an <see cref="Error{TResult}"/>
		/// </summary>
		/// <typeparam name="TResult">Result value type</typeparam>
		/// <typeparam name="TState">State value type</typeparam>
		/// <param name="r">Source result</param>
		private static IError<TResult, TState> SkipAhead<TResult, TState>(IR<TResult, TState> r) => (IError<TResult, TState>)r;

		/// <summary>
		/// Skip ahead by returning the current object as an <see cref="Error{TNext}"/>
		/// </summary>
		/// <typeparam name="TResult">Result value type</typeparam>
		/// <typeparam name="TNext">Next result value type</typeparam>
		/// <typeparam name="TState">State value type</typeparam>
		/// <param name="r">Source result</param>
		private static IError<TNext, TState> SkipAhead<TResult, TNext, TState>(IR<TResult, TState> r) => new Error<TNext, TState>(r.State) { Messages = r.Messages };

		/// <summary>
		/// Execute a function, catching any exceptions and skipping head with an error message
		/// </summary>
		/// <typeparam name="TResult">Result value type</typeparam>
		/// <typeparam name="TNext">Next result value type</typeparam>
		/// <typeparam name="TState">State value type</typeparam>
		/// <param name="r">Source result</param>
		/// <param name="f">Async function to execute</param>
		private static IR<TNext, TState> Catch<TResult, TNext, TState>(IR<TResult, TState> r, Func<Task<IR<TNext, TState>>> f)
		{
			try
			{
				return f().GetAwaiter().GetResult();
			}
			catch (Exception ex)
			{
				r.Messages.Add(new Jm.AsyncException(ex));
				return SkipAhead<TResult, TNext, TState>(r);
			}
		}
	}
}
