// Jeebs Unit Tests
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System;
using Xunit;

namespace Jeebs.RExtensions_Tests
{
	public partial class AuditSwitch_Tests
	{
		[Fact]
		public void IOkV_Input_When_IOk_Does_Nothing()
		{
			// Arrange
			var chain = Chain.Create();
			int sideEffect = 1;
			void a(IOkV<bool> _) => sideEffect++;

			// Act
			chain.AuditSwitch(isOkV: a);

			// Assert
			Assert.Equal(1, sideEffect);
		}

		[Fact]
		public void IOkV_Input_When_IOkV_Runs_Func()
		{
			// Arrange
			var value = F.Rnd.Int;
			var chain = Chain.CreateV(value);
			int sideEffect = 1;
			void a(IOkV<int> _) => sideEffect++;

			// Act
			chain.AuditSwitch(isOkV: a);

			// Assert
			Assert.Equal(2, sideEffect);
		}

		[Fact]
		public void IOkV_Input_When_IError_Does_Nothing()
		{
			// Arrange
			var chain = Chain.Create().Error();
			int sideEffect = 1;
			void a(IOkV<bool> _) => sideEffect++;

			// Act
			chain.AuditSwitch(isOkV: a);

			// Assert
			Assert.Equal(1, sideEffect);
		}

		[Fact]
		public void IOkV_Input_Catches_Exception()
		{
			// Arrange
			var value = F.Rnd.Int;
			var chain = Chain.CreateV(value);
			static void a(IOkV<int> _) => throw new Exception();

			// Act
			var next = chain.AuditSwitch(isOkV: a);

			// Assert
			Assert.Equal(1, next.Messages.Count);
			Assert.True(next.Messages.Contains<Jm.Audit.AuditSwitchExceptionMsg>());
		}
	}
}
