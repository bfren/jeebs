// Jeebs Unit Tests
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Jeebs.Exceptions;
using NSubstitute;
using Xunit;
using Jeebs;
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
			var none = Assert.IsType<None<int>>(result);
			var msg = Assert.IsType<UnhandledExceptionMsg>(none.Reason);
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
			Assert.IsType<None<int>>(result);
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
			var none = Assert.IsType<None<int>>(result);
			Assert.Same(reason, none.Reason);
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
			var none = Assert.IsType<None<int>>(result);
			Assert.IsType<UnwrapSingleNoItemsMsg>(none.Reason);
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
			var list = (IEnumerable<int>)new[] { F.Rnd.Int, F.Rnd.Int };
			var option = Return(list).AsTask;

			// Act
			var result = await UnwrapAsync(option, x => x.Single<int>());

			// Assert
			var none = Assert.IsType<None<int>>(result);
			Assert.IsType<UnwrapSingleTooManyItemsErrorMsg>(none.Reason);
		}

		[Fact]
		public async Task Too_Many_Items_Runs_TooMany()
		{
			// Arrange
			var list = (IEnumerable<int>)new[] { F.Rnd.Int, F.Rnd.Int };
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
			var value = F.Rnd.Int;
			var option = Return(value).AsTask;

			// Act
			var result = await UnwrapAsync(option, x => x.Single<int>());

			// Assert
			var none = Assert.IsType<None<int>>(result);
			Assert.IsType<UnwrapSingleNotAListMsg>(none.Reason);
		}

		[Fact]
		public async Task Not_A_List_Runs_NotAList()
		{
			// Arrange
			var value = F.Rnd.Int;
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
			var value = F.Rnd.Int;
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
