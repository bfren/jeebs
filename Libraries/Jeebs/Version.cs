﻿using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Jeebs
{
	/// <summary>
	/// Version
	/// </summary>
	/// <typeparam name="T">Class contained in assembly to get the version for</typeparam>
	public static class Version<T>
	{
		/// <summary>
		/// Get the version of the assembly the calling type belongs to
		/// </summary>
		public static Version V { get => version.Value; }

		/// <summary>
		/// Lazy property to avoid multiple reflection calls
		/// </summary>
		private static readonly Lazy<Version> version = new Lazy<Version>(() => typeof(T).GetTypeInfo().Assembly.GetName().Version);

		/// <summary>
		/// Return a short version string (e.g. v1.1)
		/// </summary>
		public static string Short
		{
			get => $"v{V.Major}.{V.Minor}";
		}

		/// <summary>
		/// Return a build version string (e.g. v1.1.45)
		/// </summary>
		public static string Build
		{
			get => $"v{V.Major}.{V.Minor}.{V.Build}";
		}

		/// <summary>
		/// Return a full version string (e.g. v1.1.45.809)
		/// </summary>
		public static string Full
		{
			get => $"v{V.Major}.{V.Minor}.{V.Build}.{V.Revision}";
		}
	}
}
