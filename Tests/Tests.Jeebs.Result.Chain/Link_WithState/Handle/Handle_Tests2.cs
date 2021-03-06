using System;
using System.Threading.Tasks;
using Xunit;

namespace Jeebs.Link_Tests.WithState
{
	public partial class Handle_Tests
	{
		[Fact]
		public void Generic_AsyncHandler_Returns_Original_Link()
		{
			// Arrange
			var state = F.Rnd.Int;
			var link = Chain.Create(state).Link();
			static async Task h0(IR<bool> _, Exception __) { }
			static async Task h1(IR<bool, int> _, Exception __) { }

			// Act
			var n0 = link.Handle().With(h0);
			var n1 = link.Handle().With(h1);

			// Assert
			Assert.Same(link, n0);
			Assert.Same(link, n1);
		}

		[Fact]
		public void Generic_AsyncHandler_Runs_For_Any_Exception()
		{
			// Arrange
			var state = F.Rnd.Int;
			var chain = Chain.Create(state);
			var sideEffect = 1;
			async Task h0(IR<bool> _, Exception __) => sideEffect++;
			async Task h1(IR<bool, int> _, Exception __) => sideEffect++;
			static void throwGeneric() => throw new Exception();
			static void throwOther() => throw new DivideByZeroException();

			// Act
			chain.Link().Handle().With(h0).Run(throwGeneric);
			chain.Link().Handle().With(h0).Run(throwOther);
			chain.Link().Handle().With(h1).Run(throwGeneric);
			chain.Link().Handle().With(h1).Run(throwOther);

			// Assert
			Assert.Equal(5, sideEffect);
		}
	}
}
