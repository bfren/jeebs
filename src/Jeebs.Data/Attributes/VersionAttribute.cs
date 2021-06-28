// Jeebs Rapid Application Development
// Copyright (c) bfren.uk - licensed under https://mit.bfren.uk/2013

using System;

namespace Jeebs.Data.Entities
{
	/// <summary>
	/// Mark property as Version
	/// </summary>
	[AttributeUsage(AttributeTargets.Property | AttributeTargets.Parameter, Inherited = true, AllowMultiple = false)]
	public sealed class VersionAttribute : Attribute { }
}
