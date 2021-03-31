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
			var one = F.Rnd.Str;
			var two = F.Rnd.Str;
			var three = F.Rnd.Str;
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
			var input = F.Rnd.Str;

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
			var name = F.Rnd.Str;
			var table = new Table(name);

			// Act
			var result = adapter.EscapeTable(table);

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
