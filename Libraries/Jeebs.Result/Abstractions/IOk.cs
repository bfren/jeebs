using System;
using System.Collections.Generic;
using System.Text;

namespace Jeebs
{
	/// <summary>
	/// Represents an OK result without a value
	/// </summary>
	/// <typeparam name="TResult">Result value type</typeparam>
	public interface IOk<TResult> : IR<TResult> { }

	/// <summary>
	/// Represents an OK result with a value
	/// </summary>
	/// <typeparam name="TResult">Result value type</typeparam>
	public interface IOkV<TResult> : IOk<TResult>
	{
		new public TResult Val { get; }
	}

	/// <summary>
	/// Represents a simple OK result without a value
	/// </summary>
	public interface IOk : IOk<bool> { }

}
