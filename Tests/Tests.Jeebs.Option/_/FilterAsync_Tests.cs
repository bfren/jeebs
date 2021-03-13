// Jeebs Unit Tests
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System;
using System.Threading.Tasks;
using Jeebs.OptionMsg;
using NSubstitute;
using Xunit;
using static F.OptionF;

namespace Jeebs.Option_Tests
{
	public class FilterAsync_Tests
	{
		[Fact]
		public async Task When_Some_And_Predicate_True_Returns_Value()
		{
			// Arrange
			var value = F.Rnd.Int;
			var option = Return(value);
			Task<bool> predicate(int x) => Task.FromResult(x == value);

			// Act
			var result = await option.FilterAsync(predicate);

			// Assert
			var some = Assert.IsType<Some<int>>(result);
			Assert.Equal(value, some.Value);
		}

		[Fact]
		public async Task When_Some_And_Predicate_False_Returns_None_With_PredicateWasFalseMsg()
		{
			// Arrange
			var value = F.Rnd.Int;
			var option = Return(value);
			Task<bool> predicate(int x) => Task.FromResult(x != value);

			// Act
			var result = await option.FilterAsync(predicate);

			// Assert
			var none = Assert.IsType<None<int>>(result);
			Assert.IsType<PredicateWasFalseMsg>(none.Reason);
		}

		[Fact]
		public async Task When_None_Returns_None_With_Original_Reason()
		{
			// Arrange
			var reason = new TestMsg();
			var option = None<int>(reason);
			var predicate = Substitute.For<Func<int, Task<bool>>>();

			// Act
			var result = await option.FilterAsync(predicate);

			// Assert
			var none = Assert.IsType<None<int>>(result);
			Assert.Same(reason, none.Reason);
		}

		public record TestMsg : IMsg { }
	}
}
