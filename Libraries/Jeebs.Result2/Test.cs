using System;
namespace Jeebs.Result2
{
	public class Test
	{
		public Test()
		{
		}

		IR<string> Ok(IOk ok)
		{
			return ok.Ok<string>();
		}
	}
}
