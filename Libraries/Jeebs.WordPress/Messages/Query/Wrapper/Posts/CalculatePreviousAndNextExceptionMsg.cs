// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System;

namespace Jm.WordPress.Query.Wrapper.Posts
{
	/// <summary>
	/// Error calculating previous and next posts
	/// </summary>
	public sealed class CalculatePreviousAndNextExceptionMsg : ExceptionMsg
	{
		/// <summary>
		/// Create message
		/// </summary>
		/// <param name="e">Exception</param>
		public CalculatePreviousAndNextExceptionMsg(Exception e) : base(e) { }
	}
}
