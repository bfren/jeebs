// Jeebs Test Applications
// Copyright (c) bfren.uk - licensed under https://mit.bfren.uk/2013

using Jeebs.WordPress.Data;

namespace AppConsoleWp.Bcg
{
	/// <summary>
	/// Bible Passage
	/// </summary>
	public sealed record PassageCustomField : TextCustomField
	{
		/// <summary>
		/// This is a required field
		/// </summary>
		public PassageCustomField() : base("passage") { }
	}
}
