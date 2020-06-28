using System;
using System.Collections.Generic;
using System.Text;

namespace Jeebs
{
	/// <summary>
	/// Represents a simple error result
	/// </summary>
	public class Error : Error<bool>, IError
	{
		internal Error() { }
	}
}
