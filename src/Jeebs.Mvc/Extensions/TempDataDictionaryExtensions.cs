// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Functions;
using Jeebs.Mvc.Enums;
using Jeebs.Mvc.Models;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace Jeebs.Mvc;

/// <summary>
/// <see cref="ITempDataDictionary"/> extension methods.
/// </summary>
public static partial class TempDataDictionaryExtensions
{
	internal const string AlertsKey = "JeebsAlerts";

	/// <summary>
	/// Add alert to TempData.
	/// </summary>
	/// <param name="tempData">ITempDataDictionary.</param>
	/// <param name="messageType">AlertType.</param>
	/// <param name="message">Message.</param>
	private static void AddAlert(ITempDataDictionary tempData, AlertType messageType, string message)
	{
		var alerts = GetAlerts(tempData);
		alerts.Insert(0, new Alert(messageType, message));
		var json = JsonF.Serialise(alerts).Unwrap(_ => JsonF.Empty);

		if (HasAlerts(tempData))
		{
			tempData[AlertsKey] = json;
		}
		else
		{
			tempData.Add(AlertsKey, json);
		}
	}
}
