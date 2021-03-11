// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System;
using System.IO;

namespace Jeebs
{
	/// <summary>
	/// Create <see cref="Option{T}"/> types and begin chains
	/// </summary>
	public static partial class Option
	{
		/// <summary>
		/// Exception handler delegate - takes exception and returns message of type <see cref="IExceptionMsg"/>
		/// </summary>
		/// <param name="e">Exception</param>
		public delegate IExceptionMsg Handler(Exception e);

		/// <summary>
		/// Special case for boolean - returns Some{bool}(true)
		/// </summary>
		public static Option<bool> True =>
			Wrap(true);

		/// <summary>
		/// Special case for boolean - returns Some{bool}(false)
		/// </summary>
		public static Option<bool> False =>
			Wrap(false);

		/// <summary>
		/// Set to log audit exceptions - otherwise they are sent to the Console
		/// </summary>
		public static Action<Exception>? LogAuditExceptions { get; set; }

		internal static void HandleAuditException(Exception e, Action<Exception>? log, TextWriter writer)
		{
			if (log is not null)
			{
				log(e);
			}
			else
			{
				writer.WriteLine("Audit Error: {0}", e);
			}
		}

		internal static void HandleAuditException(Exception e) =>
			HandleAuditException(e, LogAuditExceptions, Console.Out);
	}
}
