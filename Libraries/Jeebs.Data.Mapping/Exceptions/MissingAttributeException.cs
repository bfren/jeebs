// Copyright (c) bcg|design.
// Licensed under https://mit.bcgdesign.com/2013.

using System;
using Jeebs.Data.Mapping;

namespace Jx.Data.Mapping
{
	/// <summary>
	/// See <see cref="MapService.GetColumnWithAttribute{TEntity, TAttribute}(IMappedColumnList)"/>
	/// </summary>
	public class MissingAttributeException : Exception
	{
		/// <summary>
		/// Exception message format
		/// </summary>
		public const string Format = "There must be a [{0}] property for entity type '{1}'.";

		/// <summary>
		/// Create exception
		/// </summary>
		public MissingAttributeException() { }

		/// <summary>
		/// Create exception
		/// </summary>
		/// <param name="entity">Entity type</param>
		/// <param name="attribute">Attribute name</param>
		public MissingAttributeException(Type entity, string attribute) : this(string.Format(Format, attribute, entity)) { }

		/// <summary>
		/// Create exception
		/// </summary>
		/// <param name="message">Message</param>
		public MissingAttributeException(string message) : base(message) { }

		/// <summary>
		/// Create exception
		/// </summary>
		/// <param name="message">Message</param>
		/// <param name="inner">Exception</param>
		public MissingAttributeException(string message, Exception inner) : base(message, inner) { }
	}
}
