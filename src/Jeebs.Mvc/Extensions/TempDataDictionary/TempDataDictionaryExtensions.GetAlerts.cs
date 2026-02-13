// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System.Collections.Generic;
using Jeebs.Functions;
using Jeebs.Mvc.Models;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace Jeebs.Mvc;

public static partial class TempDataDictionaryExtensions
{
	/// <summary>
	/// Get all alerts.
	/// </summary>
	/// <param name="this">ITempDataDictionary.</param>
	public static List<Alert> GetAlerts(this ITempDataDictionary @this) =>
		@this.TryGetValue(AlertsKey, out var value) switch
		{
			true when value is string alerts =>
				JsonF.Deserialise<List<Alert>>(alerts).Unwrap(_ => []),

			_ =>
				[]
		};
}
