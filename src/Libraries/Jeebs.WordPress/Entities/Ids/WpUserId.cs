﻿// Jeebs Rapid Application Development
// Copyright (c) bfren.uk - licensed under https://mit.bfren.uk/2013

namespace Jeebs.WordPress.Entities
{
	/// <summary>
	/// WordPress User ID
	/// </summary>
	/// <param name="Value">ID Value</param>
	public sealed record WpUserId(long Value) : Id.LongId(Value)
	{
		/// <summary>
		/// Create with default value
		/// </summary>
		public WpUserId() : this(Default) { }
	}
}
