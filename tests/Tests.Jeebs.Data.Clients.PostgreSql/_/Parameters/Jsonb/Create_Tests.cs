// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Functions;

namespace Jeebs.Data.Clients.PostgreSql.Parameters.Jsonb_Tests
{
	public class Create_Tests
	{
		[Fact]
		public void Null_Object_Creates_With_Empty_Json()
		{
			// Arrange
			int? value = null;

			// Act
			var result = Jsonb.Create(value);

			// Assert
			Assert.Equal(JsonF.Empty, result.ToString());
		}

		[Theory]
		[InlineData(true, "true")]
		[InlineData(18, "18")]
		[InlineData("foo", "\"foo\"")]
		public void Value_Creates_With_Json(object input, string expected)
		{
			// Arrange

			// Act
			var result = Jsonb.Create(input);

			// Assert
			Assert.Equal(expected, result.ToString());
		}

		[Fact]
		public void Object_Creates_With_Json()
		{
			// Arrange
			var value = new Test(Rnd.Str, Rnd.Int);
			var json = JsonF.Serialise(value).UnsafeUnwrap();

			// Act
			var result = Jsonb.Create(value);

			// Assert
			Assert.Equal(json, result.ToString());
		}
	}

	public sealed record class Test(string Foo, int Bar);
}
