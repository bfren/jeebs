using System;
using System.Collections.Generic;
using System.Text;
using Jeebs;

namespace Jm.Audit
{
	/// <summary>
	/// Audit exception handling message: AuditSwitch
	/// </summary>
	public sealed class AuditSwitchExceptionMsg : ExceptionMsg
	{
		internal AuditSwitchExceptionMsg(Exception ex) : base(ex) { }
	}
}
