// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;

namespace Jeebs.Reflection;

/// <summary>
/// Dynamically gets and sets property values on an object.
/// </summary>
/// <typeparam name="TObject">Object type.</typeparam>
/// <typeparam name="TProperty">Property type.</typeparam>
public class PropertyInfo<TObject, TProperty>
{
	/// <summary>
	/// PropertyInfo object.
	/// </summary>
	public PropertyInfo Info { get; private init; }

	/// <summary>
	/// Return the property name.
	/// </summary>
	public string Name =>
		Info.Name;

	/// <summary>
	/// Create object.
	/// </summary>
	/// <param name="info">PropertyInfo object.</param>
	public PropertyInfo(PropertyInfo info) =>
		Info = info;

	/// <summary>
	/// Create object.
	/// </summary>
	/// <param name="propertyName">Property name.</param>
	public PropertyInfo(string propertyName)
	{
		if (typeof(TObject).GetProperty(propertyName) is PropertyInfo info)
		{
			if (typeof(TProperty).Equals(info.PropertyType))
			{
				Info = info;
			}
			else
			{
				throw new InvalidOperationException($"The property '{propertyName}' is not of type {typeof(TProperty)}.");
			}
		}
		else
		{
			throw new InvalidOperationException($"'{propertyName}' is not a valid property of {typeof(TObject)}.");
		}
	}

	/// <summary>
	/// Get the value of the property from the specified object.
	/// </summary>
	/// <param name="obj">Object.</param>
	/// <returns>Property value.</returns>
	public Maybe<TProperty> Get([DisallowNull] TObject obj)
	{
		if (Info.GetValue(obj, null) is TProperty value)
		{
			return value;
		}

		return M.None;
	}

	/// <summary>
	/// Set the value of the property on the specified object.
	/// </summary>
	/// <param name="obj">Object.</param>
	/// <param name="value">Value.</param>
	public void Set([DisallowNull] TObject obj, [DisallowNull] TProperty value) =>
		Info.SetValue(obj, value);
}
