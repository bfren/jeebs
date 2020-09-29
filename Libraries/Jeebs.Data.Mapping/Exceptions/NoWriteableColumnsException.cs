using System;
using System.Collections.Generic;
using System.Text;
using Jeebs.Data.Mapping;

namespace Jx.Data.Mapping
{
	/// <summary>
	/// See <see cref="TableMap.GetWriteableColumnNamesAndAliases"/>
	/// </summary>

	[Serializable]
	public class NoWriteableColumnsException : Exception
	{
		public const string Format = "Table {0} does not have any writeable columns.";

		/// <summary>
		/// Create exception
		/// </summary>
		public NoWriteableColumnsException() { }

		/// <summary>
		/// Create exception
		/// </summary>
		/// <param name="table">Table Name</param>
		public NoWriteableColumnsException(string table) : base(string.Format(Format, table)) { }

		/// <summary>
		/// Create exception
		/// </summary>
		/// <param name="message">Message</param>
		/// <param name="inner">Exception</param>
		public NoWriteableColumnsException(string message, Exception inner) : base(message, inner) { }

		/// <summary>
		/// Create exception
		/// </summary>
		/// <param name="info">SerializationInfo</param>
		/// <param name="context">StreamingContext</param>
		protected NoWriteableColumnsException(
		  System.Runtime.Serialization.SerializationInfo info,
		  System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
	}
}
