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
		public void Null_Or_Empty_Returns_Empty(string input)
		{
			// Arrange
			var adapter = GetAdapter();

			// Act
			var result = adapter.Escape(input);

			// Assert
			Assert.Equal(string.Empty, result);
		}

		[Fact]
		public void Containing_SchemaSeparator_Splits_Escapes_And_Rejoins()
		{
			// Arrange
			var adapter = GetAdapter();
			var input = $"one{SchemaSeparator}two{SchemaSeparator}three";

			// Act
			var result = adapter.Escape(input);

			// Assert
			Assert.Equal("[one].[two].[three]", result);
		}

		[Fact]
		public void Escape_Simple()
		{
			// Arrange
			var adapter = GetAdapter();
			var input = "one";

			// Act
			var result = adapter.Escape(input);

			// Assert
			Assert.Equal("[one]", result);
		}

		[Fact]
		public void Escape_Table_Name()
		{
			// Arrange
			var adapter = GetAdapter();
			var name = "one";
			var table = new Table(name);

			// Act
			var result = adapter.Escape(table);

			// Assert
			Assert.Equal("[one]", result);
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
