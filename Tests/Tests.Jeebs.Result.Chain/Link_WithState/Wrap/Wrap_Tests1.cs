using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace Jeebs.LinkTests.WithState
{
	public partial class Wrap_Tests
	{
		[Fact]
		public void Func_Input_When_IOk_Wraps_Value()
		{
			// Arrange
			const int value = 18;
			const int state = 7;
			static int f() => value;
			var r = Chain.Create(state);

			// Act
			var next = r.Link().Wrap(f);

			// Assert
			var okV = Assert.IsAssignableFrom<IOkV<int, int>>(next);
			Assert.Equal(value, okV.Value);
			Assert.Equal(state, okV.State);
		}

		[Fact]
		public void Func_Input_When_IOk_Catches_Exception_Returns_IError()
		{
			// Arrange
			const int state = 7;
			var r = Chain.Create(state);
			const string error = "Ooops!";
			static int f() => throw new Exception(error);

			// Act
			var next = r.Link().Wrap(f);
			var msg = next.Messages.Get<Jm.ChainExceptionMsg>();

			// Assert
			var e = Assert.IsAssignableFrom<IError<int, int>>(next);
			Assert.Equal(state, e.State);
			Assert.NotEmpty(msg);
			Assert.Equal($"{typeof(Exception)}: {error}", msg.Single().ToString());
		}

		[Fact]
		public void Func_Input_When_IError_Returns_IError()
		{
			// Arrange
			const int state = 7;
			var r = Chain.Create(state).Error();
			static int f() => throw new Exception();

			// Act
			var next = r.Link().Wrap(f);

			// Assert
			var e = Assert.IsAssignableFrom<IError<int, int>>(next);
			Assert.Equal(state, e.State);
		}
	}
}
