using System;
using System.Collections.Generic;
using System.Text;
using Jeebs;

namespace Jm.AuditAsync
{
	/// <inheritdoc cref="Audit.AuditSwitchExceptionMsg"/>
	public sealed class AuditSwitchAsyncExceptionMsg : ExceptionMsg
	{
		internal AuditSwitchAsyncExceptionMsg(Exception ex) : base(ex) { }
	}
}
