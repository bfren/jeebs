// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System;

namespace Jm.WordPress.Query.Wrapper.Posts
{
	/// <summary>
	/// Error adding custom fields to posts
	/// </summary>
	public sealed class AddCustomFieldsExceptionMsg : ExceptionMsg
	{
		/// <summary>
		/// Create message
		/// </summary>
		/// <param name="e">Exception</param>
		public AddCustomFieldsExceptionMsg(Exception e) : base(e) { }
	}
}
