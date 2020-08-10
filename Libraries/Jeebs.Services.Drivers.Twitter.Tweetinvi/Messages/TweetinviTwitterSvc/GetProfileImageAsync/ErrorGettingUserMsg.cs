using System;
using System.Collections.Generic;
using System.Text;
using Jeebs;

namespace Jm.Services.Twitter.TweetinviTwitterSvc.GetProfileImageAsync
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
