using System;
using System.Collections.Generic;
using System.Text;

namespace Jeebs
{
	/// <summary>
	/// Represents an OK result without a value but with state
	/// </summary>
	/// <typeparam name="TResult">Result value type</typeparam>
	/// <typeparam name="TState">State value type</typeparam>
	public interface IOk<TResult, TState> : IR<TResult, TState>
	{
		/// <inheritdoc cref="IR{TResult, TState}.RemoveState"/>
		new IOk<TResult> RemoveState();
	}

	/// <summary>
	/// Represents an OK result with a value and with state
	/// </summary>
	/// <typeparam name="TResult">Result value type</typeparam>
	/// <typeparam name="TState">State value type</typeparam>
	public interface IOkV<TResult, TState> : IR<TResult, TState>
	{
		/// <inheritdoc cref="IR.Val"/>
		new public TResult Val { get; }

		/// <inheritdoc cref="IR{TResult, TState}.RemoveState"/>
		new IOkV<TResult> RemoveState();
	}
}
