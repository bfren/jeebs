using System;
using System.Collections.Generic;
using System.Text;

namespace Jm.Link
{
	/// <summary>
	/// Used to catch and pass exceptions down the result chain
	/// </summary>
	public class LinkExceptionMsg : ExceptionMsg
	{
		internal LinkExceptionMsg(Exception ex) : base(ex) { }
	}
}
