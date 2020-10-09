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
			var result = adapter.EscapeColumn(input, F.Rnd.String);

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
			var name = F.Rnd.String;
			var adapter = GetAdapter();

			// Act
			var result = adapter.EscapeColumn(name, input);

			// Assert
			Assert.Equal($"[{name}]", result);
		}

		[Fact]
		public void Escaped_Name_With_Alias()
		{
			// Arrange
			var name = F.Rnd.String;
			var alias = F.Rnd.String;
			var adapter = GetAdapter();

			// Act
			var result = adapter.EscapeColumn(name, alias);

			// Assert
			Assert.Equal($"[{name}] AS {{{alias}}}", result);
		}

		[Fact]
		public void Escaped_Name_With_Alias_And_Table()
		{
			// Arrange
			var name = F.Rnd.String;
			var alias = F.Rnd.String;
			var table = F.Rnd.String;
			var adapter = GetAdapter();

			// Act
			var result = adapter.EscapeColumn(name, alias, table);

			// Assert
			Assert.Equal($"[{table}].[{name}] AS {{{alias}}}", result);
		}
	}
}
