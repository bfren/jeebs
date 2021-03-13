// Jeebs Unit Tests
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System;
using NSubstitute;
using Xunit;
using static F.OptionF;
using static F.OptionF.Msg;

namespace Jeebs.Option_Tests
{
	public class Filter_Tests
	{
		[Fact]
		public void When_Some_And_Predicate_True_Returns_Value()
		{
			// Arrange
			var value = F.Rnd.Int;
			var option = Return(value);

			// Act
			var result = option.Filter(x => x == value);

			// Assert
			var some = Assert.IsType<Some<int>>(result);
			Assert.Equal(value, some.Value);
		}

		[Fact]
		public void When_Some_And_Predicate_False_Returns_None_With_PredicateWasFalseMsg()
		{
			// Arrange
			var value = F.Rnd.Int;
			var option = Return(value);

			// Act
			var result = option.Filter(x => x != value);

			// Assert
			var none = Assert.IsType<None<int>>(result);
			Assert.IsType<PredicateWasFalseMsg>(none.Reason);
		}

		[Fact]
		public void When_None_Returns_None_With_Original_Reason()
		{
			// Arrange
			var reason = new TestMsg();
			var option = None<int>(reason);

			// Act
			var result = option.Filter(Substitute.For<Func<int, bool>>());

			// Assert
			var none = Assert.IsType<None<int>>(result);
			Assert.Same(reason, none.Reason);
		}

		public record TestMsg : IMsg { }
	}
}
