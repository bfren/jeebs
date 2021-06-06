// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.uk/2013

namespace Jeebs.WordPress.Data.Entities
{
	/// <summary>
	/// WordPress Term Relationship ID
	/// </summary>
	/// <param name="Value">ID Value</param>
	public sealed record WpTermRelationshipId(long Value) : StrongId(Value)
	{
		/// <summary>
		/// Create with default value
		/// </summary>
		public WpTermRelationshipId() : this(0) { }
	}
}
