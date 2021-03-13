// Jeebs Unit Tests
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System;
using System.Collections.Generic;
using Jeebs;
using Jeebs.Exceptions;
using NSubstitute;
using Xunit;
using static F.OptionF;
using static F.OptionF.Msg;

namespace F.OptionF_Tests
{
	public class UnwrapSingle_Tests
	{
		[Fact]
		public void If_Unknown_Option_Returns_None_With_UnhandledExceptionMsg()
		{
			// Arrange
			var option = new FakeOption();

			// Act
			var result = UnwrapSingle<int, int>(option, null, null, null);

			// Assert
			var none = Assert.IsType<None<int>>(result);
			var msg = Assert.IsType<UnhandledExceptionMsg>(none.Reason);
			Assert.IsType<UnknownOptionException>(msg.Exception);
		}

		[Fact]
		public void None_Returns_None()
		{
			// Arrange
			var option = None<int>(true);

			// Act
			var result = UnwrapSingle<int, int>(option, null, null, null);

			// Assert
			Assert.IsType<None<int>>(result);
		}

		[Fact]
		public void None_With_Reason_Returns_None_With_Reason()
		{
			// Arrange
			var reason = new TestMsg();
			var option = None<int>(reason);

			// Act
			var result = UnwrapSingle<int, int>(option, null, null, null);

			// Assert
			var none = Assert.IsType<None<int>>(result);
			Assert.Same(reason, none.Reason);
		}

		[Fact]
		public void No_Items_Returns_None_With_UnwrapSingleNoItemsMsg()
		{
			// Arrange
			var empty = (IEnumerable<int>)Array.Empty<int>();
			var option = Return(empty);

			// Act
			var result = UnwrapSingle<IEnumerable<int>, int>(option, null, null, null);

			// Assert
			var none = Assert.IsType<None<int>>(result);
			Assert.IsType<UnwrapSingleNoItemsMsg>(none.Reason);
		}

		[Fact]
		public void No_Items_Runs_NoItems()
		{
			// Arrange
			var empty = (IEnumerable<int>)Array.Empty<int>();
			var option = Return(empty);
			var noItems = Substitute.For<Func<IMsg>>();

			// Act
			UnwrapSingle<IEnumerable<int>, int>(option, noItems: noItems, null, null);

			// Assert
			noItems.Received().Invoke();
		}

		[Fact]
		public void Too_Many_Items_Returns_None_With_UnwrapSingleTooManyItemsErrorMsg()
		{
			// Arrange
			var list = (IEnumerable<int>)new[] { Rnd.Int, Rnd.Int };
			var option = Return(list);

			// Act
			var result = UnwrapSingle<IEnumerable<int>, int>(option, null, null, null);

			// Assert
			var none = Assert.IsType<None<int>>(result);
			Assert.IsType<UnwrapSingleTooManyItemsErrorMsg>(none.Reason);
		}

		[Fact]
		public void Too_Many_Items_Runs_TooMany()
		{
			// Arrange
			var list = (IEnumerable<int>)new[] { Rnd.Int, Rnd.Int };
			var option = Return(list);
			var tooMany = Substitute.For<Func<IMsg>>();

			// Act
			UnwrapSingle<IEnumerable<int>, int>(option, null, tooMany: tooMany, null);

			// Assert
			tooMany.Received().Invoke();
		}

		[Fact]
		public void Not_A_List_Returns_None_With_UnwrapSingleNotAListMsg()
		{
			// Arrange
			var value = Rnd.Int;
			var option = Return(value);

			// Act
			var result = UnwrapSingle<int, int>(option, null, null, null);

			// Assert
			var none = Assert.IsType<None<int>>(result);
			Assert.IsType<UnwrapSingleNotAListMsg>(none.Reason);
		}

		[Fact]
		public void Not_A_List_Runs_NotAList()
		{
			// Arrange
			var value = Rnd.Int;
			var option = Return(value);
			var notAList = Substitute.For<Func<IMsg>>();

			// Act
			var result = UnwrapSingle<int, int>(option, null, null, notAList: notAList);

			// Assert
			notAList.Received().Invoke();
		}

		[Fact]
		public void List_With_Single_Item_Returns_Single()
		{
			// Arrange
			var value = Rnd.Int;
			var list = (IEnumerable<int>)new[] { value };
			var option = Return(list);

			// Act
			var result = UnwrapSingle<IEnumerable<int>, int>(option, null, null, null);

			// Assert
			Assert.Equal(value, result);
		}

		public class FakeOption : Option<int> { }

		public record TestMsg : IMsg { }
	}
}
