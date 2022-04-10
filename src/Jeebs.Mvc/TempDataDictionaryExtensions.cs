// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System.Collections.Generic;
using Jeebs.Functions;
using Jeebs.Mvc.Enums;
using Jeebs.Mvc.Models;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace Jeebs.Mvc;

/// <summary>
/// Extension methods for TempData to add / retrieve alert messages
/// </summary>
public static class TempDataDictionaryExtensions
{
	internal const string AlertsKey = "JeebsAlerts";

	/// <summary>
	/// Returns whether or not the TempDataDictionary has any pending alerts
	/// </summary>
	/// <param name="this">ITempDataDictionary</param>
	public static bool HasAlerts(this ITempDataDictionary @this) =>
		@this.Peek(AlertsKey) != null;

	/// <summary>
	/// Get all alerts
	/// </summary>
	/// <param name="this">ITempDataDictionary</param>
	public static List<Alert> GetAlerts(this ITempDataDictionary @this) =>
		@this.TryGetValue(AlertsKey, out var value) switch
		{
			true when value is string alerts =>
				JsonF.Deserialise<List<Alert>>(alerts).Unwrap(() => new List<Alert>()),

			_ =>
				new List<Alert>()
		};

	/// <summary>
	/// Add info alert
	/// </summary>
	/// <param name="this">ITempDataDictionary</param>
	/// <param name="message">Message</param>
	public static void AddInfoAlert(this ITempDataDictionary @this, string message) =>
		AddAlert(@this, AlertType.Info, message);

	/// <summary>
	/// Add info alert
	/// </summary>
	/// <param name="this">ITempDataDictionary</param>
	/// <param name="message">Message</param>
	public static void AddInfoAlert(this ITempDataDictionary @this, IMsg message) =>
		AddAlert(@this, AlertType.Info, message);

	/// <summary>
	/// Add success alert
	/// </summary>
	/// <param name="this">ITempDataDictionary</param>
	/// <param name="message"></param>
	public static void AddSuccessAlert(this ITempDataDictionary @this, string message) =>
		AddAlert(@this, AlertType.Success, message);

	/// <summary>
	/// Add success alert
	/// </summary>
	/// <param name="this">ITempDataDictionary</param>
	/// <param name="message"></param>
	public static void AddSuccessAlert(this ITempDataDictionary @this, IMsg message) =>
		AddAlert(@this, AlertType.Success, message);

	/// <summary>
	/// Add warning alert
	/// </summary>
	/// <param name="this">ITempDataDictionary</param>
	/// <param name="message">Message</param>
	public static void AddWarningAlert(this ITempDataDictionary @this, string message) =>
		AddAlert(@this, AlertType.Warning, message);

	/// <summary>
	/// Add warning alert
	/// </summary>
	/// <param name="this">ITempDataDictionary</param>
	/// <param name="message">Message</param>
	public static void AddWarningAlert(this ITempDataDictionary @this, IMsg message) =>
		AddAlert(@this, AlertType.Warning, message);

	/// <summary>
	/// Add Error alert
	/// </summary>
	/// <param name="this">ITempDataDictionary</param>
	/// <param name="message">Message</param>
	public static void AddErrorAlert(this ITempDataDictionary @this, string message) =>
		AddAlert(@this, AlertType.Error, message);

	/// <summary>
	/// Add Error alert
	/// </summary>
	/// <param name="this">ITempDataDictionary</param>
	/// <param name="message">Message</param>
	public static void AddErrorAlert(this ITempDataDictionary @this, IMsg message) =>
		AddAlert(@this, AlertType.Error, message);

	/// <inheritdoc cref="AddAlert(ITempDataDictionary, AlertType, string)"/>
	private static void AddAlert(ITempDataDictionary tempData, AlertType messageType, IMsg message) =>
		AddAlert(tempData, messageType, message.ToString() ?? message.GetType().Name);

	/// <summary>
	/// Add alert to TempData
	/// </summary>
	/// <param name="tempData">ITempDataDictionary</param>
	/// <param name="messageType">AlertType</param>
	/// <param name="message">Message</param>
	private static void AddAlert(ITempDataDictionary tempData, AlertType messageType, string message)
	{
		var alerts = GetAlerts(tempData);
		alerts.Insert(0, new Alert(messageType, message));
		var json = JsonF.Serialise(alerts).Unwrap(JsonF.Empty);

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
