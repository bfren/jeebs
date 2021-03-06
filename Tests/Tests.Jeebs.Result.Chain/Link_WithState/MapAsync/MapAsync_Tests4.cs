using System;
using System.Threading.Tasks;
using Xunit;

namespace Jeebs.Link_Tests.WithState
{
	public partial class MapAsync_Tests
	{
		[Fact]
		public void IOk_ValueType_WithState_Input_When_IOk_Maps_To_Next_Type()
		{
			// Arrange
			var state = F.Rnd.Int;
			var chain = Chain.Create(state);
			static async Task<IR<string, int>> f(IOk<bool, int> r) => r.Ok<string>();

			// Act
			var next = chain.Link().MapAsync(f).Await();

			// Assert
			var ok = Assert.IsAssignableFrom<IOk<string, int>>(next);
			Assert.Equal(state, ok.State);
		}

		[Fact]
		public void IOk_ValueType_WithState_Input_When_IOk_Catches_Exception()
		{
			// Arrange
			var state = F.Rnd.Int;
			var chain = Chain.Create(state);
			var error = F.Rnd.Str;
			async Task<IR<string, int>> f(IOk<bool, int> _) => throw new Exception(error);

			// Act
			var next = chain.Link().MapAsync(f).Await();
			var msg = next.Messages.Get<Jm.Link.LinkExceptionMsg>();

			// Assert
			var e = Assert.IsAssignableFrom<IError<string, int>>(next);
			Assert.Equal(state, e.State);
			Assert.NotEmpty(msg);
		}

		[Fact]
		public void IOk_ValueType_WithState_Input_When_IError_Returns_IError()
		{
			// Arrange
			var state = F.Rnd.Int;
			var error = Chain.Create(state).Error();
			static async Task<IR<int, int>> f(IOk<bool, int> _) => throw new Exception();

			// Act
			var next = error.Link().MapAsync(f).Await();

			// Assert
			var e = Assert.IsAssignableFrom<IError<int, int>>(next);
			Assert.Equal(state, e.State);
			Assert.Same(error.Messages, next.Messages);
		}
	}
}
