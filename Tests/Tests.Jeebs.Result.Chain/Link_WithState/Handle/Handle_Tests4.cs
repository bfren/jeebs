using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using NSubstitute;
using Xunit;

namespace Jeebs.HandleTests.WithState
{
	public partial class Handle_Tests
	{
		[Fact]
		public void Specific_AsyncHandler_Runs_For_That_Exception()
		{
			// Arrange
			const int state = 7;
			var chain = Chain.Create(state);
			var sideEffect = 1;
			async Task h0(IR<bool> _, DivideByZeroException __) => sideEffect++;
			async Task h1(IR<bool, int> _, DivideByZeroException __) => sideEffect++;
			static void throwException() => throw new DivideByZeroException();

			// Act
			chain.Link().Handle<DivideByZeroException>(h0).Run(throwException);
			chain.Link().Handle<DivideByZeroException>(h1).Run(throwException);

			// Assert
			Assert.Equal(3, sideEffect);
		}

		[Fact]
		public void Specific_AsyncHandler_Does_Not_Run_For_Other_Exceptions()
		{
			// Arrange
			const int state = 7;
			var chain = Chain.Create(state);
			var sideEffect = 1;
			async Task h0(IR<bool> _, DivideByZeroException __) => sideEffect++;
			async Task h1(IR<bool> _, DivideByZeroException __) => sideEffect++;
			static void throwException() => throw new ArithmeticException();

			// Act
			chain.Link().Handle<DivideByZeroException>(h0).Run(throwException);
			chain.Link().Handle<DivideByZeroException>(h1).Run(throwException);

			// Assert
			Assert.Equal(1, sideEffect);
		}
	}
}
