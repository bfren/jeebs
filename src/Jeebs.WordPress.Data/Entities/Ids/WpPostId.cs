// Jeebs Rapid Application Development
// Copyright (c) bfren.uk - licensed under https://mit.bfren.uk/2013

namespace Jeebs.WordPress.Data.Entities
{
	/// <summary>
	/// WordPress Post ID
	/// </summary>
	/// <param name="Value">ID Value</param>
	public sealed record WpPostId(long Value) : StrongId(Value)
	{
		/// <summary>
		/// Create with default value
		/// </summary>
		public WpPostId() : this(0) { }
	}
}
