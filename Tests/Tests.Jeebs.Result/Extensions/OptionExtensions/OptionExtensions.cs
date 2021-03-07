// Jeebs Unit Tests
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using NSubstitute;
using Xunit;

namespace Jeebs.OptionExtensions_Tests
{
	public class ToResult_Tests
	{
		[Fact]
		public void Some_Returns_OkV()
		{
			// Arrange
			var value = F.Rnd.Int;
			var some = Option.Wrap(value);

			// Act
			var result = some.ToResult();

			// Assert
			var okV = Assert.IsAssignableFrom<IOkV<int>>(result);
			Assert.Equal(value, okV.Value);
		}

		[Fact]
		public void Some_With_State_Returns_OkV_With_State()
		{
			// Arrange
			var value = F.Rnd.Int;
			var state = F.Rnd.Int;
			var some = Option.Wrap(value);

			// Act
			var result = some.ToResult(state);

			// Assert
			var okV = Assert.IsAssignableFrom<IOkV<int, int>>(result);
			Assert.Equal(value, okV.Value);
			Assert.Equal(state, okV.State);
		}

		[Fact]
		public void None_Returns_Error()
		{
			// Arrange
			var some = Option.None<int>();

			// Act
			var result = some.ToResult();

			// Assert
			Assert.IsAssignableFrom<IError<int>>(result);
		}

		[Fact]
		public void None_With_State_Returns_Error_With_State()
		{
			// Arrange
			var state = F.Rnd.Int;
			var some = Option.None<int>();

			// Act
			var result = some.ToResult(state);

			// Assert
			var error = Assert.IsAssignableFrom<IError<int, int>>(result);
			Assert.Equal(state, error.State);
		}

		[Fact]
		public void None_With_Reason_Returns_Error_With_Msg()
		{
			// Arrange
			var reason = Substitute.For<IMsg>();
			var some = Option.None<int>().AddReason(reason);

			// Act
			var result = some.ToResult();

			// Assert
			var error = Assert.IsAssignableFrom<IError<int>>(result);
			Assert.Contains(error.Messages.GetEnumerable(), m => m == reason);
		}
	}
}
