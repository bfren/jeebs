// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;
using System.Reflection;

namespace Jeebs.Reflection;

public static partial class ObjectExtensions
{
	/// <summary>
	/// Get array of object properties.
	/// </summary>
	/// <param name="this">Object.</param>
	/// <returns>Array of properties.</returns>
	public static PropertyInfo[] GetProperties(this object @this) =>
		@this switch
		{
			Type t =>
				t.GetProperties(),

			_ =>
				@this.GetType().GetProperties()
		};
}
