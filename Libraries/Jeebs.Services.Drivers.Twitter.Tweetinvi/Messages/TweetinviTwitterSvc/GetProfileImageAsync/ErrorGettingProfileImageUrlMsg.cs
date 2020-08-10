using System;
using System.Collections.Generic;
using System.Text;

namespace Jm.Services.Twitter.TweetinviTwitterSvc.GetProfileImageAsync
{
	/// <summary>
	/// Something went wrong while retrieving Twitter profile image URL
	/// </summary>
	public sealed class ErrorGettingProfileImageUrlMsg : ExceptionMsg
	{
		/// <summary>
		/// Save exception
		/// </summary>
		/// <param name="ex">Exception</param>
		public ErrorGettingProfileImageUrlMsg(Exception ex) : base(ex) { }
	}
}
