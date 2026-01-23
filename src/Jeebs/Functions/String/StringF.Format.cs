// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using System.Text.RegularExpressions;

namespace Jeebs.Functions;

public static partial class StringF
{
	/// <summary>
	/// If <paramref name="obj"/> is not null, use string.Format() -
	/// otherwise, return <paramref name="formatString"/>.
	/// </summary>
	/// <typeparam name="T">Object type.</typeparam>
	/// <param name="formatString">Format string.</param>
	/// <param name="obj">Object (nullable).</param>
	/// <returns>Formatted string.</returns>
	public static string Format<T>(string formatString, T obj, string? ifNull) =>
		obj switch
		{
			T x =>
				string.Format(CultureInfo.InvariantCulture, formatString, x),

			_ =>
				ifNull ?? formatString
		};

	/// <summary>
	/// Works like string.Format() but with named as well as numbered placeholders.
	/// <para>Source is Array: values will be inserted in order (regardless of placeholder values).</para>
	/// <para>Source is Object: property names must match placeholders or they will be left in place.</para>
	/// </summary>
	/// <remarks>
	/// Inspired by http://james.newtonking.com/archive/2008/03/29/formatwith-2-0-string-formatting-with-named-variables,
	/// (significantly) altered to work without requiring DataBinder.
	/// </remarks>
	/// <typeparam name="T">Source type.</typeparam>
	/// <param name="formatString">String to format.</param>
	/// <param name="source">Source object to use for template values.</param>
	/// <returns>Formatted string.</returns>
	public static string Format<T>(string formatString, T source)
	{
		// Return original if source is null or if it is an empty array
		if (source is null)
		{
			return formatString;
		}
		else if (source is Array arr && arr.Length == 0)
		{
			return formatString;
		}

		// Thanks James Newton-King!
		var r = TemplateMatcherRegex();

		var values = new List<object>();
		var replaceIndex = 0; // keeps track of replace loop so we can match named template values with an array source
		var rewrittenFormat = r.Replace(formatString, (m) =>
		{
			var startGroup = m.Groups["start"];
			var templateGroup = m.Groups["template"];
			var formatGroup = m.Groups["format"];
			var endGroup = m.Groups["end"];

			// This is the value inside the braces, e.g. "0" in "{0}" or "A" in "{A}"
			// Remove any @ symbols from the start - used by Serilog to denote an object format
			// but breaks the following
			var template = templateGroup.Value.TrimStart('@');

			// Switch on the source type, using variety of methods to get this template's value
			var flags = BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance;
			var value = source switch
			{
				// Source array - get next item in array
				Array arr when replaceIndex < arr.Length && arr.GetValue(replaceIndex++) is object val =>
					val,

				// Source object - get matching property value
				{ } obj when typeof(T).GetProperty(template, flags)?.GetValue(obj) is object val =>
					val,

				// Nothing has matched yet so to be safe put the template back
				_ =>
					$"{{{template}}}"
			};

			values.Add(value);

			// Recreate format using zero-based string
			return new string('{', startGroup.Captures.Count)
				+ (values.Count - 1)
				+ formatGroup.Value
				+ new string('}', endGroup.Captures.Count);
		});

		return string.Format(CultureInfo.InvariantCulture, rewrittenFormat, [.. values]);
	}

	[GeneratedRegex("(?<start>\\{)+(?<template>[\\w\\.\\[\\]@]+)(?<format>:[^}]+)?(?<end>\\})+", RegexOptions.IgnoreCase | RegexOptions.CultureInvariant)]
	private static partial Regex TemplateMatcherRegex();
}
