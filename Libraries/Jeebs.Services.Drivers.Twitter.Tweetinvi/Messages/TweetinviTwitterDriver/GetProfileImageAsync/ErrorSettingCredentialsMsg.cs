// Copyright (c) bcg|design.
// Licensed under https://mit.bcgdesign.com/2013.

using System;

namespace Jm.Services.Twitter.TweetinviTwitterDriver.GetProfileImageAsync
{
	/// <summary>
	/// Twitter service was unable to set Twitter credentials
	/// </summary>
	public sealed class ErrorSettingCredentialsMsg : ExceptionMsg
	{
		/// <summary>
		/// Save exception
		/// </summary>
		/// <param name="ex">Exception</param>
		public ErrorSettingCredentialsMsg(Exception ex) : base(ex) { }
	}
}
