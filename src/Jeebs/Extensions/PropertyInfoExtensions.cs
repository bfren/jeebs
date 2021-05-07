// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System;
using System.Linq;
using System.Reflection;

namespace System.Runtime.CompilerServices
{
	/// <summary>
	/// Provide NullatbleAttribute so the compiler doesn't have to, and we can access it using
	/// <see cref="Jeebs.PropertyInfoExtensions.IsNullable(PropertyInfo)"/>
	/// </summary>
	/// <remarks>
	/// See https://github.com/dotnet/runtime/issues/29039#issuecomment-498481064
	/// </remarks>
	[AttributeUsage(
		AttributeTargets.Class |
		AttributeTargets.GenericParameter |
		AttributeTargets.Event | AttributeTargets.Field | AttributeTargets.Property |
		AttributeTargets.Parameter | AttributeTargets.ReturnValue,
		AllowMultiple = false)]
	public sealed class NullableAttribute : Attribute
	{
		/// <summary>
		/// Nullable Flags
		/// </summary>
		public readonly byte[] NullableFlags;

		/// <summary>
		/// Create object
		/// </summary>
		/// <param name="b">Type info</param>
		public NullableAttribute(byte b) =>
			NullableFlags = new[] { b };

		/// <summary>
		/// Create object
		/// </summary>
		/// <param name="b">Type info</param>
		public NullableAttribute(byte[] b) =>
			NullableFlags = b;
	}
}

namespace Jeebs
{
	/// <summary>
	/// PropertyInfo Extensions
	/// </summary>
	public static class PropertyInfoExtensions
	{
		/// <summary>
		/// Returns true if the property has been marked as nullable using the C# 8+ nullable feature
		/// </summary>
		/// <param name="this">PropertyInfo</param>
		public static bool IsNullable(this PropertyInfo @this) =>
			(@this.PropertyType.GenericTypeArguments.Length == 1 && @this.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>))
			|| @this.CustomAttributes.Any(a => a.AttributeType == typeof(System.Runtime.CompilerServices.NullableAttribute));
	}
}
