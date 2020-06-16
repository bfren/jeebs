using System;
using System.Collections.Generic;
using System.Text;
using Jeebs;

namespace Jm
{
	/// <summary>
	/// Audit exception handling message
	/// </summary>
	public class AuditException : Exception
	{
		/// <summary>
		/// Create object
		/// </summary>
		/// <param name="ex">Exception</param>
		public AuditException(System.Exception ex) : base(ex) { }
	}
}
