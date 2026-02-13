// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Mvc.Enums;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace Jeebs.Mvc.TempDataDictionaryExtensions_Tests;

public class GetAlerts_Tests
{
	[Fact]
	public void Returns_Empty_List_When_No_Alerts()
	{
		// Arrange
		var td = Substitute.For<ITempDataDictionary>();

		// Act
		var result = td.GetAlerts();

		// Assert
		Assert.Empty(result);
	}

	[Fact]
	public void Returns_Empty_List_When_Invalid_Json()
	{
		// Arrange
		var td = Substitute.For<ITempDataDictionary>();
		td.TryGetValue(TempDataDictionaryExtensions.AlertsKey, out Arg.Any<object?>())
			.Returns(x => { x[1] = Rnd.Str; return true; });

		// Act
		var result = td.GetAlerts();

		// Assert
		Assert.Empty(result);
	}

	[Fact]
	public void Returns_Deserialised_Alerts_When_Valid_Json()
	{
		// Arrange
		var a0Type = AlertType.Success;
		var a0Text = Rnd.Str;
		var a0 = $"{{\"type\":\"{a0Type.ToString().ToLowerInvariant()}\",\"text\":\"{a0Text}\"}}";
		var a1Type = AlertType.Warning;
		var a1Text = Rnd.Str;
		var a1 = $"{{\"type\":\"{a1Type.ToString().ToLowerInvariant()}\",\"text\":\"{a1Text}\"}}";
		var json = $"[{a0},{a1}]";

		var td = Substitute.For<ITempDataDictionary>();
		td.TryGetValue(TempDataDictionaryExtensions.AlertsKey, out Arg.Any<object?>())
			.Returns(x => { x[1] = json; return true; });

		// Act
		var result = td.GetAlerts();

		// Assert
		Assert.Collection(result,
			x =>
			{
				Assert.Equal(a0Type, x.Type);
				Assert.Equal(a0Text, x.Text);
			},
			x =>
			{
				Assert.Equal(a1Type, x.Type);
				Assert.Equal(a1Text, x.Text);
			}
		);
	}
}
