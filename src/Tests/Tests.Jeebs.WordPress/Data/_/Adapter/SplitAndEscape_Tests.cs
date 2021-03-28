﻿// Jeebs Unit Tests
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using Xunit;
using static Jeebs.WordPress.Data.Adapter_Tests.Adapter;

namespace Jeebs.WordPress.Data.Adapter_Tests
{
	public class SplitAndEscape_Tests
	{
		[Theory]
		[InlineData(null)]
		[InlineData("")]
		[InlineData(" ")]
		public void Invalid_Characters_Returns_Empty(string input)
		{
			// Arrange
			var adapter = GetAdapter();

			// Act
			var result = adapter.SplitAndEscape(input);

			// Assert
			Assert.Equal(string.Empty, result);
		}

		[Fact]
		public void Contains_SchemaSeparator_Splits_Escapes_And_Rejoins()
		{
			// Arrange
			var adapter = GetAdapter();
			var one = F.Rnd.Str;
			var two = F.Rnd.Str;
			var three = F.Rnd.Str;
			var input = $"{one}{SchemaSeparator}{two}{SchemaSeparator}{three}";

			// Act
			var result = adapter.SplitAndEscape(input);

			// Assert
			Assert.Equal($"[{one}].[{two}].[{three}]", result);
		}
	}
}
