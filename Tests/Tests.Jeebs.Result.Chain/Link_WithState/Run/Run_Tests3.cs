using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace Jeebs.LinkTests.WithState
{
	public partial class Run_Tests
	{
		[Fact]
		public void IOk_Value_Input_When_IOk_Runs_Action()
		{
			// Arrange
			const int state = 7;
			var chain = Chain.CreateV(true, state);
			var sideEffect = 1;
			void f(IOkV<bool> _) => sideEffect++;

			// Act
			var next = chain.Link().Run(f);

			// Assert
			Assert.Same(chain, next);
			Assert.Equal(2, sideEffect);
		}

		[Fact]
		public void IOk_Value_Input_When_IOk_Catches_Exception()
		{
			// Arrange
			const int state = 7;
			var chain = Chain.CreateV(true, state);
			const string error = "Error!";
			static void f(IOkV<bool> _) => throw new Exception(error);

			// Act
			var next = chain.Link().Run(f);
			var msg = next.Messages.Get<Jm.Link.LinkExceptionMsg>();

			// Assert
			var e = Assert.IsAssignableFrom<IError<bool, int>>(next);
			Assert.Equal(state, e.State);
			Assert.NotEmpty(msg);
			Assert.Equal($"{typeof(Exception)}: {error}", msg.Single().ToString());
		}

		[Fact]
		public void IOk_Value_Input_When_IError_Returns_IError()
		{
			// Arrange
			const int state = 7;
			var error = Chain.CreateV(true, state).Error();
			static void f(IOkV<bool> _) => throw new Exception();

			// Act
			var next = error.Link().Run(f);

			// Assert
			var e = Assert.IsAssignableFrom<IError<bool, int>>(next);
			Assert.Equal(state, e.State);
			Assert.Same(error.Messages, next.Messages);
		}
	}
}
