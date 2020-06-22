using System;
using System.Collections.Generic;
using System.Text;
using Jeebs;

namespace Jm
{
	/// <summary>
	/// Audit Async exception handling message
	/// </summary>
	public class AuditAsyncException : Exception
	{
		/// <summary>
		/// Create object
		/// </summary>
		/// <param name="ex">Exception</param>
		public AuditAsyncException(System.Exception ex) : base(ex) { }
	}
}
