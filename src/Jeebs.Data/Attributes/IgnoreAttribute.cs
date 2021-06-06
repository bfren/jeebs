// Jeebs Rapid Application Development
// Copyright (c) bfren.uk - licensed under https://mit.bfren.uk/2013

using System;

namespace Jeebs.Data.Entities
{
	/// <summary>
	/// Mark property as to be ignored when mapping entity properties to table columns
	/// </summary>
	[AttributeUsage(AttributeTargets.Property, Inherited = true, AllowMultiple = false)]
	public sealed class IgnoreAttribute : Attribute { }
}
