using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Jeebs.Link_Tests.WithState
{
	public partial class MapAsync_Tests
	{
		[Fact]
		public void IOk_Input_Maps_To_Next_Type()
		{
			// Arrange
			const int state = 7;
			var chain = Chain.Create(state);
			static async Task<IR<int>> f(IOk r) => r.WithState(state).Ok<int>();

			// Act
			var next = chain.Link().MapAsync(f).Await();

			// Assert
			var ok = Assert.IsAssignableFrom<IOk<int, int>>(next);
			Assert.Equal(state, ok.State);
		}

		[Fact]
		public void IOk_Input_When_IOk_Catches_Exception()
		{
			// Arrange
			const int state = 7;
			var chain = Chain.Create(state);
			const string error = "Error!";
			static async Task<IR<int>> f(IOk _) => throw new Exception(error);

			// Act
			var next = chain.Link().MapAsync(f).Await();
			var msg = next.Messages.Get<Jm.Link.LinkExceptionMsg>();

			// Assert
			var e = Assert.IsAssignableFrom<IError<int, int>>(next);
			Assert.Equal(state, e.State);
			Assert.NotEmpty(msg);
		}

		[Fact]
		public void IOk_Input_When_IError_Returns_IError()
		{
			// Arrange
			const int state = 7;
			var error = Chain.Create(state).Error();
			static async Task<IR<int>> f(IOk _) => throw new Exception();

			// Act
			var next = error.Link().MapAsync(f).Await();

			// Assert
			var e = Assert.IsAssignableFrom<IError<int, int>>(next);
			Assert.Equal(state, e.State);
			Assert.Same(error.Messages, next.Messages);
		}
	}
}
