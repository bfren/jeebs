// Jeebs Rapid Application Development
// Copyright (c) bfren.uk - licensed under https://mit.bfren.uk/2013

using System;

namespace Jeebs.Data.Entities
{
	/// <summary>
	/// Mark property as Computed
	/// </summary>
	[AttributeUsage(AttributeTargets.Property, Inherited = true, AllowMultiple = false)]
	public sealed class ComputedAttribute : Attribute { }
}
