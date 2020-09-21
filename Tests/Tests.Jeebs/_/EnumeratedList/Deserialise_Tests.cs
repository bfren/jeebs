using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Jeebs.EnumeratedList_Tests
{
	public class Deserialise_Tests
	{
		[Theory]
		[InlineData(null)]
		[InlineData("")]
		[InlineData(" ")]
		public void Null_Or_Empty_Returns_Empty_List(string input)
		{
			// Arrange

			// Act
			var result = EnumeratedList<Foo>.Deserialise(input);

			// Assert
			Assert.Empty(result);
		}

		[Fact]
		public void Invalid_Json_Returns_Empty_List()
		{
			// Arrange
			var json = "this is invalid json";

			// Act
			var result = EnumeratedList<Foo>.Deserialise(json);

			// Assert
			Assert.Empty(result);
		}

		[Fact]
		public void Incorrect_Json_Returns_Empty_List()
		{
			// Arrange
			var json = "{\"foo\":\"bar\"}";

			// Act
			var result = EnumeratedList<Foo>.Deserialise(json);

			// Assert
			Assert.Empty(result);
		}

		[Fact]
		public void Correct_Json_Returns_List()
		{
			// Arrange
			var itemA = new Foo("Item A");
			var itemB = new Foo("Item B");
			var itemC = new Foo("Item C");
			var itemD = new Foo("Item D");
			var json = $"[\"{itemB}\",\"{itemD}\",\"{itemA}\",\"{itemC}\"]";

			// Act
			var result = EnumeratedList<Foo>.Deserialise(json);

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

			public static Foo A = new Foo("Item A");

			public static Foo B = new Foo("Item B");
		}
	}
}
