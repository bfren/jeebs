using System;
using System.Collections.Generic;
using System.Text;

namespace Jm.Services.Twitter.TweetinviTwitterDriver.GetTweetsAsync
{
	/// <summary>
	/// Twitter service was unable to get a user's timeline
	/// </summary>
	public sealed class ErrorGettingTimelineMsg : ExceptionMsg
	{
		/// <summary>
		/// Save exception
		/// </summary>
		/// <param name="ex">Exception</param>
		public ErrorGettingTimelineMsg(Exception ex) : base(ex) { }
	}
}
