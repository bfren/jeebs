// Jeebs Unit Tests
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

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
			var s0 = Assert.IsType<Some<int>>(r0);
			Assert.Equal(value ^ 2, s0.Value);
			var s1 = Assert.IsType<Some<int>>(r1);
			Assert.Equal(value ^ 2, s1.Value);
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
			var s0 = Assert.IsType<Some<int>>(r0);
			Assert.Equal(value ^ 2, s0.Value);
			var s1 = Assert.IsType<Some<int>>(r1);
			Assert.Equal(value ^ 2, s1.Value);
			var s2 = Assert.IsType<Some<int>>(r2);
			Assert.Equal(value ^ 2, s2.Value);
			var s3 = Assert.IsType<Some<int>>(r3);
			Assert.Equal(value ^ 2, s3.Value);
			var s4 = Assert.IsType<Some<int>>(r4);
			Assert.Equal(value ^ 2, s4.Value);
			var s5 = Assert.IsType<Some<int>>(r5);
			Assert.Equal(value ^ 2, s5.Value);
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
			var n0 = Assert.IsType<None<int>>(r0);
			Assert.IsType<InvalidIntegerMsg>(n0.Reason);
			var n1 = Assert.IsType<None<int>>(r1);
			Assert.IsType<InvalidIntegerMsg>(n1.Reason);
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
			var n0 = Assert.IsType<None<int>>(r0);
			Assert.IsType<InvalidIntegerMsg>(n0.Reason);
			var n1 = Assert.IsType<None<int>>(r1);
			Assert.IsType<InvalidIntegerMsg>(n1.Reason);
			var n2 = Assert.IsType<None<int>>(r2);
			Assert.IsType<InvalidIntegerMsg>(n2.Reason);
			var n3 = Assert.IsType<None<int>>(r3);
			Assert.IsType<InvalidIntegerMsg>(n3.Reason);
			var n4 = Assert.IsType<None<int>>(r4);
			Assert.IsType<InvalidIntegerMsg>(n4.Reason);
			var n5 = Assert.IsType<None<int>>(r5);
			Assert.IsType<InvalidIntegerMsg>(n5.Reason);
		}

		public record InvalidIntegerMsg : IMsg { }
	}
}
