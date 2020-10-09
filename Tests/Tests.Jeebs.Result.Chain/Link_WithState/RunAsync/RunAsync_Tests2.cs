using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Jeebs.Link_Tests.WithState
{
	public partial class RunAsync_Tests
	{
		[Fact]
		public void IOk_ValueType_Input_When_IOk_Runs_Action()
		{
			// Arrange
			var state = F.Rnd.Integer;
			var chain = Chain.Create(state);
			var sideEffect = 1;
			async Task f(IOk<bool> _) => sideEffect++;

			// Act
			var next = chain.Link().RunAsync(f).Await();

			// Assert
			Assert.Same(chain, next);
			Assert.Equal(2, sideEffect);
		}

		[Fact]
		public void IOk_ValueType_Input_When_IOk_Catches_Exception()
		{
			// Arrange
			var state = F.Rnd.Integer;
			var chain = Chain.Create(state);
			var error = F.Rnd.String;
			async Task f(IOk<bool> _) => throw new Exception(error);

			// Act
			var next = chain.Link().RunAsync(f).Await();
			var msg = next.Messages.Get<Jm.Link.LinkExceptionMsg>();

			// Assert
			var e = Assert.IsAssignableFrom<IError<bool, int>>(next);
			Assert.Equal(state, e.State);
			Assert.NotEmpty(msg);
		}

		[Fact]
		public void IOk_ValueType_Input_When_IError_Returns_IError()
		{
			// Arrange
			var state = F.Rnd.Integer;
			var error = Chain.Create(state).Error();
			static async Task f(IOk<bool> _) => throw new Exception();

			// Act
			var next = error.Link().RunAsync(f).Await();

			// Assert
			var e = Assert.IsAssignableFrom<IError<bool, int>>(next);
			Assert.Equal(state, e.State);
			Assert.Same(error.Messages, next.Messages);
		}
	}
}
