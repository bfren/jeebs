// Jeebs Test Applications
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.WordPress.Data;

namespace AppConsoleWp.Bcg
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
