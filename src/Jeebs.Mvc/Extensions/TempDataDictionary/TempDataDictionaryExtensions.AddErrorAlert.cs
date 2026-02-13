// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Mvc.Enums;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace Jeebs.Mvc;

public static partial class TempDataDictionaryExtensions
{
	/// <summary>
	/// Add Error alert.
	/// </summary>
	/// <param name="this">ITempDataDictionary.</param>
	/// <param name="message">Message.</param>
	public static void AddErrorAlert(this ITempDataDictionary @this, string message) =>
		AddAlert(@this, AlertType.Error, message);
}
