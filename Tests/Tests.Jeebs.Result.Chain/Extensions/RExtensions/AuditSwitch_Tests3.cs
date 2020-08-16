using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Jeebs.RExtensions_Tests
{
	public partial class AuditSwitch_Tests
	{
		[Fact]
		public void IError_Input_When_IOk_Does_Nothing()
		{
			// Arrange
			var chain = Chain.Create();
			int sideEffect = 1;
			void a(IError<bool> _) => sideEffect++;

			// Act
			chain.AuditSwitch(isError: a);

			// Assert
			Assert.Equal(1, sideEffect);
		}

		[Fact]
		public void IError_Input_When_IOkV_Does_Nothing()
		{
			// Arrange
			const int value = 18;
			var chain = Chain.CreateV(value);
			int sideEffect = 1;
			void a(IError<int> _) => sideEffect++;

			// Act
			chain.AuditSwitch(isError: a);

			// Assert
			Assert.Equal(1, sideEffect);
		}

		[Fact]
		public void IError_Input_When_IError_Runs_Func()
		{
			// Arrange
			var chain = Chain.Create().Error();
			int sideEffect = 1;
			void a(IError<bool> _) => sideEffect++;

			// Act
			chain.AuditSwitch(isError: a);

			// Assert
			Assert.Equal(2, sideEffect);
		}

		[Fact]
		public void IError_Input_Catches_Exception()
		{
			// Arrange
			var chain = Chain.Create().Error();
			static void a(IError<bool> _) => throw new Exception();

			// Act
			var next = chain.AuditSwitch(isError: a);

			// Assert
			Assert.Equal(1, next.Messages.Count);
			Assert.True(next.Messages.Contains<Jm.Audit.AuditSwitchExceptionMsg>());
		}
	}
}
