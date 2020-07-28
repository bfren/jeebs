﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Jeebs.Reflection
{
	/// <summary>
	/// Object Extensions
	/// </summary>
	public static class ObjectExtensions
	{
		/// <summary>
		/// Get array of object properties
		/// </summary>
		/// <param name="this">Object</param>
		/// <returns>Array of properties</returns>
		public static PropertyInfo[] GetProperties(this object @this)
			=> @this.GetType().GetTypeInfo().GetProperties();

		/// <summary>
		/// Return the value of a property dynamically - i.e. by property name
		/// </summary>
		/// <param name="this">Object</param>
		/// <param name="propertyName">The name of the property whose value you want to return</param>
		/// <returns>The value of the property, or an empty string if the property does not exist</returns>
		public static object GetProperty(this object @this, string propertyName)
		{
			TypeInfo type = @this.GetType().GetTypeInfo();
			if (!type.DeclaredProperties.Any(x => x.Name == propertyName))
			{
				throw new KeyNotFoundException($"Property {propertyName} cannot be found in type {type.FullName}");
			}

			PropertyInfo info = type.GetProperty(propertyName);
			return info.GetValue(@this, null);
		}

		/// <summary>
		/// Return the value of a property dynamically - i.e. by property name
		/// </summary>
		/// <typeparam name="T">Property type</typeparam>
		/// <param name="this">Object</param>
		/// <param name="propertyName">The name of the property whose value you want to return</param>
		/// <returns>The value of the property, or an empty string if the property does not exist</returns>
		public static T GetProperty<T>(this object @this, string propertyName)
		{
			TypeInfo type = @this.GetType().GetTypeInfo();
			if (!type.DeclaredProperties.Any(x => x.Name == propertyName))
			{
				throw new KeyNotFoundException($"Property {propertyName} cannot be found in type {type.FullName}");
			}

			PropertyInfo info = type.GetProperty(propertyName);
			if (typeof(T) != info.PropertyType)
			{
				throw new InvalidOperationException($"Type parameter {typeof(T)} does not match type of property {propertyName}: {info.GetType()}.");
			}

			return (T)info.GetValue(@this, null);
		}
	}
}
