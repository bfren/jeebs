using System;
using System.Collections.Generic;
using System.Text;

namespace Jeebs
{
	/// <summary>
	/// Error List Interface
	/// </summary>
	public interface IErrorList : IList<string>
	{
		/// <summary>
		/// Join errors with a | char
		/// </summary>
		public string ToString() => string.Join('|', this);
	}
}
