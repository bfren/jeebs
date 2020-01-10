using System;
using System.Collections.Generic;
using System.Text;

namespace Jeebs.Data
{
	/// <summary>
	/// Database result
	/// </summary>
	public interface IDbResult : IResult { }

	/// <summary>
	/// Database result with value type
	/// </summary>
	/// <typeparam name="T">Type of value</typeparam>
	public interface IDbResult<T> : IResult<T> { }
}
