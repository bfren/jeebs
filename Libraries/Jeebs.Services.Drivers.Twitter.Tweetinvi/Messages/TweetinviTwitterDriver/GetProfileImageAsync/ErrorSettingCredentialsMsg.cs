using System;
using System.Collections.Generic;
using System.Text;
using Jeebs;

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
