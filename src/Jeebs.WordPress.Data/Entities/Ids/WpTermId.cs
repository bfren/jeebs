// Jeebs Rapid Application Development
// Copyright (c) bfren.uk - licensed under https://mit.bfren.uk/2013

namespace Jeebs.WordPress.Data.Entities
{
	/// <summary>
	/// WordPress Term ID
	/// </summary>
	/// <param name="Value">ID Value</param>
	public sealed record WpTermId(long Value) : StrongId(Value)
	{
		/// <summary>
		/// Create with default value
		/// </summary>
		public WpTermId() : this(0) { }
	}
}
