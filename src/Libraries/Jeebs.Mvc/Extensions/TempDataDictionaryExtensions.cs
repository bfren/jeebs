// Jeebs Rapid Application Development
// Copyright (c) bfren.uk - licensed under https://mit.bfren.uk/2013

using System.Collections.Generic;
using Jeebs.Mvc.Models;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace Jeebs.Mvc
{
	/// <summary>
	/// Extension methods for TempData to add / retrieve alert messages
	/// </summary>
	public static class TempDataDictionaryExtensions
	{
		private const string alertsKey = "JeebsAlerts";

		/// <summary>
		/// Returns whether or not the TempDataDictionary has any pending alerts
		/// </summary>
		/// <param name="this">ITempDataDictionary</param>
		/// <returns>True if the TempDataDictionary has any pending alerts</returns>
		public static bool HasAlerts(this ITempDataDictionary @this) =>
			@this.Peek(alertsKey) != null;

		/// <summary>
		/// Get all alerts
		/// </summary>
		/// <param name="this">ITempDataDictionary</param>
		/// <returns>List of alerts</returns>
		public static List<Alert> GetAlerts(this ITempDataDictionary @this) =>
			@this.TryGetValue(alertsKey, out var value) switch
			{
				true when value is string alerts =>
					F.JsonF.Deserialise<List<Alert>>(alerts).Unwrap(() => new List<Alert>()),

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
		/// Add success alert
		/// </summary>
		/// <param name="this">ITempDataDictionary</param>
		/// <param name="message"></param>
		public static void AddSuccessAlert(this ITempDataDictionary @this, string message) =>
			AddAlert(@this, AlertType.Success, message);

		/// <summary>
		/// Add warning alert
		/// </summary>
		/// <param name="this">ITempDataDictionary</param>
		/// <param name="message">Message</param>
		public static void AddWarningAlert(this ITempDataDictionary @this, string message) =>
			AddAlert(@this, AlertType.Warning, message);

		/// <summary>
		/// Add Error alert
		/// </summary>
		/// <param name="this">ITempDataDictionary</param>
		/// <param name="message">Message</param>
		public static void AddErrorAlert(this ITempDataDictionary @this, string message) =>
			AddAlert(@this, AlertType.Error, message);

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

			tempData.Add(alertsKey, F.JsonF.Serialise(alerts).Unwrap(F.JsonF.Empty));
		}
	}
}
