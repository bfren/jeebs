using System;
using System.Collections.Generic;
using System.Text;

namespace Jm.Services.Twitter.TweetinviTwitterDriver.GetProfileImageAsync
{
	/// <summary>
	/// Something went wrong while retrieving Twitter profile image
	/// </summary>
	public sealed class ErrorGettingProfileImageStreamMsg : ExceptionMsg
	{
		/// <summary>
		/// Save exception
		/// </summary>
		/// <param name="ex">Exception</param>
		public ErrorGettingProfileImageStreamMsg(Exception ex) : base(ex) { }
	}
}
