// Jeebs Unit Tests
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using NSubstitute;
using Xunit;

namespace Jeebs.Option_Tests
{
	public class DoUnwrapSingle_Tests
	{
		[Fact]
		public void If_Unknown_Option_Returns_None_With_UnhandledExceptionMsg()
		{
			// Arrange
			var option = new FakeOption();

			// Act
			var result = option.DoUnwrapSingle<int>();

			// Assert
			var none = Assert.IsType<None<int>>(result);
			var msg = Assert.IsType<Jm.Option.UnhandledExceptionMsg>(none.Reason);
			Assert.IsType<Jx.Option.UnknownOptionException>(msg.Exception);
		}

		[Fact]
		public void None_Returns_None()
		{
			// Arrange
			var option = Option.None<int>(true);

			// Act
			var r0 = option.DoUnwrapSingle<int>();
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
			var option = Option.None<int>(reason);

			// Act
			var r0 = option.DoUnwrapSingle<int>();
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
			var option = Option.Wrap(empty);

			// Act
			var r0 = option.DoUnwrapSingle<int>();
			var r1 = option.UnwrapSingle<int>();

			// Assert
			var n0 = Assert.IsType<None<int>>(r0);
			Assert.IsType<Jm.Option.UnwrapSingleNoItemsMsg>(n0.Reason);
			var n1 = Assert.IsType<None<int>>(r1);
			Assert.IsType<Jm.Option.UnwrapSingleNoItemsMsg>(n1.Reason);
		}

		[Fact]
		public void No_Items_Runs_NoItems()
		{
			// Arrange
			var empty = (IEnumerable<int>)Array.Empty<int>();
			var option = Option.Wrap(empty);
			var noItems = Substitute.For<Func<IMsg>>();

			// Act
			option.DoUnwrapSingle<int>(noItems: noItems);
			option.UnwrapSingle<int>(noItems: noItems);

			// Assert
			noItems.Received(2).Invoke();
		}

		[Fact]
		public void Too_Many_Items_Returns_None_With_UnwrapSingleTooManyItemsErrorMsg()
		{
			// Arrange
			var list = (IEnumerable<int>)new[] { F.Rnd.Int, F.Rnd.Int };
			var option = Option.Wrap(list);

			// Act
			var r0 = option.DoUnwrapSingle<int>();
			var r1 = option.UnwrapSingle<int>();

			// Assert
			var n0 = Assert.IsType<None<int>>(r0);
			Assert.IsType<Jm.Option.UnwrapSingleTooManyItemsErrorMsg>(n0.Reason);
			var n1 = Assert.IsType<None<int>>(r1);
			Assert.IsType<Jm.Option.UnwrapSingleTooManyItemsErrorMsg>(n1.Reason);
		}

		[Fact]
		public void Too_Many_Items_Runs_TooMany()
		{
			// Arrange
			var list = (IEnumerable<int>)new[] { F.Rnd.Int, F.Rnd.Int };
			var option = Option.Wrap(list);
			var tooMany = Substitute.For<Func<IMsg>>();

			// Act
			option.DoUnwrapSingle<int>(tooMany: tooMany);
			option.UnwrapSingle<int>(tooMany: tooMany);

			// Assert
			tooMany.Received(2).Invoke();
		}

		[Fact]
		public void Not_A_List_Returns_None_With_UnwrapSingleNotAListMsg()
		{
			// Arrange
			var value = F.Rnd.Int;
			var option = Option.Wrap(value);

			// Act
			var r0 = option.DoUnwrapSingle<int>();
			var r1 = option.UnwrapSingle<int>();

			// Assert
			var n0 = Assert.IsType<None<int>>(r0);
			Assert.IsType<Jm.Option.UnwrapSingleNotAListMsg>(n0.Reason);
			var n1 = Assert.IsType<None<int>>(r1);
			Assert.IsType<Jm.Option.UnwrapSingleNotAListMsg>(n1.Reason);
		}

		[Fact]
		public void Not_A_List_Runs_NotAList()
		{
			// Arrange
			var value = F.Rnd.Int;
			var option = Option.Wrap(value);
			var notAList = Substitute.For<Func<IMsg>>();

			// Act
			option.DoUnwrapSingle<int>(notAList: notAList);
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
			var option = Option.Wrap(list);

			// Act
			var result = option.UnwrapSingle<int>();

			// Assert
			Assert.Equal(value, result);
		}

		public class FakeOption : Option<int> { }

		public record TestMsg : IMsg { }
	}
}
