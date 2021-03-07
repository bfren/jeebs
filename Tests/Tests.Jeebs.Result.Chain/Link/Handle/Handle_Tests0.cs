// Jeebs Unit Tests
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System;
using Xunit;

namespace Jeebs.Link_Tests
{
	public partial class Handle_Tests : ILink_Handle
	{
		[Fact]
		public void Generic_Handler_Returns_Original_Link()
		{
			// Arrange
			var link = Chain.Create().Link();
			static void handler(IR<bool> _, Exception __) { }

			// Act
			var next = link.Handle().With(handler);

			// Assert
			Assert.Same(link, next);
		}

		[Fact]
		public void Generic_Handler_Runs_For_Any_Exception()
		{
			// Arrange
			var chain = Chain.Create();
			var sideEffect = 1;
			void handler(IR<bool> _, Exception __) => sideEffect++;
			static void throwGeneric() => throw new Exception();
			static void throwOther() => throw new DivideByZeroException();

			// Act
			chain.Link().Handle().With(handler).Run(throwGeneric);
			chain.Link().Handle().With(handler).Run(throwOther);

			// Assert
			Assert.Equal(3, sideEffect);
		}
	}
}
