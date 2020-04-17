using System;
using System.Collections.Generic;
using System.Text;

namespace Jeebs
{
	/// <summary>
	/// Wrapper for List to hold lists of errors
	/// </summary>
	public sealed class ErrorList : List<string>, IErrorList
	{
		/// <summary>
		/// Create from an array of errors
		/// </summary>
		/// <param name="errors">Errors</param>
		internal ErrorList(string[] errors) : base(errors) { }
	}
}
