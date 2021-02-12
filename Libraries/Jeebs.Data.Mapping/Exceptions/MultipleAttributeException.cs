using System;
using System.Collections.Generic;
using System.Text;
using Jeebs.Data.Mapping;

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
