using System;
using System.Collections.Generic;
using System.Text;
using Jeebs.Data;
using Jeebs.Data.Mapping;

namespace Jx.Data.Mapping
{
	/// <summary>
	/// See <see cref="MappedColumnList(Type)"/> and <see cref="MappedColumnList(Type, IEnumerable{IMappedColumn})"/>
	/// </summary>
	[Serializable]
	public class InvalidTypeException : Exception
	{
		/// <summary>
		/// Exception message format
		/// </summary>
		public const string Format = "'{0}' must inherit from " + nameof(IEntity) + ".";

		/// <summary>
		/// Create exception
		/// </summary>
		public InvalidTypeException() { }

		/// <summary>
		/// Create exception
		/// </summary>
		/// <param name="entityType">Entity type</param>
		public InvalidTypeException(Type entityType) : this(string.Format(Format, entityType)) { }

		/// <summary>
		/// Create exception
		/// </summary>
		/// <param name="message">Message</param>
		public InvalidTypeException(string message) : base(message) { }

		/// <summary>
		/// Create exception
		/// </summary>
		/// <param name="message">Message</param>
		/// <param name="inner">Exception</param>
		public InvalidTypeException(string message, Exception inner) : base(message, inner) { }

		/// <summary>
		/// Create exception
		/// </summary>
		/// <param name="info">SerializationInfo</param>
		/// <param name="context">StreamingContext</param>
		protected InvalidTypeException(
		  System.Runtime.Serialization.SerializationInfo info,
		  System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
	}
}
