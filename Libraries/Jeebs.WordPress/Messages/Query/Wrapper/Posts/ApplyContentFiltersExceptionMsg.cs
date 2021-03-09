// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System;

namespace Jm.WordPress.Query.Wrapper.Posts
{
	/// <summary>
	/// Error applying content filters to posts
	/// </summary>
	public sealed class ApplyContentFiltersExceptionMsg : ExceptionMsg
	{
		/// <summary>
		/// Create message
		/// </summary>
		/// <param name="e">Exception</param>
		public ApplyContentFiltersExceptionMsg(Exception e) : base(e) { }
	}
}
