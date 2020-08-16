using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Jeebs.RExtensions_Tests.WithState
{
	public partial class AuditSwitch_Tests
	{
		[Fact]
		public void IOk_Input_When_IOk_Runs_Func()
		{
			// Arrange
			const int state = 7;
			var chain = Chain.Create(state);
			int sideEffect = 1;
			void a(IOk<bool, int> _) => sideEffect++;

			// Act
			chain.AuditSwitch(isOk: a);

			// Assert
			Assert.Equal(2, sideEffect);
		}

		[Fact]
		public void IOk_Input_When_IOkV_Does_Nothing()
		{
			// Arrange
			const int value = 18;
			const int state = 7;
			var chain = Chain.CreateV(value, state);
			int sideEffect = 1;
			void a(IOk<int, int> _) => sideEffect++;

			// Act
			chain.AuditSwitch(isOk: a);

			// Assert
			Assert.Equal(1, sideEffect);
		}

		[Fact]
		public void IOk_Input_When_IError_Does_Nothing()
		{
			// Arrange
			const int state = 7;
			var chain = Chain.Create(state).Error();
			int sideEffect = 1;
			void a(IOk<bool, int> _) => sideEffect++;

			// Act
			chain.AuditSwitch(isOk: a);

			// Assert
			Assert.Equal(1, sideEffect);
		}

		[Fact]
		public void IOk_Input_Catches_Exception()
		{
			// Arrange
			const int state = 7;
			var chain = Chain.Create(state);
			static void a<TValue, TState>(IOk<TValue, TState> _) => throw new Exception();

			// Act
			var next = chain.AuditSwitch(a);

			// Assert
			Assert.Equal(1, next.Messages.Count);
			Assert.True(next.Messages.Contains<Jm.Audit.AuditSwitchExceptionMsg>());
			Assert.Equal(state, next.State);
		}
	}
}
