// Jeebs Unit Tests
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System;
using System.Threading.Tasks;
using Jeebs;
using Jeebs.Exceptions;
using NSubstitute;
using Xunit;
using static F.OptionF;
using static F.OptionF.Msg;

namespace Jeebs_Tests
{
	public abstract class UnwrapSingleAsync_Tests
	{
		public abstract Task Test00_If_Unknown_Option_Returns_None_With_UnhandledExceptionMsg();

		protected static async Task Test00(Func<Task<Option<int>>, Task<Option<int>>> act)
		{
			// Arrange
			var option = new FakeOption();

			// Act
			var result = await act(option.AsTask);

			// Assert
			var none = result.AssertNone();
			var msg = Assert.IsType<UnhandledExceptionMsg>(none);
			Assert.IsType<UnknownOptionException>(msg.Exception);
		}

		public abstract Task Test01_None_Returns_None();

		protected static async Task Test01(Func<Task<Option<int>>, Task<Option<int>>> act)
		{
			// Arrange
			var option = Create.None<int>();

			// Act
			var result = await act(option.AsTask);

			// Assert
			result.AssertNone();
		}

		public abstract Task Test02_None_With_Reason_Returns_None_With_Reason();

		protected static async Task Test02(Func<Task<Option<int>>, Task<Option<int>>> act)
		{
			// Arrange
			var reason = new TestMsg();
			var option = None<int>(reason);

			// Act
			var result = await act(option.AsTask);

			// Assert
			var none = result.AssertNone();
			Assert.Same(reason, none);
		}

		public abstract Task Test03_No_Items_Returns_None_With_UnwrapSingleNoItemsMsg();

		protected static async Task Test03(Func<Task<Option<int[]>>, Task<Option<int>>> act)
		{
			// Arrange
			var empty = Array.Empty<int>();
			var option = Return(empty);

			// Act
			var result = await act(option.AsTask);

			// Assert
			var none = result.AssertNone();
			Assert.IsType<UnwrapSingleNoItemsMsg>(none);
		}

		public abstract Task Test04_No_Items_Runs_NoItems();

		protected static async Task Test04(Func<Task<Option<int[]>>, Func<IMsg>?, Task<Option<int>>> act)
		{
			// Arrange
			var empty = Array.Empty<int>();
			var option = Return(empty);
			var noItems = Substitute.For<Func<IMsg>>();

			// Act
			await act(option.AsTask, noItems);

			// Assert
			noItems.Received().Invoke();
		}

		public abstract Task Test05_Too_Many_Items_Returns_None_With_UnwrapSingleTooManyItemsErrorMsg();

		protected static async Task Test05(Func<Task<Option<int[]>>, Task<Option<int>>> act)
		{
			// Arrange
			var list = new[] { F.Rnd.Int, F.Rnd.Int };
			var option = Return(list);

			// Act
			var result = await act(option.AsTask);

			// Assert
			var none = result.AssertNone();
			Assert.IsType<UnwrapSingleTooManyItemsErrorMsg>(none);
		}

		public abstract Task Test06_Too_Many_Items_Runs_TooMany();

		protected static async Task Test06(Func<Task<Option<int[]>>, Func<IMsg>?, Task<Option<int>>> act)
		{
			// Arrange
			var list = new[] { F.Rnd.Int, F.Rnd.Int };
			var option = Return(list);
			var tooMany = Substitute.For<Func<IMsg>>();

			// Act
			await act(option.AsTask, tooMany);

			// Assert
			tooMany.Received().Invoke();
		}

		public abstract Task Test07_Not_A_List_Returns_None_With_UnwrapSingleNotAListMsg();

		protected static async Task Test07(Func<Task<Option<int>>, Task<Option<int>>> act)
		{
			// Arrange
			var value = F.Rnd.Int;
			var option = Return(value);

			// Act
			var result = await act(option.AsTask);

			// Assert
			var none = result.AssertNone();
			Assert.IsType<UnwrapSingleNotAListMsg>(none);
		}

		public abstract Task Test08_Not_A_List_Runs_NotAList();

		protected static async Task Test08(Func<Task<Option<int>>, Func<IMsg>?, Task<Option<int>>> act)
		{
			// Arrange
			var value = F.Rnd.Int;
			var option = Return(value);
			var notAList = Substitute.For<Func<IMsg>>();

			// Act
			var result = await act(option.AsTask, notAList);

			// Assert
			notAList.Received().Invoke();
		}

		public abstract Task Test09_Incorrect_Type_Returns_None_With_UnwrapSingleIncorrectTypeErrorMsg();

		protected static async Task Test09(Func<Task<Option<int[]>>, Task<Option<string>>> act)
		{
			// Arrange
			var value = F.Rnd.Int;
			var list = new[] { value };
			var option = Return(list);

			// Act
			var result = await act(option.AsTask);

			// Assert
			var none = result.AssertNone();
			Assert.IsType<UnwrapSingleIncorrectTypeErrorMsg>(none);
		}

		public abstract Task Test10_List_With_Single_Item_Returns_Single();

		protected static async Task Test10(Func<Task<Option<int[]>>, Task<Option<int>>> act)
		{
			// Arrange
			var value = F.Rnd.Int;
			var list = new[] { value };
			var option = Return(list);

			// Act
			var result = await act(option.AsTask);

			// Assert
			Assert.Equal(value, result);
		}

		public record FakeOption : Option<int> { }

		public record TestMsg : IMsg { }
	}
}
