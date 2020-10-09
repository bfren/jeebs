using System;
using System.Collections.Generic;
using System.Text;
using Jeebs;
using Xunit;

namespace F.JsonF_Tests
{
	public partial class Deserialise_Tests
	{
		[Theory]
		[InlineData(null)]
		[InlineData("")]
		[InlineData(" ")]
		[InlineData("\n")]
		public void Null_Or_Whitespace_Returns_None(string input)
		{
			// Arrange

			// Act
			var result = JsonF.Deserialise<Test>(input);

			// Assert
			var none = Assert.IsAssignableFrom<None<Test>>(result);
			Assert.True(none.Reason is Jm.Functions.JsonF.DeserialisingNullOrEmptyStringMsg);
		}

		[Fact]
		public void InvalidJson_Returns_None()
		{
			// Arrange
			var input = Rnd.String;

			// Act
			var result = JsonF.Deserialise<Test>(input);

			// Assert
			var none = Assert.IsAssignableFrom<None<Test>>(result);
			Assert.True(none.Reason is Jm.Functions.JsonF.DeserialiseExceptionMsg);
		}

		[Fact]
		public void ValidJson_ReturnsObject()
		{
			// Arrange
			var v0 = Rnd.String;
			var v1 = Rnd.Integer;
			var input = $"{{\"foo\":\"{v0}\",\"bar\":{v1},\"ignore\":\"this\"}}";
			var expected = new Test { Foo = v0, Bar = v1 };

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
