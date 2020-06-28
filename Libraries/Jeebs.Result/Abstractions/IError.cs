using System;
using System.Collections.Generic;
using System.Text;

namespace Jeebs
{
	public interface IError<T> : IR<T> { }

	public interface IError : IError<bool> { }
}
