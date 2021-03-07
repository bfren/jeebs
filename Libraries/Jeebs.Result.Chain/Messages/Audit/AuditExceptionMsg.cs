// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System;

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
