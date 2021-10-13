// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;

namespace F;

/// <summary>
/// Calendar functions
/// </summary>
public static class CalendarF
{
	/// <summary>
	/// Generate Event UID
	/// </summary>
	/// <param name="counter">Event counter - should be increased each time</param>
	/// <param name="lastModified">Calendar Last Modified</param>
	public static string GenerateEventUid(int counter, DateTime lastModified) =>
		@$"{lastModified:yyyyMMdd\THHmmss}-{counter:000000}";

	/// <summary>
	/// Generate Event UID with additional domain
	/// </summary>
	/// <param name="counter">Event counter - should be increased each time</param>
	/// <param name="lastModified">Calendar Last Modified</param>
	/// <param name="domain">Calendar / app Domain</param>
	public static string GenerateEventUid(int counter, DateTime lastModified, string domain) =>
		$"{GenerateEventUid(counter, lastModified)}@{domain}";
}
