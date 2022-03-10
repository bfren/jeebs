// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Mvc.Enums;

namespace Jeebs.Mvc.TempDataDictionaryExtensions_Tests;

public class AddWarningAlert_Tests : Setup
{
	[Fact]
	public void Adds_Warning_Alert() =>
		Adds_Alert((td, txt) => td.AddWarningAlert(txt), AlertType.Warning);
}
