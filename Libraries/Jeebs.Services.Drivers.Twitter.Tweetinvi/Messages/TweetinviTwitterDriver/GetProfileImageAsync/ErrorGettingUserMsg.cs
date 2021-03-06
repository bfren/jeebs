// Copyright (c) bcg|design.
// Licensed under https://mit.bcgdesign.com/2013.

using System;

namespace Jm.Services.Twitter.TweetinviTwitterDriver.GetProfileImageAsync
{
	/// <summary>
	/// Twitter service was unable to find the specified user
	/// </summary>
	public sealed class ErrorGettingUserMsg : ExceptionMsg
	{
		/// <summary>
		/// Save exception
		/// </summary>
		/// <param name="ex">Exception</param>
		public ErrorGettingUserMsg(Exception ex) : base(ex) { }
	}
}
