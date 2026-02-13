// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Mvc.Enums;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace Jeebs.Mvc.TempDataDictionaryExtensions_Tests;

public abstract class Setup
{
	internal static void Adds_Alert(Action<ITempDataDictionary, string> add, AlertType type)
	{
		// Arrange
		var text = Rnd.Str;
		var td = Substitute.For<ITempDataDictionary>();
		var expected = $"[{{\"type\":\"{type.ToString().ToLowerInvariant()}\",\"text\":\"{text}\"}}]";

		// Act
		add(td, text);

		// Assert
		td.Received().Add(TempDataDictionaryExtensions.AlertsKey, expected);
	}
}
