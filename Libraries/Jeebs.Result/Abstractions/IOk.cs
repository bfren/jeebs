using System;
using System.Collections.Generic;
using System.Text;

namespace Jeebs
{
	public interface IOk<T> : IR<T> { }

	public interface IOk : IOk<bool> { }

	public interface IOkV<T> : IOk<T>
	{
		new public T Val { get; }
	}
}
