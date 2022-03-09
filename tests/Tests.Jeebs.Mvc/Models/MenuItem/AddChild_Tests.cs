// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

namespace Jeebs.Mvc.Models.MenuItem_Tests;

public class AddChild_Tests
{
	[Theory]
	[InlineData(null)]
	[InlineData("")]
	public void Use_Action_If_Text_Null_Or_Empty(string? input)
	{
		// Arrange
		var menu = new MenuItem();
		var action = Rnd.Str;

		// Act
		menu.AddChild(action, input);

		// Assert
		Assert.Collection(menu.Children,
			x =>
			{
				Assert.Equal(action, x.Text);
			}
		);
	}

	[Fact]
	public void Sets_Child_Properties()
	{
		// Arrange
		var controller = Rnd.Str;
		var routeValues = new object();
		var menu = new MenuItem
		{
			Controller = controller,
			RouteValues = routeValues
		};

		var action = Rnd.Str;
		var text = Rnd.Str;
		var description = Rnd.Str;

		// Act
		menu.AddChild(action, text, description);

		// Assert
		Assert.Collection(menu.Children,
			x =>
			{
				Assert.Equal(controller, x.Controller);
				Assert.Equal(action, x.Action);
				Assert.Equal(text, x.Text);
				Assert.Equal(description, x.Description);
				Assert.Same(routeValues, x.RouteValues);
			}
		);
	}
}
