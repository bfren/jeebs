﻿// Copyright (c) bcg|design.
// Licensed under https://mit.bcgdesign.com/2013.

using System;

namespace Jm.AuditAsync
{
	/// <inheritdoc cref="Audit.AuditSwitchExceptionMsg"/>
	public sealed class AuditSwitchAsyncExceptionMsg : ExceptionMsg
	{
		internal AuditSwitchAsyncExceptionMsg(Exception ex) : base(ex) { }
	}
}
