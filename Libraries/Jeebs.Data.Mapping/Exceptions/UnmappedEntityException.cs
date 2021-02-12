using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using Jeebs.Data.Mapping;

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
