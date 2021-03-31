// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System;
using Jeebs.WordPress.Data.Mapping;

namespace Jx.Data.Mapping
{
	/// <summary>
	/// See <see cref="MapService.GetTableMapFor{TEntity}"/>
	/// </summary>
	public class UnmappedEntityException : Exception
	{
		/// <summary>
		/// Exception message format
		/// </summary>
		public const string Format = "Entity '{0}' has not been mapped.";

		/// <summary>
		/// Create exception
		/// </summary>
		public UnmappedEntityException() { }

		/// <summary>
		/// Create exception
		/// </summary>
		/// <param name="entityType">Entity Type</param>
		public UnmappedEntityException(Type entityType) : this(string.Format(Format, entityType)) { }

		/// <summary>
		/// Create exception
		/// </summary>
		/// <param name="message">Message</param>
		public UnmappedEntityException(string message) : base(message) { }

		/// <summary>
		/// Create exception
		/// </summary>
		/// <param name="message">Message</param>
		/// <param name="inner">Exception</param>
		public UnmappedEntityException(string message, Exception inner) : base(message, inner) { }
	}
}
