// Jeebs Rapid Application Development
// Copyright (c) bfren.uk - licensed under https://mit.bfren.uk/2013

using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Jeebs.Reflection;

namespace Jeebs
{
	public static partial class StringExtensions
	{
		/// <summary>
		/// Works like string.Format() but with named as well as numbered placeholders
		/// <para>Source is Array: values will be inserted in order (regardless of placeholder values)</para>
		/// <para>Source is Object: property names must match placeholders or they will be left in place</para>
		/// <para>Inspired by http://james.newtonking.com/archive/2008/03/29/formatwith-2-0-string-formatting-with-named-variables</para>
		/// <para>(Significantly) altered to work without requiring DataBinder</para>
		/// </summary>
		/// <param name="this">String to format</param>
		/// <param name="source"></param>
		public static string FormatWith<T>(this string @this, T source) =>
			Modify(@this, () =>
			{
				// Return original if source is null or if it is an empty array
				if (source is null)
				{
					return @this;
				}
				else if (source is Array arr && arr.Length == 0)
				{
					return @this;
				}

				// Thanks James Newton-King!
				var r = new Regex(
					@"(?<start>\{)+(?<template>[\w\.\[\]@]+)(?<format>:[^}]+)?(?<end>\})+",
					RegexOptions.CultureInvariant | RegexOptions.IgnoreCase
				);

				var values = new List<object>();
				int replaceIndex = 0; // keeps track of replace loop so we can match named template values with an array source
				string rewrittenFormat = r.Replace(@this, (Match m) =>
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
					var value = source switch
					{
						// Source array - get next item in array
						Array arr when replaceIndex < arr.Length && arr.GetValue(replaceIndex++) is object val =>
							val,

						// Source object - get matching property value
						{ } obj when obj.GetPropertyValue(template) is Some<object> property =>
							property.Value,

						// Nothing has matched yet so to be safe put the template back
						_ =>
							$"{{{template}}}"
					};

					values.Add(value);

					// Recreate format using zero-based string
					return
						new string('{', startGroup.Captures.Count)
						+ (values.Count - 1)
						+ formatGroup.Value
						+ new string('}', endGroup.Captures.Count);
				});

				return string.Format(rewrittenFormat, values.ToArray());
			});
	}
}
