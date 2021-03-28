// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System;

namespace Jx.Data.Querying
{
	/// <summary>
	/// See <see cref="Jeebs.WordPress.Data.Querying.QueryPartsBuilder{TModel, TOptions}.AddSelect(string, bool)"/>
	/// </summary>
	public class SelectAlreadySetException : Exception
	{
		/// <summary>
		/// Empty constructor
		/// </summary>
		public SelectAlreadySetException() : base("SELECT has already been set.") { }

		/// <summary>
		/// Construct exception
		/// </summary>
		/// <param name="message">Message</param>
		public SelectAlreadySetException(string message) : base(message) { }

		/// <summary>
		/// Construct exception
		/// </summary>
		/// <param name="message">Message</param>
		/// <param name="inner">Inner exception</param>
		public SelectAlreadySetException(string message, Exception inner) : base(message, inner) { }
	}
}
