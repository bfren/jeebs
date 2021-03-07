// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System;

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
