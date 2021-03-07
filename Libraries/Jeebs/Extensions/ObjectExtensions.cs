// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System.Linq;
using System.Reflection;
using Jm.Extensions.ObjectExtensions;

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
		public static PropertyInfo[] GetProperties(this object @this) =>
			@this.GetType().GetTypeInfo().GetProperties();

		/// <summary>
		/// Return whether or not the object contains the specified property
		/// </summary>
		/// <param name="this">Object</param>
		/// <param name="propertyName">The name of the property whose value you want to return</param>
		public static bool HasProperty(this object @this, string propertyName)
		{
			TypeInfo type = @this.GetType().GetTypeInfo();
			return type.DeclaredProperties.Any(x => x.Name == propertyName);
		}

		/// <summary>
		/// Return the value of a property dynamically - i.e. by property name
		/// </summary>
		/// <param name="this">Object</param>
		/// <param name="propertyName">The name of the property whose value you want to return</param>
		/// <returns>The value of the property, or an empty string if the property does not exist</returns>
		public static Option<object> GetProperty(this object @this, string propertyName)
		{
			TypeInfo type = @this.GetType().GetTypeInfo();
			if (!HasProperty(@this, propertyName))
			{
				return Option.None<object>().AddReason(new TypeDoesNotContainPropertyMsg(@this.GetType(), propertyName));
			}

			return type.GetProperty(propertyName)?.GetValue(@this, null) switch
			{
				object val =>
					val,

				_ =>
					Option.None<object>().AddReason(new NullPropertyOrValueMsg(@this.GetType(), propertyName))
			};
		}

		/// <summary>
		/// Return the value of a property dynamically - i.e. by property name
		/// </summary>
		/// <typeparam name="T">Property type</typeparam>
		/// <param name="this">Object</param>
		/// <param name="propertyName">The name of the property whose value you want to return</param>
		/// <returns>The value of the property, or an empty string if the property does not exist</returns>
		public static Option<T> GetProperty<T>(this object @this, string propertyName)
		{
			TypeInfo type = @this.GetType().GetTypeInfo();
			if (!type.DeclaredProperties.Any(x => x.Name == propertyName))
			{
				return Option.None<T>().AddReason(new TypeDoesNotContainPropertyMsg(@this.GetType(), propertyName));
			}

			if (type.GetProperty(propertyName) is PropertyInfo info)
			{
				if (typeof(T) != info.PropertyType)
				{
					return Option.None<T>().AddReason(new UnexpectedPropertyTypeMsg(@this.GetType(), propertyName, typeof(T)));
				}

				return info.GetValue(@this, null) switch
				{
					T val =>
						val,

					_ =>
						Option.None<T>().AddReason(new NullValueMsg(@this.GetType(), propertyName))
				};
			}

			return Option.None<T>().AddReason(new NullPropertyMsg(@this.GetType(), propertyName));
		}
	}
}
