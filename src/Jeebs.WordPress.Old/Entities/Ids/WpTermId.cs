﻿// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

namespace Jeebs.WordPress.Entities
{
	/// <summary>
	/// WordPress Term ID
	/// </summary>
	/// <param name="Value">ID Value</param>
	public sealed record WpTermId(long Value) : Id.LongId(Value)
	{
		/// <summary>
		/// Create with default value
		/// </summary>
		public WpTermId() : this(Default) { }
	}
}