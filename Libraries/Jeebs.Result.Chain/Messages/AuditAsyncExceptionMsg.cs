using System;
using System.Collections.Generic;
using System.Text;
using Jeebs;

namespace Jm
{
	/// <summary>
	/// Audit exception handling message
	/// </summary>
	public sealed class AuditExceptionMsg : ExceptionMsg
	{
		internal AuditExceptionMsg(Exception ex) : base(ex) { }
	}
}
