// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

namespace Jeebs.Data.Entities
{
	/// <summary>
	/// Mark property as Id
	/// </summary>
	[AttributeUsage(AttributeTargets.Property | AttributeTargets.Parameter, Inherited = true, AllowMultiple = false)]
	public sealed class IdAttribute : Attribute { }
}
