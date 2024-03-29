﻿// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

namespace Jeebs.Mvc.Models.MenuItemText_Tests;

public class Constructor_Tests
{
	[Fact]
	public void Sets_Text()
	{
		// Arrange
		var text = Rnd.Str;

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
		var result = new MenuItemText(Rnd.Str);

		// Assert
		Assert.False(result.IsLink);
	}
}
