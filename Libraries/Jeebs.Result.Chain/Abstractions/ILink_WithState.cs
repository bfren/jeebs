using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Jeebs
{
	/// <summary>
	/// Chain Link interface - with value and state
	/// </summary>
	/// <typeparam name="TValue">Result value type</typeparam>
	/// <typeparam name="TState">Result chain type</typeparam>
	public interface ILink<TValue, TState> : ILink<TValue>
	{
		#region Map

		/// <inheritdoc cref="Map{TNext}(Func{IOk, IR{TNext}})"/>
		new IR<TNext, TState> Map<TNext>(Func<IOk, IR<TNext>> f);

		/// <inheritdoc cref="Map{TNext}(Func{IOk, IR{TNext}})"/>
		new Task<IR<TNext, TState>> MapAsync<TNext>(Func<IOk, Task<IR<TNext>>> f);

		/// <inheritdoc cref="Map{TNext}(Func{IOk{TValue}, IR{TNext}})"/>
		new IR<TNext, TState> Map<TNext>(Func<IOk<TValue>, IR<TNext>> f);

		/// <inheritdoc cref="Map{TNext}(Func{IOk{TValue}, IR{TNext}})"/>
		new Task<IR<TNext, TState>> MapAsync<TNext>(Func<IOk<TValue>, Task<IR<TNext>>> f);

		/// <inheritdoc cref="Map{TNext}(Func{IOkV{TValue}, IR{TNext}})"/>
		new IR<TNext, TState> Map<TNext>(Func<IOkV<TValue>, IR<TNext>> f);

		/// <inheritdoc cref="Map{TNext}(Func{IOkV{TValue}, IR{TNext}})"/>
		new Task<IR<TNext, TState>> MapAsync<TNext>(Func<IOkV<TValue>, Task<IR<TNext>>> f);

		/// <summary>
		/// Map to a new result with a new value type
		/// <para>Any exceptions will be caught and added to <see cref="IR.Messages"/> as a <see cref="Jm.ChainExceptionMsg"/> - and an <see cref="IError{TValue, TState}"/> will be returned</para>
		/// </summary>
		/// <typeparam name="TNext">Next result type</typeparam>
		/// <param name="f">Function which receives the current result (if it's an <see cref="IOk{TValue, TState}"/>) and returns the next result</param>
		IR<TNext, TState> Map<TNext>(Func<IOk<TValue, TState>, IR<TNext, TState>> f);

		/// <inheritdoc cref="Map{TNext}(Func{IOk{TValue, TState}, IR{TNext, TState}})"/>
		Task<IR<TNext, TState>> MapAsync<TNext>(Func<IOk<TValue, TState>, Task<IR<TNext, TState>>> f);

		/// <summary>
		/// Map to a new result with a new value type
		/// <para>Any exceptions will be caught and added to <see cref="IR.Messages"/> as a <see cref="Jm.ChainExceptionMsg"/> - and an <see cref="IError{TValue, TState}"/> will be returned</para>
		/// </summary>
		/// <typeparam name="TNext">Next result type</typeparam>
		/// <param name="f">Function which receives the current result (if it's an <see cref="IOkV{TValue, TState}"/>) and returns the next result</param>
		IR<TNext, TState> Map<TNext>(Func<IOkV<TValue, TState>, IR<TNext, TState>> f);

		/// <inheritdoc cref="Map{TNext}(Func{IOkV{TValue, TState}, IR{TNext, TState}})"/>
		Task<IR<TNext, TState>> MapAsync<TNext>(Func<IOkV<TValue, TState>, Task<IR<TNext, TState>>> f);

		#endregion

		#region Run

		/// <inheritdoc cref="ILink{TValue}.Run(Action)"/>
		new IR<TValue, TState> Run(Action f);

		/// <inheritdoc cref="Run(Action)"/>
		new Task<IR<TValue, TState>> RunAsync(Func<Task> f);

		/// <inheritdoc cref="ILink{TValue}.Run(Action{IOk})"/>
		new IR<TValue, TState> Run(Action<IOk> f);

		/// <inheritdoc cref="ILink.RunAsync(Func{IOk, Task})"/>
		new Task<IR<TValue, TState>> RunAsync(Func<IOk, Task> f);

		/// <inheritdoc cref="ILink{TValue}.Run(Action{IOk{TValue}})"/>
		new IR<TValue, TState> Run(Action<IOk<TValue>> f);

		/// <inheritdoc cref="Run(Action{IOk{TValue}})"/>
		new Task<IR<TValue, TState>> RunAsync(Func<IOk<TValue>, Task> f);

		/// <inheritdoc cref="ILink{TValue}.Run(Action{IOkV{TValue}})"/>
		new IR<TValue, TState> Run(Action<IOkV<TValue>> f);

		/// <inheritdoc cref="Run(Action{IOkV{TValue}})"/>
		new Task<IR<TValue, TState>> RunAsync(Func<IOkV<TValue>, Task> f);

		/// <summary>
		/// Run an action and return <see cref="IR{TValue, TState}"/>
		/// <para>The action will receive the current result as an input - if it's an <see cref="IOk{TValue, TState}"/></para>
		/// <para>Any exceptions will be caught and added to <see cref="IR.Messages"/> as a <see cref="Jm.ChainExceptionMsg"/> - and an <see cref="IError{TValue, TState}"/> will be returned</para>
		/// </summary>
		/// <param name="f">Action which receives the current result (if it's an <see cref="IOk{TValue, TState}"/>)</param>
		IR<TValue, TState> Run(Action<IOk<TValue, TState>> f);

		/// <inheritdoc cref="Run(Action{IOk{TValue, TState}})"/>
		Task<IR<TValue, TState>> RunAsync(Func<IOk<TValue, TState>, Task> f);

		/// <summary>
		/// Run an action and return <see cref="IR{TValue, TState}"/>
		/// <para>The action will receive the current result as an input - if it's an <see cref="IOkV{TValue, TState}"/></para>
		/// <para>Any exceptions will be caught and added to <see cref="IR.Messages"/> as a <see cref="Jm.ChainExceptionMsg"/> - and an <see cref="IError{TValue, TState}"/> will be returned</para>
		/// </summary>
		/// <param name="f">Action which receives the current result (if it's an <see cref="IOkV{TValue, TState}"/>)</param>
		IR<TValue, TState> Run(Action<IOkV<TValue, TState>> f);

		/// <inheritdoc cref="Run(Action{IOkV{TValue, TState}})"/>
		Task<IR<TValue, TState>> RunAsync(Func<IOkV<TValue, TState>, Task> f);

		#endregion

		#region Wrap

		/// <inheritdoc cref="ILink{TValue}.Wrap(TValue)"/>
		new IR<TNext, TState> Wrap<TNext>(TNext value);

		/// <inheritdoc cref="ILink{TValue}.Wrap(Func{TValue})"/>
		new IR<TNext, TState> Wrap<TNext>(Func<TNext> f);

		/// <inheritdoc cref="ILink{TValue}.WrapAsync(Func{Task{TValue}})"/>
		new Task<IR<TNext, TState>> WrapAsync<TNext>(Func<Task<TNext>> f);

		#endregion
	}
}
