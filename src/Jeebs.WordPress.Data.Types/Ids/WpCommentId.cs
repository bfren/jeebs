// Jeebs Rapid Application Development
// Copyright (c) bfren.uk - licensed under https://mit.bfren.uk/2013

namespace Jeebs.WordPress.Data.Entities
{
	/// <summary>
	/// WordPress Comment ID
	/// </summary>
	/// <param name="Value">ID Value</param>
	public readonly record struct WpCommentId(ulong Value) : IStrongId;
}
