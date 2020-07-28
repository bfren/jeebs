using System;
using System.Collections.Generic;
using System.Text;

namespace Jm
{
	/// <summary>
	/// Used to catch and pass exceptions down the result chain
	/// </summary>
	public class ChainExceptionMsg : ExceptionMsg
	{
		internal ChainExceptionMsg(Exception ex) : base(ex) { }
	}
}
