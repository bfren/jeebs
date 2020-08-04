using System;
using System.Collections.Generic;
using System.Text;
using Jeebs.WordPress;

namespace AppConsoleWordPress.Bcg
{
	/// <summary>
	/// The place a sermon was first preached
	/// </summary>
	public sealed class FirstPreachedCustomField : TermCustomField
	{
		/// <summary>
		/// This is a required field
		/// </summary>
		public FirstPreachedCustomField() : base("first_preached") { }
	}
}
