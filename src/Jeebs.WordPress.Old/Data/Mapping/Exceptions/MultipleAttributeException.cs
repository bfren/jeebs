// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System;
using Jeebs.WordPress.Data.Mapping;

namespace Jx.Data.Mapping
{
	/// <summary>
	/// See <see cref="MapService.GetColumnWithAttribute{TEntity, TAttribute}(IMappedColumnList)"/>
	/// </summary>
	public class MultipleAttributesException : Exception
	{
		/// <summary>
		/// Exception message format
		/// </summary>
		public const string Format = "There may only be one [{0}] property for entity type '{1}'.";

		/// <summary>
		/// Create exception
		/// </summary>
		public MultipleAttributesException() { }

		/// <summary>
		/// Create exception
		/// </summary>
		/// <param name="entity">Entity type</param>
		/// <param name="attribute">Attribute name</param>
		public MultipleAttributesException(Type entity, string attribute) : this(string.Format(Format, attribute, entity)) { }

		/// <summary>
		/// Create exception
		/// </summary>
		/// <param name="message">Message</param>
		public MultipleAttributesException(string message) : base(message) { }

		/// <summary>
		/// Create exception
		/// </summary>
		/// <param name="message">Message</param>
		/// <param name="inner">Exception</param>
		public MultipleAttributesException(string message, Exception inner) : base(message, inner) { }
	}
}
