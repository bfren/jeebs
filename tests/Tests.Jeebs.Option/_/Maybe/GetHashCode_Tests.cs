// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Exceptions;
using NSubstitute;
using Xunit;
using static F.MaybeF;

namespace Jeebs.Maybe_Tests;

public class GetHashCode_Tests
{
	[Fact]
	public void If_Unknown_Maybe_Throws_UnknownOptionException()
	{
		// Arrange
		var maybe = new FakeMaybe();

		// Act
		var action = void () => maybe.GetHashCode();

		// Assert
		Assert.Throws<UnknownMaybeException>(action);
	}

	[Fact]
	public void Some_With_Same_Value_Generates_Same_HashCode()
	{
		// Arrange
		var value = F.Rnd.Str;
		var s0 = value.Some();
		var s1 = value.Some();

		// Act
		var h0 = s0.GetHashCode();
		var h1 = s1.GetHashCode();

		// Assert
		Assert.Equal(h0, h1);
	}

	[Fact]
	public void Some_With_Same_Type_And_Different_Value_Generates_Different_HashCode()
	{
		// Arrange
		var v0 = F.Rnd.Str;
		var v1 = F.Rnd.Str;
		var s0 = v0.Some();
		var s1 = v1.Some();

		// Act
		var h0 = s0.GetHashCode();
		var h1 = s1.GetHashCode();

		// Assert
		Assert.NotEqual(h0, h1);
	}

	[Fact]
	public void Some_With_Null_Value_And_Same_Type_Generates_Same_HashCode()
	{
		// Arrange
		string? v0 = null;
		string? v1 = null;
		var s0 = Some(v0, true);
		var s1 = Some(v1, true);

		// Act
		var h0 = s0.GetHashCode();
		var h1 = s1.GetHashCode();

		// Assert
		Assert.Equal(h0, h1);
	}

	[Fact]
	public void Some_With_Null_Value_And_Different_Type_Generates_Different_HashCode()
	{
		// Arrange
		string? v0 = null;
		int? v1 = null;
		var s0 = Some(v0, true);
		var s1 = Some(v1, true);

		// Act
		var h0 = s0.GetHashCode();
		var h1 = s1.GetHashCode();

		// Assert
		Assert.NotEqual(h0, h1);
	}

	[Fact]
	public void None_With_Same_Type_And_Same_Reason_Generates_Same_HashCode()
	{
		// Arrange
		var msg = Substitute.For<Msg>();
		var n0 = None<int>(msg);
		var n1 = None<int>(msg);

		// Act
		var h0 = n0.GetHashCode();
		var h1 = n1.GetHashCode();

		// Assert
		Assert.Equal(h0, h1);
	}

	[Fact]
	public void None_With_Different_Type_And_Same_Reason_Generates_Different_HashCode()
	{
		// Arrange
		var msg = Substitute.For<Msg>();
		var n0 = None<int>(msg);
		var n1 = None<string>(msg);

		// Act
		var h0 = n0.GetHashCode();
		var h1 = n1.GetHashCode();

		// Assert
		Assert.NotEqual(h0, h1);
	}

	[Fact]
	public void None_With_Same_Type_And_Different_Reason_Generates_Different_HashCode()
	{
		// Arrange
		var m0 = new TestMsg0();
		var m1 = new TestMsg1();
		var n0 = None<int>(m0);
		var n1 = None<int>(m1);

		// Act
		var h0 = n0.GetHashCode();
		var h1 = n1.GetHashCode();

		// Assert
		Assert.NotEqual(h0, h1);
	}

	public record class FakeMaybe : Maybe<int> { }

	public record class TestMsg0 : Msg;

	public record class TestMsg1 : Msg;
}
