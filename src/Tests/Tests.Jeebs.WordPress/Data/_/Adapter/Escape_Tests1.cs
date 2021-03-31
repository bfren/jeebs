// Jeebs Unit Tests
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using Xunit;
using static Jeebs.WordPress.Data.Adapter_Tests.Adapter;

namespace Jeebs.WordPress.Data.Adapter_Tests
{
	public partial class Escape_Tests
	{
		[Theory]
		[InlineData(null)]
		[InlineData("")]
		[InlineData(" ")]
		public void No_Name_Returns_Empty(string input)
		{
			// Arrange
			var adapter = GetAdapter();

			// Act
			var result = adapter.Escape(input, F.Rnd.Str);

			// Assert
			Assert.Equal(string.Empty, result);
		}

		[Theory]
		[InlineData(null)]
		[InlineData("")]
		[InlineData(" ")]
		public void Name_Without_Alias_Returns_Escaped_Name(string input)
		{
			// Arrange
			var name = F.Rnd.Str;
			var adapter = GetAdapter();

			// Act
			var result = adapter.Escape(name, input);

			// Assert
			Assert.Equal($"[{name}]", result);
		}

		[Fact]
		public void Escaped_Name_With_Alias()
		{
			// Arrange
			var name = F.Rnd.Str;
			var alias = F.Rnd.Str;
			var adapter = GetAdapter();

			// Act
			var result = adapter.Escape(name, alias);

			// Assert
			Assert.Equal($"[{name}] AS {{{alias}}}", result);
		}

		[Fact]
		public void Escaped_Name_With_Alias_And_Table()
		{
			// Arrange
			var name = F.Rnd.Str;
			var alias = F.Rnd.Str;
			var table = F.Rnd.Str;
			var adapter = GetAdapter();

			// Act
			var result = adapter.Escape(name, alias, table);

			// Assert
			Assert.Equal($"[{table}].[{name}] AS {{{alias}}}", result);
		}
	}
}
