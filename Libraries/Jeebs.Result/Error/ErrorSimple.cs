using System;
using System.Collections.Generic;
using System.Text;

namespace Jeebs
{
	/// <inheritdoc cref="IError"/>
	public class Error : Error<bool>, IError
	{
		internal Error() { }
	}
}
