// Jeebs Unit Tests
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System;
using Xunit;

namespace Jeebs.LinkExtensions_Tests
{
	public partial class Catch_Tests : ILinkExtensions_Catch
	{
		[Fact]
		public void Generic_Handler_Returns_Original_Link()
		{
			// Arrange
			var link = Chain.Create().Link();
			static void handler(IR<bool> _, Exception __) { }

			// Act
			var next = link.Catch().AllUnhandled().With(handler);

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
			chain.Link().Catch().AllUnhandled().With(handler).Run(throwGeneric);
			chain.Link().Catch().AllUnhandled().With(handler).Run(throwOther);

			// Assert
			Assert.Equal(3, sideEffect);
		}
	}
}
