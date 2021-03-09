// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System;

namespace Jm.Data.Querying.Query
{
	/// <summary>
	/// Get paging values exception
	/// </summary>
	public sealed class GetPagingValuesExceptionMsg : ExceptionMsg
	{
		/// <summary>
		/// Create message
		/// </summary>
		/// <param name="e">Exception</param>
		public GetPagingValuesExceptionMsg(Exception e) : base(e) { }
	}
}
