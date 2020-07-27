using System;
using System.Collections.Generic;
using System.Text;
using Jeebs;

namespace Jm
{
	/// <inheritdoc cref="AuditExceptionMsg"/>
	public sealed class AuditAsyncExceptionMsg : ExceptionMsg
	{
		internal AuditAsyncExceptionMsg(Exception ex) : base(ex) { }
	}
}
