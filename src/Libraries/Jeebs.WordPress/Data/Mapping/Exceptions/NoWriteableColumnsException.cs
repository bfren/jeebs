// Jeebs Rapid Application Development
// Copyright (c) bfren.uk - licensed under https://mit.bfren.uk/2013

using System;
using Jeebs.WordPress.Data.Mapping;

namespace Jx.Data.Mapping
{
	/// <summary>
	/// See <see cref="TableMap.GetWriteableColumnNamesAndAliases"/>
	/// </summary>
	public class NoWriteableColumnsException : Exception
	{
		/// <summary>
		/// Exception message format
		/// </summary>
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
	}
}
