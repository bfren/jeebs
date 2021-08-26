// Jeebs Rapid Application Development
// Copyright (c) bfren.uk - licensed under https://mit.bfren.uk/2013

namespace Jeebs.WordPress.Data.Entities
{
	/// <summary>
	/// WordPress Link ID
	/// </summary>
	/// <param name="Value">ID Value</param>
	public readonly record struct WpLinkId(ulong Value) : IStrongId;
}
