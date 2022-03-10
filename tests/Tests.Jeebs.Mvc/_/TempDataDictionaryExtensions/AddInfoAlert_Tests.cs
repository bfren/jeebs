// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Mvc.Enums;

namespace Jeebs.Mvc.TempDataDictionaryExtensions_Tests;

public class AddInfoAlert_Tests : Setup
{
	[Fact]
	public void Adds_Info_Alert() =>
		Adds_Alert((td, txt) => td.AddInfoAlert(txt), AlertType.Info);
}
