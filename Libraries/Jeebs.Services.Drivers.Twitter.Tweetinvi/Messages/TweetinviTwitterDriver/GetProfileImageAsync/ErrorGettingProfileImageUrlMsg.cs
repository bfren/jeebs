// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System;

namespace Jm.Services.Twitter.TweetinviTwitterDriver.GetProfileImageAsync
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
