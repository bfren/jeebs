// Jeebs Unit Tests
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

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
