// Jeebs Unit Tests
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System;
using System.Threading.Tasks;
using Xunit;

namespace Jeebs.Link_Tests
{
	public partial class Handle_Tests
	{
		[Fact]
		public void Specific_AsyncHandler_Runs_For_That_Exception()
		{
			// Arrange
			var chain = Chain.Create();
			var sideEffect = 1;
			async Task handler(IR<bool> _, DivideByZeroException __) => sideEffect++;
			static void throwException() => throw new DivideByZeroException();

			// Act
			chain.Link().Handle<DivideByZeroException>().With(handler).Run(throwException);

			// Assert
			Assert.Equal(2, sideEffect);
		}

		[Fact]
		public void Specific_AsyncHandler_Does_Not_Run_For_Other_Exceptions()
		{
			// Arrange
			var chain = Chain.Create();
			var sideEffect = 1;
			async Task handler(IR<bool> _, DivideByZeroException __) => sideEffect++;
			static void throwException() => throw new ArithmeticException();

			// Act
			chain.Link().Handle<DivideByZeroException>().With(handler).Run(throwException);

			// Assert
			Assert.Equal(1, sideEffect);
		}
	}
}
