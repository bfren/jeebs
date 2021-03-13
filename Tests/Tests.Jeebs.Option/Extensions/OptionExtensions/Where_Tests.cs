// Jeebs Unit Tests
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System.Threading.Tasks;
using Jeebs.Linq;
using Xunit;
using static F.OptionF;

namespace Jeebs.OptionExtensions_Tests
{
	public class Where_Tests
	{
		[Fact]
		public void Where_True_With_Some_Returns_Some()
		{
			// Arrange
			var value = F.Rnd.Int;
			var option = Return(value);

			// Act
			var result = from a in option
						 where a == value
						 select a ^ 2;

			// Assert
			var some = Assert.IsType<Some<int>>(result);
			Assert.Equal(value ^ 2, some.Value);
		}

		[Fact]
		public async Task Async_Where_True_With_Some_Returns_Some()
		{
			// Arrange
			var value = F.Rnd.Int;
			var option = Task.FromResult(Return(value));

			// Act
			var result = await (
				from a in option
				where a == value
				select a ^ 2
			);

			// Assert
			var some = Assert.IsType<Some<int>>(result);
			Assert.Equal(value ^ 2, some.Value);
		}

		[Fact]
		public void Where_False_With_Some_Returns_None()
		{
			// Arrange
			var value = F.Rnd.Int;
			var option = Return(value);

			// Act
			var result = from a in option
						 where a != value
						 select a ^ 2;

			// Assert
			Assert.IsType<None<int>>(result);
		}

		[Fact]
		public async Task Async_Where_False_With_Some_Returns_None()
		{
			// Arrange
			var value = F.Rnd.Int;
			var option = Task.FromResult(Return(value));

			// Act
			var result = await (
				from a in option
				where a != value
				select a ^ 2
			);

			// Assert
			Assert.IsType<None<int>>(result);
		}

		[Fact]
		public void Where_With_None_Returns_None()
		{
			// Arrange
			var option = None<int>(new InvalidIntegerMsg());

			// Act
			var result = from a in option
						 where a == 0
						 select a ^ 2;

			// Assert
			var none = Assert.IsType<None<int>>(result);
			Assert.IsType<InvalidIntegerMsg>(none.Reason);
		}

		[Fact]
		public async Task Async_Where_With_None_Returns_None()
		{
			// Arrange
			var option = None<int>(new InvalidIntegerMsg()).AsTask;

			// Act
			var result = await (
				from a in option
				where a == 0
				select a ^ 2
			);

			// Assert
			var none = Assert.IsType<None<int>>(result);
			Assert.IsType<InvalidIntegerMsg>(none.Reason);
		}

		public record InvalidIntegerMsg : IMsg { }
	}
}
