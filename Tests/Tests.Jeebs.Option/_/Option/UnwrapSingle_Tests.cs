// Jeebs Unit Tests
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System;
using System.Collections.Generic;
using Jeebs.Exceptions;
using NSubstitute;
using Xunit;
using static F.OptionF;
using static F.OptionF.Msg;

namespace Jeebs.Option_Tests
{
	public class UnwrapSingle_Tests
	{
		[Fact]
		public void If_Unknown_Option_Returns_None_With_UnhandledExceptionMsg()
		{
			// Arrange
			var option = new FakeOption();

			// Act
			var result = option.DoUnwrapSingle<int>(null, null, null);

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
			var r0 = option.DoUnwrapSingle<int>(null, null, null);
			var r1 = option.UnwrapSingle<int>();

			// Assert
			Assert.IsType<None<int>>(r0);
			Assert.IsType<None<int>>(r1);
		}

		[Fact]
		public void None_With_Reason_Returns_None_With_Reason()
		{
			// Arrange
			var reason = new TestMsg();
			var option = None<int>(reason);

			// Act
			var r0 = option.DoUnwrapSingle<int>(null, null, null);
			var r1 = option.UnwrapSingle<int>();

			// Assert
			var n0 = Assert.IsType<None<int>>(r0);
			Assert.Same(reason, n0.Reason);
			var n1 = Assert.IsType<None<int>>(r1);
			Assert.Same(reason, n1.Reason);
		}

		[Fact]
		public void No_Items_Returns_None_With_UnwrapSingleNoItemsMsg()
		{
			// Arrange
			var empty = (IEnumerable<int>)Array.Empty<int>();
			var option = Return(empty);

			// Act
			var r0 = option.DoUnwrapSingle<int>(null, null, null);
			var r1 = option.UnwrapSingle<int>();

			// Assert
			var n0 = Assert.IsType<None<int>>(r0);
			Assert.IsType<UnwrapSingleNoItemsMsg>(n0.Reason);
			var n1 = Assert.IsType<None<int>>(r1);
			Assert.IsType<UnwrapSingleNoItemsMsg>(n1.Reason);
		}

		[Fact]
		public void No_Items_Runs_NoItems()
		{
			// Arrange
			var empty = (IEnumerable<int>)Array.Empty<int>();
			var option = Return(empty);
			var noItems = Substitute.For<Func<IMsg>>();

			// Act
			option.DoUnwrapSingle<int>(noItems: noItems, null, null);
			option.UnwrapSingle<int>(noItems: noItems);

			// Assert
			noItems.Received(2).Invoke();
		}

		[Fact]
		public void Too_Many_Items_Returns_None_With_UnwrapSingleTooManyItemsErrorMsg()
		{
			// Arrange
			var list = (IEnumerable<int>)new[] { F.Rnd.Int, F.Rnd.Int };
			var option = Return(list);

			// Act
			var r0 = option.DoUnwrapSingle<int>(null, null, null);
			var r1 = option.UnwrapSingle<int>();

			// Assert
			var n0 = Assert.IsType<None<int>>(r0);
			Assert.IsType<UnwrapSingleTooManyItemsErrorMsg>(n0.Reason);
			var n1 = Assert.IsType<None<int>>(r1);
			Assert.IsType<UnwrapSingleTooManyItemsErrorMsg>(n1.Reason);
		}

		[Fact]
		public void Too_Many_Items_Runs_TooMany()
		{
			// Arrange
			var list = (IEnumerable<int>)new[] { F.Rnd.Int, F.Rnd.Int };
			var option = Return(list);
			var tooMany = Substitute.For<Func<IMsg>>();

			// Act
			option.DoUnwrapSingle<int>(null, tooMany: tooMany, null);
			option.UnwrapSingle<int>(tooMany: tooMany);

			// Assert
			tooMany.Received(2).Invoke();
		}

		[Fact]
		public void Not_A_List_Returns_None_With_UnwrapSingleNotAListMsg()
		{
			// Arrange
			var value = F.Rnd.Int;
			var option = Return(value);

			// Act
			var r0 = option.DoUnwrapSingle<int>(null, null, null);
			var r1 = option.UnwrapSingle<int>();

			// Assert
			var n0 = Assert.IsType<None<int>>(r0);
			Assert.IsType<UnwrapSingleNotAListMsg>(n0.Reason);
			var n1 = Assert.IsType<None<int>>(r1);
			Assert.IsType<UnwrapSingleNotAListMsg>(n1.Reason);
		}

		[Fact]
		public void Not_A_List_Runs_NotAList()
		{
			// Arrange
			var value = F.Rnd.Int;
			var option = Return(value);
			var notAList = Substitute.For<Func<IMsg>>();

			// Act
			option.DoUnwrapSingle<int>(null, null, notAList: notAList);
			option.UnwrapSingle<int>(notAList: notAList);

			// Assert
			notAList.Received(2).Invoke();
		}

		[Fact]
		public void List_With_Single_Item_Returns_Single()
		{
			// Arrange
			var value = F.Rnd.Int;
			var list = (IEnumerable<int>)new[] { value };
			var option = Return(list);

			// Act
			var result = option.UnwrapSingle<int>(null, null, null);

			// Assert
			Assert.Equal(value, result);
		}

		public class FakeOption : Option<int> { }

		public record TestMsg : IMsg { }
	}
}
