// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

namespace Jm.Services.Twitter.TweetinviTwitterDriver.GetProfileImageAsync
{
	/// <summary>
	/// Twitter service was unable to find the specified user
	/// </summary>
	public sealed class UserNotFoundMsg : WithValueMsg<string>
	{
		/// <summary>
		/// Save screen name
		/// </summary>
		/// <param name="screenName">Screen name</param>
		public UserNotFoundMsg(string screenName) : base(screenName) { }
	}
}
