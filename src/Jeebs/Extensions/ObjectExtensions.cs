// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System;
using System.Linq;
using System.Reflection;
using static F.OptionF;

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
		public static PropertyInfo[] GetProperties(this object @this) =>
			@this switch
			{
				Type t =>
					t.GetProperties(),

				_ =>
					@this.GetType().GetProperties()
			};

		/// <summary>
		/// Return whether or not the object contains the specified property
		/// </summary>
		/// <param name="this">Object</param>
		/// <param name="propertyName">The name of the property whose value you want to return</param>
		public static bool HasProperty(this object @this, string propertyName) =>
			GetProperties(@this).Any(x => x.Name == propertyName);

		/// <summary>
		/// Return the value of a property dynamically - i.e. by property name
		/// </summary>
		/// <param name="this">Object</param>
		/// <param name="propertyName">The name of the property whose value you want to return</param>
		public static Option<object> GetPropertyValue(this object @this, string propertyName) =>
			GetPropertyValue<object>(@this, propertyName);

		/// <summary>
		/// Return the value of a property dynamically - i.e. by property name
		/// </summary>
		/// <typeparam name="T">Property type</typeparam>
		/// <param name="this">Object</param>
		/// <param name="propertyName">The name of the property whose value you want to return</param>
		public static Option<T> GetPropertyValue<T>(this object @this, string propertyName)
		{
			// Get type
			var type = @this.GetType();

			// Get the property info
			if (type.GetProperty(propertyName) is PropertyInfo info)
			{
				// If the types don't match, return None
				if (typeof(T) != typeof(object) && typeof(T) != info.PropertyType)
				{
					return None<T>(new Msg.UnexpectedPropertyTypeMsg<T>((type, propertyName)));
				}

				// Get the value - if it's null return None
				return info.GetValue(@this, null) switch
				{
					T val =>
						val,

					_ =>
						None<T>(new Msg.NullValueMsg<T>((type, propertyName)))
				};
			}

			// No property with this name was found
			return None<T>(new Msg.PropertyNotFoundMsg((type, propertyName)));
		}

		/// <summary>Messages</summary>
		public static class Msg
		{
			/// <summary>See <see cref="GetPropertyValue{T}(object, string)"/> and <see cref="GetPropertyValue(object, string)"/></summary>
			/// <param name="Value">Object type and Property name</param>
			public abstract record GetPropertyMsg((Type type, string property) Value) : WithValueMsg<(Type type, string property)>() { }

			/// <summary>The property could not be found</summary>
			/// <inheritdoc cref="GetPropertyMsg"/>
			public sealed record PropertyNotFoundMsg((Type, string) Value) : GetPropertyMsg(Value) { }

			/// <summary>The property value is null</summary>
			/// <inheritdoc cref="GetPropertyMsg"/>
			public sealed record NullValueMsg<T>((Type, string) Value) : GetPropertyMsg(Value) { }

			/// <summary>The property type doesn't match the requested type</summary>
			/// <typeparam name="T">Requested property value type</typeparam>
			/// <inheritdoc cref="GetPropertyMsg"/>
			public sealed record UnexpectedPropertyTypeMsg<T>((Type type, string property) Value) : GetPropertyMsg(Value) { }
		}
	}
}
