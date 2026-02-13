// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Mvc.Enums;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace Jeebs.Mvc;

public static partial class TempDataDictionaryExtensions
{
	/// <summary>
	/// Add success alert.
	/// </summary>
	/// <param name="this">ITempDataDictionary.</param>
	/// <param name="message"></param>
	public static void AddSuccessAlert(this ITempDataDictionary @this, string message) =>
		AddAlert(@this, AlertType.Success, message);
}
