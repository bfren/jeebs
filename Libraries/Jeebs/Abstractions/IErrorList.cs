using System;
using System.Collections.Generic;
using System.Text;

namespace Jeebs
{
	/// <summary>
	/// Holds lists of errors
	/// </summary>
	public interface IErrorList : IList<string>
	{
		/// <summary>
		/// Special case - set to true if the result of the operation is 'Not Found'
		/// </summary>
		bool NotFound { get; }
	}
}
