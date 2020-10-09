using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using static Jeebs.Data.Adapter_Tests.Adapter;

namespace Jeebs.Data.Adapter_Tests
{
	public class Escape_Tests
	{
		[Theory]
		[InlineData(null)]
		[InlineData("")]
		[InlineData(" ")]
		public void Invalid_Identifier_Returns_Empty(string input)
		{
			// Arrange
			var adapter = GetAdapter();

			// Act
			var result = adapter.Escape(input);

			// Assert
			Assert.Equal(string.Empty, result);
		}

		[Fact]
		public void Contains_SchemaSeparator_Splits_Escapes_And_Rejoins()
		{
			// Arrange
			var adapter = GetAdapter();
			var one = F.Rand.String;
			var two = F.Rand.String;
			var three = F.Rand.String;
			var input = $"{one}{SchemaSeparator}{two}{SchemaSeparator}{three}";

			// Act
			var result = adapter.Escape(input);

			// Assert
			Assert.Equal($"[{one}].[{two}].[{three}]", result);
		}

		[Fact]
		public void Escapes_Simple()
		{
			// Arrange
			var adapter = GetAdapter();
			var input = F.Rand.String;

			// Act
			var result = adapter.Escape(input);

			// Assert
			Assert.Equal($"[{input}]", result);
		}

		[Fact]
		public void Escapes_Table_Name()
		{
			// Arrange
			var adapter = GetAdapter();
			var name = F.Rand.String;
			var table = new Table(name);

			// Act
			var result = adapter.Escape(table);

			// Assert
			Assert.Equal($"[{name}]", result);
		}

		public class Table
		{
			public string Name { get; set; }

			public Table(string name)
				=> Name = name;

			public override string ToString()
				=> Name;
		}
	}
}
