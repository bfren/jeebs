// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Mvc.Enums;

namespace Jeebs.Mvc.TempDataDictionaryExtensions_Tests;

public class AddSuccessAlert_Tests : Setup
{
	[Fact]
	public void Adds_Success_Alert() =>
		Adds_Alert((td, txt) => td.AddSuccessAlert(txt), AlertType.Success);
}
