// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System.Linq;
using System.Threading.Tasks;
using Jeebs.Linq;
using Xunit;
using static F.MaybeF;

namespace Jeebs.Linq_Tests;

public class Where_Tests
{
	[Fact]
	public void Where_True_With_Some_Returns_Some()
	{
		// Arrange
		var value = F.Rnd.Int;
		var maybe = Some(value);

		// Act
		var r0 = maybe.Where(s => s == value).Select(s => s ^ 2);
		var r1 = from a in maybe
				 where a == value
				 select a ^ 2;

		// Assert
		var s0 = r0.AssertSome();
		Assert.Equal(value ^ 2, s0);
		var s1 = r1.AssertSome();
		Assert.Equal(value ^ 2, s1);
	}

	[Fact]
	public async Task Async_Where_True_With_Some_Returns_Some()
	{
		// Arrange
		var value = F.Rnd.Int;
		var maybe = Some(value);

		// Act
		var r0 = await maybe.AsTask.Where(s => s == value).Select(s => s ^ 2).ConfigureAwait(false);
		var r1 = await maybe.Where(s => Task.FromResult(s == value)).Select(s => s ^ 2).ConfigureAwait(false);
		var r2 = await maybe.AsTask.Where(s => Task.FromResult(s == value)).Select(s => s ^ 2).ConfigureAwait(false);
		var r3 = await (
			from a in maybe.AsTask
			where a == value
			select a ^ 2
		).ConfigureAwait(false);
		var r4 = await (
			from a in maybe
			where Task.FromResult(a == value)
			select a ^ 2
		).ConfigureAwait(false);
		var r5 = await (
			from a in maybe.AsTask
			where Task.FromResult(a == value)
			select a ^ 2
		).ConfigureAwait(false);

		// Assert
		var s0 = r0.AssertSome();
		Assert.Equal(value ^ 2, s0);
		var s1 = r1.AssertSome();
		Assert.Equal(value ^ 2, s1);
		var s2 = r2.AssertSome();
		Assert.Equal(value ^ 2, s2);
		var s3 = r3.AssertSome();
		Assert.Equal(value ^ 2, s3);
		var s4 = r4.AssertSome();
		Assert.Equal(value ^ 2, s4);
		var s5 = r5.AssertSome();
		Assert.Equal(value ^ 2, s5);
	}

	[Fact]
	public void Where_False_With_Some_Returns_None()
	{
		// Arrange
		var value = F.Rnd.Int;
		var maybe = Some(value);

		// Act
		var r0 = maybe.Where(s => s != value).Select(s => s ^ 2);
		var r1 = from a in maybe
				 where a != value
				 select a ^ 2;

		// Assert
		_ = r0.AssertNone();
		_ = r1.AssertNone();
	}

	[Fact]
	public async Task Async_Where_False_With_Some_Returns_None()
	{
		// Arrange
		var value = F.Rnd.Int;
		var maybe = Some(value);

		// Act
		var r0 = await maybe.AsTask.Where(s => s != value).Select(s => s ^ 2).ConfigureAwait(false);
		var r1 = await maybe.Where(s => Task.FromResult(s != value)).Select(s => s ^ 2).ConfigureAwait(false);
		var r2 = await maybe.AsTask.Where(s => Task.FromResult(s != value)).Select(s => s ^ 2).ConfigureAwait(false);
		var r3 = await (
			from a in maybe.AsTask
			where a != value
			select a ^ 2
		).ConfigureAwait(false);
		var r4 = await (
			from a in maybe
			where Task.FromResult(a != value)
			select a ^ 2
		).ConfigureAwait(false);
		var r5 = await (
			from a in maybe.AsTask
			where Task.FromResult(a != value)
			select a ^ 2
		).ConfigureAwait(false);

		// Assert
		_ = r0.AssertNone();
		_ = r1.AssertNone();
		_ = r2.AssertNone();
		_ = r3.AssertNone();
		_ = r4.AssertNone();
		_ = r5.AssertNone();
	}

	[Fact]
	public void Where_With_None_Returns_None()
	{
		// Arrange
		var maybe = None<int>(new InvalidIntegerMsg());

		// Act
		var r0 = maybe.Where(s => s == 0).Select(s => s ^ 2);
		var r1 = from a in maybe
				 where a == 0
				 select a ^ 2;

		// Assert
		var n0 = r0.AssertNone();
		_ = Assert.IsType<InvalidIntegerMsg>(n0);
		var n1 = r1.AssertNone();
		_ = Assert.IsType<InvalidIntegerMsg>(n1);
	}

	[Fact]
	public async Task Async_Where_With_None_Returns_None()
	{
		// Arrange
		var maybe = None<int>(new InvalidIntegerMsg());

		// Act
		var r0 = await maybe.AsTask.Where(s => s != 0).Select(s => s ^ 2).ConfigureAwait(false);
		var r1 = await maybe.Where(s => Task.FromResult(s != 0)).Select(s => s ^ 2).ConfigureAwait(false);
		var r2 = await maybe.AsTask.Where(s => Task.FromResult(s != 0)).Select(s => s ^ 2).ConfigureAwait(false);
		var r3 = await (
			from a in maybe.AsTask
			where a != 0
			select a ^ 2
		).ConfigureAwait(false);
		var r4 = await (
			from a in maybe
			where Task.FromResult(a != 0)
			select a ^ 2
		).ConfigureAwait(false);
		var r5 = await (
			from a in maybe.AsTask
			where Task.FromResult(a != 0)
			select a ^ 2
		).ConfigureAwait(false);

		// Assert
		var n0 = r0.AssertNone();
		_ = Assert.IsType<InvalidIntegerMsg>(n0);
		var n1 = r1.AssertNone();
		_ = Assert.IsType<InvalidIntegerMsg>(n1);
		var n2 = r2.AssertNone();
		_ = Assert.IsType<InvalidIntegerMsg>(n2);
		var n3 = r3.AssertNone();
		_ = Assert.IsType<InvalidIntegerMsg>(n3);
		var n4 = r4.AssertNone();
		_ = Assert.IsType<InvalidIntegerMsg>(n4);
		var n5 = r5.AssertNone();
		_ = Assert.IsType<InvalidIntegerMsg>(n5);
	}

	public record class InvalidIntegerMsg : Msg;
}
