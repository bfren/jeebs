// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System;

namespace Jm.Link
{
	/// <summary>
	/// Used to catch and pass exceptions down the result chain
	/// </summary>
	public class LinkExceptionMsg : ExceptionMsg
	{
		internal LinkExceptionMsg(Exception ex) : base(ex) { }
	}
}
