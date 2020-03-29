using System;
using System.Collections.Generic;
using System.Text;

namespace Jeebs
{
	/// <summary>
	/// Wrapper for List to hold lists of errors
	/// </summary>
	public sealed class ErrorList : List<string>
	{
		/// <summary>
		/// Create from an array of errors
		/// </summary>
		/// <param name="errors">Errors</param>
		internal ErrorList(string[] errors) : base(errors) { }

		/// <summary>
		/// Join errors with a comma
		/// </summary>
		public override string ToString() => string.Join(", ", this);
	}
}
