// Jeebs Unit Tests
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System;
using System.Threading.Tasks;
using Xunit;

namespace Jeebs.LinkExtensions_Tests
{
	public partial class Catch_Tests
	{
		[Fact]
		public void Generic_AsyncHandler_Returns_Original_Link()
		{
			// Arrange
			var link = Chain.Create().Link();
			static async Task handler(IR<bool> _, Exception __) { }

			// Act
			var next = link.Catch().AllUnhandled().With(handler);

			// Assert
			Assert.Same(link, next);
		}

		[Fact]
		public void Generic_AsyncHandler_Runs_For_Any_Exception()
		{
			// Arrange
			var chain = Chain.Create();
			var sideEffect = 1;
			async Task handler(IR<bool> _, Exception __) => sideEffect++;
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
