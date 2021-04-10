// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

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
