// Jeebs Unit Tests
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System;
using Xunit;

namespace Jeebs.RExtensions_Tests
{
	public partial class AuditSwitch_Tests
	{
		[Fact]
		public void IOk_Input_When_IOk_Runs_Func()
		{
			// Arrange
			var chain = Chain.Create();
			int sideEffect = 1;
			void a(IOk<bool> _) => sideEffect++;

			// Act
			chain.AuditSwitch(isOk: a);

			// Assert
			Assert.Equal(2, sideEffect);
		}

		[Fact]
		public void IOk_Input_When_IOkV_Does_Nothing()
		{
			// Arrange
			var value = F.Rnd.Int;
			var chain = Chain.CreateV(value);
			int sideEffect = 1;
			void a(IOk<int> _) => sideEffect++;

			// Act
			chain.AuditSwitch(isOk: a);

			// Assert
			Assert.Equal(1, sideEffect);
		}

		[Fact]
		public void IOk_Input_When_IError_Does_Nothing()
		{
			// Arrange
			var chain = Chain.Create().Error();
			int sideEffect = 1;
			void a(IOk<bool> _) => sideEffect++;

			// Act
			chain.AuditSwitch(isOk: a);

			// Assert
			Assert.Equal(1, sideEffect);
		}

		[Fact]
		public void IOk_Input_Catches_Exception()
		{
			// Arrange
			var chain = Chain.Create();
			static void a(IOk<bool> _) => throw new Exception();

			// Act
			var next = chain.AuditSwitch(isOk: a);

			// Assert
			Assert.Equal(1, next.Messages.Count);
			Assert.True(next.Messages.Contains<Jm.Audit.AuditSwitchExceptionMsg>());
		}
	}
}
