// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System;

namespace Jm.WordPress.Query.Wrapper.PostsMeta
{
	/// <summary>
	/// Error getting attached files
	/// </summary>
	public sealed class GetAttachedFilesExceptionMsg : ExceptionMsg
	{
		/// <summary>
		/// Create message
		/// </summary>
		/// <param name="e">Exception</param>
		public GetAttachedFilesExceptionMsg(Exception e) : base(e) { }
	}
}
