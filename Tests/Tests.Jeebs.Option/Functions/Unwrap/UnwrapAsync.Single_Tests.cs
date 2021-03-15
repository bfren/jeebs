// Jeebs Unit Tests
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Jeebs;
using Jeebs.Exceptions;
using NSubstitute;
using Xunit;
using static F.OptionF;
using static F.OptionF.Msg;

namespace F.OptionF_Tests
{
	public class UnwrapSingleAsync_Tests
	{
		[Fact]
		public async Task If_Unknown_Option_Returns_None_With_UnhandledExceptionMsg()
		{
			// Arrange
			var option = new FakeOption().AsTask;

			// Act
			var result = await UnwrapAsync(option, x => x.Single<int>());

			// Assert
			var none = result.AssertNone();
			var msg = Assert.IsType<UnhandledExceptionMsg>(none);
			Assert.IsType<UnknownOptionException>(msg.Exception);
		}

		[Fact]
		public async Task None_Returns_None()
		{
			// Arrange
			var option = None<int>(true).AsTask;

			// Act
			var result = await UnwrapAsync(option, x => x.Single<int>());

			// Assert
			result.AssertNone();
		}

		[Fact]
		public async Task None_With_Reason_Returns_None_With_Reason()
		{
			// Arrange
			var reason = new TestMsg();
			var option = None<int>(reason).AsTask;

			// Act
			var result = await UnwrapAsync(option, x => x.Single<int>());

			// Assert
			var none = result.AssertNone();
			Assert.Same(reason, none);
		}

		[Fact]
		public async Task No_Items_Returns_None_With_UnwrapSingleNoItemsMsg()
		{
			// Arrange
			var empty = (IEnumerable<int>)Array.Empty<int>();
			var option = Return(empty).AsTask;

			// Act
			var result = await UnwrapAsync(option, x => x.Single<int>());

			// Assert
			var none = result.AssertNone();
			Assert.IsType<UnwrapSingleNoItemsMsg>(none);
		}

		[Fact]
		public async Task No_Items_Runs_NoItems()
		{
			// Arrange
			var empty = (IEnumerable<int>)Array.Empty<int>();
			var option = Return(empty).AsTask;
			var noItems = Substitute.For<Func<IMsg>>();

			// Act
			await UnwrapAsync(option, x => x.Single<int>(noItems: noItems));

			// Assert
			noItems.Received().Invoke();
		}

		[Fact]
		public async Task Too_Many_Items_Returns_None_With_UnwrapSingleTooManyItemsErrorMsg()
		{
			// Arrange
			var list = (IEnumerable<int>)new[] { Rnd.Int, Rnd.Int };
			var option = Return(list).AsTask;

			// Act
			var result = await UnwrapAsync(option, x => x.Single<int>());

			// Assert
			var none = result.AssertNone();
			Assert.IsType<UnwrapSingleTooManyItemsErrorMsg>(none);
		}

		[Fact]
		public async Task Too_Many_Items_Runs_TooMany()
		{
			// Arrange
			var list = (IEnumerable<int>)new[] { Rnd.Int, Rnd.Int };
			var option = Return(list).AsTask;
			var tooMany = Substitute.For<Func<IMsg>>();

			// Act
			await UnwrapAsync(option, x => x.Single<int>(tooMany: tooMany));

			// Assert
			tooMany.Received().Invoke();
		}

		[Fact]
		public async Task Not_A_List_Returns_None_With_UnwrapSingleNotAListMsg()
		{
			// Arrange
			var value = Rnd.Int;
			var option = Return(value).AsTask;

			// Act
			var result = await UnwrapAsync(option, x => x.Single<int>());

			// Assert
			var none = result.AssertNone();
			Assert.IsType<UnwrapSingleNotAListMsg>(none);
		}

		[Fact]
		public async Task Not_A_List_Runs_NotAList()
		{
			// Arrange
			var value = Rnd.Int;
			var option = Return(value).AsTask;
			var notAList = Substitute.For<Func<IMsg>>();

			// Act
			await UnwrapAsync(option, x => x.Single<int>(notAList: notAList));

			// Assert
			notAList.Received().Invoke();
		}

		[Fact]
		public async Task List_With_Single_Item_Returns_Single()
		{
			// Arrange
			var value = Rnd.Int;
			var list = (IEnumerable<int>)new[] { value };
			var option = Return(list).AsTask;

			// Act
			var result = await UnwrapAsync(option, x => x.Single<int>());

			// Assert
			Assert.Equal(value, result);
		}

		public class FakeOption : Option<int> { }

		public record TestMsg : IMsg { }
	}
}
