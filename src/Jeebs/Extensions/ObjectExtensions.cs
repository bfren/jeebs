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
				return None<object>(new Msg.TypeDoesNotContainPropertyMsg((@this.GetType(), propertyName)));
			}

			return type.GetProperty(propertyName)?.GetValue(@this, null) switch
			{
				object val =>
					val,

				_ =>
					None<object>(new Msg.NullPropertyOrValueMsg((@this.GetType(), propertyName)))
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
				return None<T>(new Msg.TypeDoesNotContainPropertyMsg((@this.GetType(), propertyName)));
			}

			if (type.GetProperty(propertyName) is PropertyInfo info)
			{
				if (typeof(T) != info.PropertyType)
				{
					return None<T>(new Msg.UnexpectedPropertyTypeMsg<T>((@this.GetType(), propertyName)));
				}

				return info.GetValue(@this, null) switch
				{
					T val =>
						val,

					_ =>
						None<T>(new Msg.NullValueMsg<T>((@this.GetType(), propertyName)))
				};
			}

			return None<T>(new Msg.PropertyNotFoundMsg((@this.GetType(), propertyName)));
		}

		/// <summary>Messages</summary>
		public static class Msg
		{
			/// <summary>See <see cref="GetProperty{T}(object, string)"/> and <see cref="GetProperty(object, string)"/></summary>
			/// <param name="Value">Object type and Property name</param>
			public abstract record GetPropertyMsg((Type type, string property) Value) : WithValueMsg<(Type type, string property)>() { }

			/// <summary>The property could not be found</summary>
			/// <inheritdoc cref="GetPropertyMsg"/>
			public sealed record PropertyNotFoundMsg((Type, string) Value) : GetPropertyMsg(Value) { }

			/// <summary>The property or value is null</summary>
			/// <inheritdoc cref="GetPropertyMsg"/>
			public sealed record NullPropertyOrValueMsg((Type, string) Value) : GetPropertyMsg(Value) { }

			/// <summary>The property value is null</summary>
			/// <inheritdoc cref="GetPropertyMsg"/>
			public sealed record NullValueMsg<T>((Type, string) Value) : GetPropertyMsg(Value) { }

			/// <summary>The object does not contain the property</summary>
			/// <inheritdoc cref="GetPropertyMsg"/>
			public sealed record TypeDoesNotContainPropertyMsg((Type type, string property) Value) : GetPropertyMsg(Value) { }

			/// <summary>The property type doesn't match the requested type</summary>
			/// <typeparam name="T">Requested property value type</typeparam>
			/// <inheritdoc cref="GetPropertyMsg"/>
			public sealed record UnexpectedPropertyTypeMsg<T>((Type type, string property) Value) : GetPropertyMsg(Value) { }
		}
	}
}
