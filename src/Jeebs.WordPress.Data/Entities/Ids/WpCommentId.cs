// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.uk/2013

namespace Jeebs.WordPress.Data.Entities
{
	/// <summary>
	/// WordPress Comment ID
	/// </summary>
	/// <param name="Value">ID Value</param>
	public sealed record WpCommentId(long Value) : StrongId(Value)
	{
		/// <summary>
		/// Create with default value
		/// </summary>
		public WpCommentId() : this(0) { }
	}
}
