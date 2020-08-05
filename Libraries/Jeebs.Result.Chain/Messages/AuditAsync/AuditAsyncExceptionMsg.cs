using System;
using System.Collections.Generic;
using System.Text;
using Jeebs;

namespace Jm.AuditAsync
{
	/// <inheritdoc cref="Audit.AuditExceptionMsg"/>
	public sealed class AuditAsyncExceptionMsg : ExceptionMsg
	{
		internal AuditAsyncExceptionMsg(Exception ex) : base(ex) { }
	}
}
