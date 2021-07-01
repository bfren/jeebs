// Jeebs Unit Tests
// Copyright (c) bfren.uk - licensed under https://mit.bfren.uk/2013

using Xunit;

namespace Jeebs.Mvc.Models.MenuItemText_Tests
{
	public class Constructor_Tests
	{
		[Fact]
		public void Sets_Text()
		{
			// Arrange
			var text = F.Rnd.Str;

			// Act
			var result = new MenuItemText(text);

			// Assert
			Assert.Equal(text, result.Text);
		}

		[Fact]
		public void Sets_IsLink_To_False()
		{
			// Arrange

			// Act
			var result = new MenuItemText(F.Rnd.Str);

			// Assert
			Assert.False(result.IsLink);
		}
	}
}
