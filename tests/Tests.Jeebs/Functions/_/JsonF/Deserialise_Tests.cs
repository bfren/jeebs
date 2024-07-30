// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System.Collections;
using MaybeF;
using StrongId;
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
		var v0 = Rnd.Lng;
		var v1 = Rnd.Str;
		var v2 = Rnd.Int;
		var v3 = Rnd.DateTime;
		var v4 = Rnd.Flip;
		var input =
			"{" +
			$"\"id\":\"{v0}\"," +
			$"\"str\":\"{v1}\"," +
			$"\"num\":{v2}," +
			$"\"dt\":\"{v3:u}\"," +
			$"\"mbe\":{JsonF.Bool(v4)}," +
			"\"empty\":null" +
			"}";
		var expected = new Test { Id = new(v0), Str = v1, Num = v2, DT = new(v3.Year, v3.Month, v3.Day, v3.Hour, v3.Minute, v3.Second), Mbe = v4 };

		// Act
		var result = JsonF.Deserialise<Test>(input).Unwrap(() => new Test());

		// Assert
		Assert.Equal(expected, result, new TestComparer());
	}

	public sealed class Test
	{
		public TestId Id { get; set; } = new();

		public string Str { get; set; } = string.Empty;

		public int Num { get; set; }

		public DateTime DT { get; set; }

		public Maybe<bool> Mbe { get; set; } = false;

		public string? Empty { get; set; }
	}

	public sealed record class TestId : LongId
	{
		public TestId() { }

		public TestId(long value) : base(value) { }
	}

	public sealed class TestComparer : IEqualityComparer<Test>, IEqualityComparer
	{
		public bool Equals(Test? x, Test? y)
		{
			var id = x?.Id.Value == y?.Id.Value;
			var str = x?.Str == y?.Str;
			var num = x?.Num == y?.Num;
			var dt = x?.DT == y?.DT;
			var mbe = x?.Mbe == y?.Mbe;
			return id && str && num && dt && mbe;
		}

		public new bool Equals(object? x, object? y) =>
			x == y;

		public int GetHashCode(Test obj) =>
			obj.GetHashCode();

		public int GetHashCode(object obj) =>
			throw new NotImplementedException();
	}
}
