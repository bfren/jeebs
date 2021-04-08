// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

namespace Jeebs.WordPress.Entities
{
	/// <summary>
	/// WordPress Term Taxonomy ID
	/// </summary>
	/// <param name="Value">ID Value</param>
	public sealed record WpTermTaxonomyId(long Value) : StrongId(Value)
	{
		/// <summary>
		/// Create with default value
		/// </summary>
		public WpTermTaxonomyId() : this(0) { }
	}
}
