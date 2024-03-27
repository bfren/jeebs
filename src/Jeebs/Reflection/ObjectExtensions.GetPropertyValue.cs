// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System.Reflection;

namespace Jeebs.Reflection;

public static partial class ObjectExtensions
{
	/// <summary>
	/// Return the value of a property dynamically - i.e. by property name.
	/// </summary>
	/// <param name="this">Object.</param>
	/// <param name="propertyName">The name of the property whose value you want to return.</param>
	/// <returns>Property value.</returns>
	public static Maybe<object> GetPropertyValue(this object @this, string propertyName) =>
		GetPropertyValue<object>(@this, propertyName);

	/// <summary>
	/// Return the value of a property dynamically - i.e. by property name.
	/// </summary>
	/// <typeparam name="T">Property type.</typeparam>
	/// <param name="this">Object.</param>
	/// <param name="propertyName">The name of the property whose value you want to return.</param>
	/// <returns>Property value.</returns>
	public static Maybe<T> GetPropertyValue<T>(this object @this, string propertyName)
	{
		// Get type
		var type = @this.GetType();

		// Get the property info
		if (type.GetProperty(propertyName) is PropertyInfo info)
		{
			// If the types don't match, return None
			if (typeof(T) != typeof(object) && typeof(T) != info.PropertyType)
			{
				return M.None;
			}

			// Get the value - if it's null return None
			return info.GetValue(@this, null) switch
			{
				T val =>
					val,

				_ =>
					M.None
			};
		}

		// No property with this name was found
		return M.None;
	}
}
