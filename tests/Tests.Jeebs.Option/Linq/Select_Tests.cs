// Jeebs Unit Tests
// Copyright (c) bfren.uk - licensed under https://mit.bfren.uk/2013

using System.Threading.Tasks;
using Jeebs.Linq;
using Xunit;
using static F.OptionF;

namespace Jeebs.Linq_Tests
{
	public class Select_Tests
	{
		[Fact]
		public void Select_With_Some_Returns_Some()
		{
			// Arrange
			var value = F.Rnd.Int;
			var option = Return(value);

			// Act
			var r0 = option.Select(s => s ^ 2);
			var r1 = from a in option
					 select a ^ 2;

			// Assert
			var s0 = r0.AssertSome();
			Assert.Equal(value ^ 2, s0);
			var s1 = r1.AssertSome();
			Assert.Equal(value ^ 2, s1);
		}

		[Fact]
		public async Task Async_Select_With_Some_Returns_Some()
		{
			// Arrange
			var value = F.Rnd.Int;
			var option = Return(value);

			// Act
			var r0 = await option.AsTask.Select(s => s ^ 2);
			var r1 = await option.Select(s => Task.FromResult(s ^ 2));
			var r2 = await option.AsTask.Select(s => Task.FromResult(s ^ 2));
			var r3 = await (
				from a in option.AsTask
				select a ^ 2
			);
			var r4 = await (
				from a in option
				select Task.FromResult(a ^ 2)
			);
			var r5 = await (
				from a in option.AsTask
				select Task.FromResult(a ^ 2)
			);

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
		public void Select_With_None_Returns_None()
		{
			// Arrange
			var option = None<int>(new InvalidIntegerMsg());

			// Act
			var r0 = option.Select(s => s ^ 2);
			var r1 = from a in option
					 select a ^ 2;

			// Assert
			var n0 = r0.AssertNone();
			Assert.IsType<InvalidIntegerMsg>(n0);
			var n1 = r1.AssertNone();
			Assert.IsType<InvalidIntegerMsg>(n1);
		}

		[Fact]
		public async Task Async_Select_With_None_Returns_None()
		{
			// Arrange
			var option = None<int>(new InvalidIntegerMsg());

			// Act
			var r0 = await option.AsTask.Select(s => s ^ 2);
			var r1 = await option.Select(s => Task.FromResult(s ^ 2));
			var r2 = await option.AsTask.Select(s => Task.FromResult(s ^ 2));
			var r3 = await (
				from a in option.AsTask
				select a ^ 2
			);
			var r4 = await (
				from a in option
				select Task.FromResult(a ^ 2)
			);
			var r5 = await (
				from a in option.AsTask
				select Task.FromResult(a ^ 2)
			);

			// Assert
			var n0 = r0.AssertNone();
			Assert.IsType<InvalidIntegerMsg>(n0);
			var n1 = r1.AssertNone();
			Assert.IsType<InvalidIntegerMsg>(n1);
			var n2 = r2.AssertNone();
			Assert.IsType<InvalidIntegerMsg>(n2);
			var n3 = r3.AssertNone();
			Assert.IsType<InvalidIntegerMsg>(n3);
			var n4 = r4.AssertNone();
			Assert.IsType<InvalidIntegerMsg>(n4);
			var n5 = r5.AssertNone();
			Assert.IsType<InvalidIntegerMsg>(n5);
		}

		public record InvalidIntegerMsg : IMsg { }
	}
}
