// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace Jeebs.Mvc;

public static partial class TempDataDictionaryExtensions
{
	/// <summary>
	/// Returns whether or not the TempDataDictionary has any pending alerts.
	/// </summary>
	/// <param name="this">ITempDataDictionary.</param>
	public static bool HasAlerts(this ITempDataDictionary @this) =>
		@this.Peek(AlertsKey) != null;
}
