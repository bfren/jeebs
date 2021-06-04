// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System;

namespace F.MvcF
{
	/// <summary>
	/// Calendar functions
	/// </summary>
	public static class CalendarF
	{
		internal static int EventCounter { get; set; }

		/// <summary>
		/// Generate Event UID
		/// </summary>
		/// <param name="lastModified">Calendar Last Modified</param>
		/// <param name="domain">Calendar / app Domain</param>
		public static string GenerateEventUid(DateTime lastModified, string domain) =>
			@$"{lastModified:yyyyMMdd\THHmmss}-{EventCounter++:000000}@{domain}";
	}
}
