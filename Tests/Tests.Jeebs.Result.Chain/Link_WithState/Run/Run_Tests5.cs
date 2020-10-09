using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace Jeebs.Link_Tests.WithState
{
	public partial class Run_Tests
	{
		[Fact]
		public void IOk_Value_WithState_Input_When_IOk_Runs_Action()
		{
			// Arrange
			var state = F.Rand.Integer;
			var chain = Chain.CreateV(true, state);
			var sideEffect = 1;
			void f(IOkV<bool, int> _) => sideEffect++;

			// Act
			var next = chain.Link().Run(f);

			// Assert
			Assert.Same(chain, next);
			Assert.Equal(2, sideEffect);
		}

		[Fact]
		public void IOk_Value_WithState_Input_When_IOk_Catches_Exception()
		{
			// Arrange
			var state = F.Rand.Integer;
			var chain = Chain.CreateV(true, state);
			var error = F.Rand.String;
			void f(IOkV<bool, int> _) => throw new Exception(error);

			// Act
			var next = chain.Link().Run(f);
			var msg = next.Messages.Get<Jm.Link.LinkExceptionMsg>();

			// Assert
			var e = Assert.IsAssignableFrom<IError<bool, int>>(next);
			Assert.Equal(state, e.State);
			Assert.NotEmpty(msg);
		}

		[Fact]
		public void IOk_Value_WithState_Input_When_IError_Returns_IError()
		{
			// Arrange
			var state = F.Rand.Integer;
			var error = Chain.CreateV(true, state).Error();
			static void f(IOkV<bool, int> _) => throw new Exception();

			// Act
			var next = error.Link().Run(f);

			// Assert
			var e = Assert.IsAssignableFrom<IError<bool, int>>(next);
			Assert.Equal(state, e.State);
			Assert.Same(error.Messages, next.Messages);
		}
	}
}
