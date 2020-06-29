using System;
using System.Collections.Generic;
using System.Text;

namespace Jeebs
{
	/// <summary>
	/// Represents an error result with state.
	/// </summary>
	/// <typeparam name="TResult">Result value type - not used in Error classes, only Ok classes have a value</typeparam>
	/// <typeparam name="TState">State value type</typeparam>
	public interface IError<TResult, TState> : IR<TResult, TState> { }
}
