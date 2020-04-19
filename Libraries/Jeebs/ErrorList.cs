using System;
using System.Collections.Generic;
using System.Text;

namespace Jeebs
{
	/// <inheritdoc cref="IErrorList"/>
	public sealed class ErrorList : List<string>, IErrorList
	{
		/// <summary>
		/// Create from an array of errors
		/// </summary>
		/// <param name="errors">Errors</param>
		internal ErrorList(string[] errors) : base(errors) { }

		/// <summary>
		/// Join errors with a | char
		/// </summary>
		public override string ToString() => string.Join('|', this);
	}
}
