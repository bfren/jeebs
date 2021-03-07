// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System;

namespace Jm.AuditAsync
{
	/// <inheritdoc cref="Audit.AuditExceptionMsg"/>
	public sealed class AuditAsyncExceptionMsg : ExceptionMsg
	{
		internal AuditAsyncExceptionMsg(Exception ex) : base(ex) { }
	}
}
