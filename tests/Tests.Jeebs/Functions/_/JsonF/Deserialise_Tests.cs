// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using static Jeebs.Functions.JsonF.M;

namespace Jeebs.Functions.JsonF_Tests;

public class Deserialise_Tests
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
		result.AssertNone().AssertType<DeserialisingNullOrEmptyStringMsg>();
	}

	[Fact]
	public void InvalidJson_Returns_None()
	{
		// Arrange
		var input = Rnd.Str;

		// Act
		var result = JsonF.Deserialise<Test>(input);

		// Assert
		result.AssertNone().AssertType<DeserialiseExceptionMsg>();
	}

	[Fact]
	public void ValidJson_ReturnsObject()
	{
		// Arrange
		var v0 = Rnd.Str;
		var v1 = Rnd.Int;
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
