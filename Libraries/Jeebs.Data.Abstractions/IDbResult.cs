using System;
using System.Collections.Generic;
using System.Text;

namespace Jeebs.Data
{
	public interface IDbResult : IResult { }

	public interface IDbResult<T> : IResult<T> { }
}
