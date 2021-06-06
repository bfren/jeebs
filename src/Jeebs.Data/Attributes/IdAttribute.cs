// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.uk/2013

using System;

namespace Jeebs.Data.Entities
{
	/// <summary>
	/// Mark property as Id
	/// </summary>
	[AttributeUsage(AttributeTargets.Property, Inherited = true, AllowMultiple = false)]
	public sealed class IdAttribute : Attribute { }
}
