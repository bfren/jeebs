// Jeebs Unit Tests
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using Xunit;

namespace Jeebs.Data.TypeHandlers.EnumeratedList_Tests
{
	public class Parse_Tests
	{
		[Theory]
		[InlineData("")]
		[InlineData(" ")]
		public void Empty_Returns_Empty_List(string input)
		{
			// Arrange
			var handler = new EnumeratedListTypeHandler<Foo>();

			// Act
			var result = handler.Parse(input);

			// Assert
			Assert.Empty(result);
		}

		[Fact]
		public void Invalid_Json_Returns_Empty_List()
		{
			// Arrange
			var handler = new EnumeratedListTypeHandler<Foo>();
			var json = F.Rnd.Str;

			// Act
			var result = handler.Parse(json);

			// Assert
			Assert.Empty(result);
		}

		[Fact]
		public void Incorrect_Json_Returns_Empty_List()
		{
			// Arrange
			var handler = new EnumeratedListTypeHandler<Foo>();
			const string? json = "{\"foo\":\"bar\"}";

			// Act
			var result = handler.Parse(json);

			// Assert
			Assert.Empty(result);
		}

		[Fact]
		public void Correct_Json_Returns_List()
		{
			// Arrange
			var handler = new EnumeratedListTypeHandler<Foo>();
			var itemA = new Foo(F.Rnd.Str);
			var itemB = new Foo(F.Rnd.Str);
			var itemC = new Foo(F.Rnd.Str);
			var itemD = new Foo(F.Rnd.Str);
			var json = $"[\"{itemB}\",\"{itemD}\",\"{itemA}\",\"{itemC}\"]";

			// Act
			var result = handler.Parse(json);

			// Assert
			Assert.Collection(result,
				b => Assert.Equal(itemB, b),
				d => Assert.Equal(itemD, d),
				a => Assert.Equal(itemA, a),
				c => Assert.Equal(itemC, c)
			);
		}

		public class Foo : Enumerated
		{
			public Foo(string name) : base(name) { }
		}
	}
}
