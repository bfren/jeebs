// Jeebs Rapid Application Development
// Copyright (c) bfren.uk - licensed under https://mit.bfren.uk/2013

namespace Jeebs.Auth.Data
{
	/// <summary>
	/// User ID
	/// </summary>
	/// <param name="Value">ID Value</param>
	public readonly record struct AuthUserId(ulong Value) : IStrongId;
}
