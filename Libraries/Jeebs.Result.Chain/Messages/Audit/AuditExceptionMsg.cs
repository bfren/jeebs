using System;
using System.Collections.Generic;
using System.Text;
using Jeebs;

namespace Jm.Audit
{
	/// <summary>
	/// Audit exception handling message: Audit
	/// </summary>
	public sealed class AuditExceptionMsg : ExceptionMsg
	{
		internal AuditExceptionMsg(Exception ex) : base(ex) { }
	}
}
