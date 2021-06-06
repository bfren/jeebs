// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.uk/2013

namespace Jeebs.WordPress.Data.Entities
{
	/// <summary>
	/// WordPress Comment Meta ID
	/// </summary>
	/// <param name="Value">ID Value</param>
	public sealed record WpCommentMetaId(long Value) : StrongId(Value)
	{
		/// <summary>
		/// Create with default value
		/// </summary>
		public WpCommentMetaId() : this(0) { }
	}
}
