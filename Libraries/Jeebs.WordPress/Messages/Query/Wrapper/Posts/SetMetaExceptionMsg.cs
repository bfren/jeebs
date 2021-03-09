// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System;

namespace Jm.WordPress.Query.Wrapper.Posts
{
	/// <summary>
	/// Error setting meta
	/// </summary>
	public sealed class SetMetaExceptionMsg : ExceptionMsg
	{
		/// <summary>
		/// Create message
		/// </summary>
		/// <param name="e">Exception</param>
		public SetMetaExceptionMsg(Exception e) : base(e) { }
	}
}
