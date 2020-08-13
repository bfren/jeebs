using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using Xunit;

namespace Jeebs.Util
{
	public partial class Json_Tests
	{
		[Theory]
		[InlineData(null)]
		public void Serialise_Null_ReturnsEmpty(object input)
		{
			// Arrange
			const string emptyJson = "{ }";

			// Act
			var result = Json.Serialise(input);

			// Assert
			Assert.Equal(emptyJson, result);
		}

		[Fact]
		public void Serialise_Object_ReturnsJson()
		{
			// Arrange
			var input = new Test { Foo = "test", Bar = 2 };
			const string expected = "{\"foo\":\"test\",\"bar\":2}";

			// Act
			var result = Json.Serialise(input);

			// Assert
			Assert.Equal(expected, result);
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
