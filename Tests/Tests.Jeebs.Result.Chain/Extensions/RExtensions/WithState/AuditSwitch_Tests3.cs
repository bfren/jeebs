using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Jeebs.RExtensions_Tests.WithState
{
	public partial class AuditSwitch_Tests
	{
		[Fact]
		public void IError_Input_When_IOk_Does_Nothing()
		{
			// Arrange
			var state = F.Rnd.Integer;
			var chain = Chain.Create(state);
			int sideEffect = 1;
			void a(IError<bool, int> _) => sideEffect++;

			// Act
			chain.AuditSwitch(isError: a);

			// Assert
			Assert.Equal(1, sideEffect);
		}

		[Fact]
		public void IError_Input_When_IOkV_Does_Nothing()
		{
			// Arrange
			var value = F.Rnd.Integer;
			var state = F.Rnd.Integer;
			var chain = Chain.CreateV(value, state);
			int sideEffect = 1;
			void a(IError<int, int> _) => sideEffect++;

			// Act
			chain.AuditSwitch(isError: a);

			// Assert
			Assert.Equal(1, sideEffect);
		}

		[Fact]
		public void IError_Input_When_IError_Runs_Func()
		{
			// Arrange
			var state = F.Rnd.Integer;
			var chain = Chain.Create(state).Error();
			int sideEffect = 1;
			void a(IError<bool, int> _) => sideEffect++;

			// Act
			chain.AuditSwitch(isError: a);

			// Assert
			Assert.Equal(2, sideEffect);
		}

		[Fact]
		public void IError_Input_Catches_Exception()
		{
			// Arrange
			var state = F.Rnd.Integer;
			var chain = Chain.Create(state).Error();
			static void a(IError<bool, int> _) => throw new Exception();

			// Act
			var next = chain.AuditSwitch(isError: a);

			// Assert
			Assert.Equal(1, next.Messages.Count);
			Assert.True(next.Messages.Contains<Jm.Audit.AuditSwitchExceptionMsg>());
		}
	}
}
