using System;
using System.Threading.Tasks;
using Xunit;

namespace Jeebs.Link_Tests
{
	public partial class RunAsync_Tests : ILink_Run
	{
		[Fact]
		public void No_Input_When_IOk_Runs_Action()
		{
			// Arrange
			var chain = Chain.Create();
			var sideEffect = 1;
			async Task f() => sideEffect++;

			// Act
			var next = chain.Link().RunAsync(f).Await();

			// Assert
			Assert.Same(chain, next);
			Assert.Equal(2, sideEffect);
		}

		[Fact]
		public void No_Input_When_IOk_Catches_Exception()
		{
			// Arrange
			var chain = Chain.Create();
			var error = F.Rnd.Str;
			async Task f() => throw new Exception(error);

			// Act
			var next = chain.Link().RunAsync(f).Await();
			var msg = next.Messages.Get<Jm.Link.LinkExceptionMsg>();

			// Assert
			Assert.IsAssignableFrom<IError<bool>>(next);
			Assert.NotEmpty(msg);
		}

		[Fact]
		public void No_Input_When_IError_Returns_IError()
		{
			// Arrange
			var error = Chain.Create().Error();
			static async Task f() => throw new Exception();

			// Act
			var next = error.Link().RunAsync(f).Await();

			// Assert
			Assert.IsAssignableFrom<IError<bool>>(next);
			Assert.Same(error.Messages, next.Messages);
		}
	}
}
