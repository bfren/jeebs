// Jeebs Unit Tests
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using Xunit;

namespace Jeebs.Mvc.Models.MenuItem_Tests
{
	public class AddChild_Tests
	{
		[Theory]
		[InlineData(null)]
		[InlineData("")]
		public void Use_Action_If_Text_Null_Or_Empty(string? input)
		{
			// Arrange
			var menu = new MenuItem();
			var action = F.Rnd.Str;

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
			var controller = F.Rnd.Str;
			var routeValues = new object();
			var menu = new MenuItem
			{
				Controller = controller,
				RouteValues = routeValues
			};

			var action = F.Rnd.Str;
			var text = F.Rnd.Str;
			var description = F.Rnd.Str;

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
}
