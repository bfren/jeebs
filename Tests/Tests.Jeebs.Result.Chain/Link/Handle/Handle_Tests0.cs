using System;
using System.Collections.Generic;
using System.Text;
using NSubstitute;
using Xunit;

namespace Jeebs.HandleTests
{
	public partial class Handle_Tests
	{
		[Fact]
		public void Generic_Handler_Returns_Original_Link()
		{
			// Arrange
			var link = Chain.Create().Link();
			static void handler(IR<bool> _, Exception __) { }

			// Act
			var next = link.Handle(handler);

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
			chain.Link().Handle(handler).Run(throwGeneric);
			chain.Link().Handle(handler).Run(throwOther);

			// Assert
			Assert.Equal(3, sideEffect);
		}
	}
}
