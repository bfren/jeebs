// Jeebs Rapid Application Development
// Copyright (c) bfren.uk - licensed under https://mit.bfren.uk/2013

namespace Jeebs.Mvc.Auth.Models
{
	/// <summary>
	/// Access Denied Model
	/// </summary>
	/// <param name="AccessUrl">The URL the user was trying to access</param>
	public sealed record class DeniedModel(string? AccessUrl);
}
