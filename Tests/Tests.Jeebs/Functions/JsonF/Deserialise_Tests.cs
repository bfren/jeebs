using System;
using System.Collections.Generic;
using System.Text;
using Jeebs;
using Newtonsoft.Json;
using Xunit;

namespace F.JsonF_Tests
{
	public partial class Deserialise_Tests
	{
		[Theory]
		[InlineData(null)]
		public void Null_Returns_None(string input)
		{
			// Arrange

			// Act
			var result = JsonF.Deserialise<Test>(input);

			// Assert
			var none = Assert.IsAssignableFrom<None<Test>>(result);
			Assert.True(none.Reason is Jm.Util.Json.DeserialisingNullStringMsg);
		}

		[Theory]
		[InlineData("")]
		[InlineData(" ")]
		[InlineData("\n")]
		public void Whitespace_Returns_None(string input)
		{
			// Arrange

			// Act
			var result = JsonF.Deserialise<Test>(input);

			// Assert
			var none = Assert.IsAssignableFrom<None<Test>>(result);
			Assert.True(none.Reason is Jm.Util.Json.DeserialisingReturnedNullMsg);
		}

		[Fact]
		public void InvalidJson_Returns_None()
		{
			// Arrange
			const string input = "this is not valid json";

			// Act
			var result = JsonF.Deserialise<Test>(input);

			// Assert
			var none = Assert.IsAssignableFrom<None<Test>>(result);
			Assert.True(none.Reason is Jm.Util.Json.DeserialiseExceptionMsg);
		}

		[Fact]
		public void ValidJson_ReturnsObject()
		{
			// Arrange
			const string input = "{\"foo\":\"test\",\"bar\":2,\"ignore\":\"this\"}";
			var expected = new Test { Foo = "test", Bar = 2 };

			// Act
			var result = JsonF.Deserialise<Test>(input).Unwrap(() => new Test());

			// Assert
			Assert.Equal(expected, result, new TestComparer());
		}

		public class Test
		{
			public string Foo { get; set; } = string.Empty;

			public int Bar { get; set; }

			public string? Empty { get; set; }
		}

		public class TestComparer : IEqualityComparer<Test>
		{
			public bool Equals(Test? x, Test? y) => x?.Foo == y?.Foo && x?.Bar == y?.Bar;

			public int GetHashCode(Test obj) => obj.GetHashCode();
		}
	}
}
