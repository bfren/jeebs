// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System.Collections.Generic;

namespace Jeebs
{
	/// <summary>
	/// List that supports paging operations
	/// </summary>
	/// <typeparam name="T">Type of objects in the list</typeparam>
	public interface IPagedList<T> : IList<T>
	{
		/// <summary>
		/// IPagingValues used to create this list
		/// </summary>
		IPagingValues Values { get; }
	}
}
