using System;
using System.Collections.Generic;
using System.Text;

namespace Jeebs
{
	public static class TypeExtensions
	{
		public static bool InheritsFrom<T>(this Type @this)
			=> @this.InheritsFrom(typeof(T));

		public static bool InheritsFrom(this Type @this, Type type)
		{
			// Handle base object
			if (@this == typeof(object))
			{
				return false;
			}

			// Handle value types
			if (@this.IsValueType)
			{
				return false;
			}

			// Handle interfaces
			if (type.IsInterface)
			{
				return @this.ImplementsInterface(type);
			}

			// Handle generic types
			if (type.IsGenericType)
			{
				return @this.ImplementsGenericType(type);
			}

			// Handle basic inheritance
			return @this.IsSubclassOf(type);
		}

		internal static bool ImplementsInterface(this Type @this, Type @interface)
		{
			// Handle basic implementation
			if (@interface.IsAssignableFrom(@this))
			{
				return true;
			}

			// Handle interfaces with generic types
			foreach (var item in @this.GetInterfaces())
			{
				if (@interface.IsGenericTypeDefinition && @this.ImplementsGenericType(item))
				{
					return true;
				}
			}

			return false;
		}

		internal static bool ImplementsGenericType(this Type @this, Type generic)
		{
			return @this.GetGenericTypeDefinition() == generic
				|| @this.BaseType.ImplementsGenericType(generic);
		}
	}
}
