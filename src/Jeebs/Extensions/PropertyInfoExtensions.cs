// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

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
		AttributeTargets.Event |
		AttributeTargets.Field |
		AttributeTargets.GenericParameter |
		AttributeTargets.Parameter |
		AttributeTargets.Property |
		AttributeTargets.ReturnValue,
		AllowMultiple = false,
		Inherited = false)]
	public sealed class NullableAttribute : Attribute
	{
		/// <summary>
		/// Nullable Flags
		/// </summary>
		public readonly byte[] NullableFlags;

		/// <summary>
		/// Create object
		/// </summary>
		/// <param name="flag">Type info</param>
		public NullableAttribute(byte flag) =>
			NullableFlags = new[] { flag };

		/// <summary>
		/// Create object
		/// </summary>
		/// <param name="flags">Type info</param>
		public NullableAttribute(byte[] flags) =>
			NullableFlags = flags;
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
