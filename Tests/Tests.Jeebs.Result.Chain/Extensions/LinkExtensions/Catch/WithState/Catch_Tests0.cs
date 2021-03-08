// Jeebs Unit Tests
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System;
using Xunit;

namespace Jeebs.LinkExtensions_Tests.WithState
{
	public partial class Catch_Tests : ILinkExtensions_Catch_WithState
	{
		[Fact]
		public void Generic_Handler_Returns_Original_Link()
		{
			// Arrange
			var state = F.Rnd.Int;
			var link = Chain.Create(state).Link();
			static void h0(IR<bool> _, Exception __) { }
			static void h1(IR<bool, int> _, Exception __) { }

			// Act
			var n0 = link.Catch().AllUnhandled().With(h0);
			var n1 = link.Catch().AllUnhandled().With(h1);

			// Assert
			Assert.Same(link, n0);
			Assert.Same(link, n1);
		}

		[Fact]
		public void Generic_Handler_Runs_For_Any_Exception()
		{
			// Arrange
			var state = F.Rnd.Int;
			var chain = Chain.Create(state);
			var sideEffect = 1;
			void h0(IR<bool> _, Exception __) => sideEffect++;
			void h1(IR<bool, int> _, Exception __) => sideEffect++;
			static void throwGeneric() => throw new Exception();
			static void throwOther() => throw new DivideByZeroException();

			// Act
			chain.Link().Catch().AllUnhandled().With(h0).Run(throwGeneric);
			chain.Link().Catch().AllUnhandled().With(h0).Run(throwOther);
			chain.Link().Catch().AllUnhandled().With(h1).Run(throwGeneric);
			chain.Link().Catch().AllUnhandled().With(h1).Run(throwOther);

			// Assert
			Assert.Equal(5, sideEffect);
		}
	}
}
