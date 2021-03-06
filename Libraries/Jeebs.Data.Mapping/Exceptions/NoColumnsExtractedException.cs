// Copyright (c) bcg|design.
// Licensed under https://mit.bcgdesign.com/2013.

using System;
using System.Linq;
using Jeebs.Data.Mapping;

namespace Jx.Data.Mapping
{
	/// <summary>
	/// See <see cref="Extract{TModel}.From(Table[])"/>
	/// </summary>
	public class NoColumnsExtractedException : Exception
	{
		/// <summary>
		/// Exception message format
		/// </summary>
		public const string Format = "No columns were extracted for '{0}' from tables '{1}'.";

		/// <summary>
		/// Create exception
		/// </summary>
		public NoColumnsExtractedException() { }

		/// <summary>
		/// Create exception
		/// </summary>
		/// <param name="model">Model type</param>
		/// <param name="tables">Tables for extraction</param>
		public NoColumnsExtractedException(Type model, Table[] tables) : this(string.Format(Format, model, string.Join(", ", tables.ToList()))) { }

		/// <summary>
		/// Create exception
		/// </summary>
		/// <param name="message">Message</param>
		public NoColumnsExtractedException(string message) : base(message) { }

		/// <summary>
		/// Create exception
		/// </summary>
		/// <param name="message">Message</param>
		/// <param name="inner">Exception</param>
		public NoColumnsExtractedException(string message, Exception inner) : base(message, inner) { }
	}
}
