using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Jeebs
{
	/// <summary>
	/// Chain Link interface
	/// </summary>
	public interface ILink
	{
		#region Map

		/// <summary>
		/// Map to a new result with a new value type
		/// <para>Any exceptions will be caught and added to <see cref="IR.Messages"/> as a <see cref="Jm.ChainExceptionMsg"/> - and an <see cref="IError{TValue}"/> will be returned</para>
		/// </summary>
		/// <typeparam name="TNext">Next result type</typeparam>
		/// <param name="f">Function which receives the current result (if it's an <see cref="IOk"/>) and returns the next result</param>
		IR<TNext> Map<TNext>(Func<IOk, IR<TNext>> f);

		/// <inheritdoc cref="Map{TNext}(Func{IOk, IR{TNext}})"/>
		Task<IR<TNext>> MapAsync<TNext>(Func<IOk, Task<IR<TNext>>> f);

		#endregion

		#region Run

		/// <summary>
		/// Run an action and return <see cref="IOk"/>
		/// <para>Any exceptions will be caught and added to <see cref="IR.Messages"/> as a <see cref="Jm.ChainExceptionMsg"/> - and an <see cref="IError"/> will be returned</para>
		/// </summary>
		/// <param name="f">Action to run</param>
		IR Run(Action f);

		/// <inheritdoc cref="Run(Action)"/>
		Task<IR> RunAsync(Func<Task> f);

		/// <summary>
		/// Run an action and return <see cref="IOk"/>
		/// <para>The action will receive the current result as an input - if it's an <see cref="IOk"/></para>
		/// <para>Any exceptions will be caught and added to <see cref="IR.Messages"/> as a <see cref="Jm.ChainExceptionMsg"/> - and an <see cref="IError"/> will be returned</para>
		/// </summary>
		/// <param name="f">Action which receives the current result (if it's an <see cref="IOk"/>)</param>
		IR Run(Action<IOk> f);

		/// <inheritdoc cref="Run(Action{IOk})"/>
		Task<IR> RunAsync(Func<IOk, Task> f);

		#endregion

		#region Wrap

		/// <summary>
		/// Wrap a value in an <see cref="IOkV{TValue}"/> object
		/// </summary>
		/// <typeparam name="TValue">Result value type</typeparam>
		/// <param name="value">Value to wrap</param>
		IR<TValue> Wrap<TValue>(TValue value);

		/// <summary>
		/// Wrap a value in an <see cref="IOkV{TValue}"/> object
		/// </summary>
		/// <typeparam name="TValue">Result value type</typeparam>
		/// <param name="f">Function to return the value to wrap</param>
		IR<TValue> Wrap<TValue>(Func<TValue> f);

		/// <inheritdoc cref="Wrap{TValue}(Func{TValue})"/>
		Task<IR<TValue>> WrapAsync<TValue>(Func<Task<TValue>> f);

		#endregion
	}
}
