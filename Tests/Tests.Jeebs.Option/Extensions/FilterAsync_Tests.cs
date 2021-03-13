// Jeebs Unit Tests
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System;
using System.Threading.Tasks;
using Jeebs.OptionMsg;
using NSubstitute;
using Xunit;
using Xunit.Sdk;
using static F.OptionF;

namespace Jeebs.OptionExtensions_Tests
{
	public class FilterAsync_Tests
	{
		[Fact]
		public async Task When_Some_And_Predicate_True_Returns_Value()
		{
			// Arrange
			var value = F.Rnd.Int;
			var task = Return(value).AsTask;
			bool syncPredicate(int x) => x == value;
			Task<bool> asyncPredicate(int x) => Task.FromResult(x == value);

			// Act
			var r0 = await OptionExtensions.DoFilterAsync(task, asyncPredicate, null);
			var r1 = await task.FilterAsync(syncPredicate);
			var r2 = await task.FilterAsync(asyncPredicate);

			// Assert
			var s0 = Assert.IsType<Some<int>>(r0);
			Assert.Equal(value, s0.Value);
			var s1 = Assert.IsType<Some<int>>(r1);
			Assert.Equal(value, s1.Value);
			var s2 = Assert.IsType<Some<int>>(r2);
			Assert.Equal(value, s2.Value);
		}

		[Fact]
		public async Task When_Some_And_Predicate_False_Returns_None_With_PredicateWasFalseMsg()
		{
			// Arrange
			var value = F.Rnd.Int;
			var task = Return(value).AsTask;
			bool syncPredicate(int x) => x != value;
			Task<bool> asyncPredicate(int x) => Task.FromResult(x != value);

			// Act
			var r0 = await OptionExtensions.DoFilterAsync(task, asyncPredicate, null);
			var r1 = await task.FilterAsync(syncPredicate);
			var r2 = await task.FilterAsync(asyncPredicate);

			// Assert
			var n0 = Assert.IsType<None<int>>(r0);
			Assert.IsType<PredicateWasFalseMsg>(n0.Reason);
			var n1 = Assert.IsType<None<int>>(r1);
			Assert.IsType<PredicateWasFalseMsg>(n1.Reason);
			var n2 = Assert.IsType<None<int>>(r2);
			Assert.IsType<PredicateWasFalseMsg>(n2.Reason);
		}

		[Fact]
		public async Task When_None_Returns_None_With_Original_Reason()
		{
			// Arrange
			var reason = new TestMsg();
			var task = None<int>(reason).AsTask;
			var syncPredicate = Substitute.For<Func<int, bool>>();
			var asyncPredicate = Substitute.For<Func<int, Task<bool>>>();

			// Act
			var r0 = await OptionExtensions.DoFilterAsync(task, asyncPredicate, null);
			var r1 = await task.FilterAsync(syncPredicate);
			var r2 = await task.FilterAsync(asyncPredicate);

			// Assert
			var n0 = Assert.IsType<None<int>>(r0);
			Assert.Same(reason, n0.Reason);
			var n1 = Assert.IsType<None<int>>(r1);
			Assert.Same(reason, n1.Reason);
			var n2 = Assert.IsType<None<int>>(r2);
			Assert.Same(reason, n2.Reason);
		}

		public record TestMsg : IMsg { }
	}
}
