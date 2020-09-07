using System;
using System.Collections.Generic;
using System.Text;

namespace Jm.Services.Twitter.TweetinviTwitterDriver.GetTweetsAsync
{
	/// <summary>
	/// Twitter service was unable to convert tweets to <see cref="Jeebs.Services.Twitter.Models.TweetModel"/> 
	/// </summary>
	public sealed class ErrorConvertingTweetsMsg : ExceptionMsg
	{
		/// <summary>
		/// Save exception
		/// </summary>
		/// <param name="ex">Exception</param>
		public ErrorConvertingTweetsMsg(Exception ex) : base(ex) { }
	}
}
