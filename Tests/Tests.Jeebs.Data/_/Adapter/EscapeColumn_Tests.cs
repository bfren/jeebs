using System;
using System.Collections.Generic;
using System.Text;
using NSubstitute;
using Xunit;
using static Jeebs.Data.Adapter_Tests.Adapter;

namespace Jeebs.Data.Adapter_Tests
{
	public class EscapeColumn_Tests
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
			var result = adapter.EscapeColumn(input, Arg.Any<string>());

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
			var name = "one";
			var adapter = GetAdapter();

			// Act
			var result = adapter.EscapeColumn(name, input);

			// Assert
			Assert.Equal("[one]", result);
		}

		[Fact]
		public void Escaped_Name_With_Alias()
		{
			// Arrange
			var name = "one";
			var alias = "two";
			var adapter = GetAdapter();

			// Act
			var result = adapter.EscapeColumn(name, alias);

			// Assert
			Assert.Equal("[one] AS {two}", result);
		}

		[Fact]
		public void Escaped_Name_With_Alias_And_Table()
		{
			// Arrange
			var name = "one";
			var alias = "two";
			var table = "three";
			var adapter = GetAdapter();

			// Act
			var result = adapter.EscapeColumn(name, alias, table);

			// Assert
			Assert.Equal("[three].[one] AS {two}", result);
		}
	}
}
