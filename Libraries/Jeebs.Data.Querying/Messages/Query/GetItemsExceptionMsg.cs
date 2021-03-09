﻿// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System;

namespace Jm.Data.Querying.Query
{
	/// <summary>
	/// Get items exception
	/// </summary>
	public sealed class GetItemsExceptionMsg : ExceptionMsg
	{
		/// <summary>
		/// Create message
		/// </summary>
		/// <param name="e">Exception</param>
		public GetItemsExceptionMsg(Exception e) : base(e) { }
	}
}
