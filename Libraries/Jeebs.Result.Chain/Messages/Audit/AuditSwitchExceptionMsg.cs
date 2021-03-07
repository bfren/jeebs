// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System;

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
