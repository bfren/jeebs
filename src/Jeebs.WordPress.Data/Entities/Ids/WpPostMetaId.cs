// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.uk/2013

namespace Jeebs.WordPress.Data.Entities
{
	/// <summary>
	/// WordPress Post Meta ID
	/// </summary>
	/// <param name="Value">ID Value</param>
	public sealed record WpPostMetaId(long Value) : StrongId(Value)
	{
		/// <summary>
		/// Create with default value
		/// </summary>
		public WpPostMetaId() : this(0) { }
	}
}
