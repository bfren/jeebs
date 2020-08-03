using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Jeebs.AuditTests.WithState
{
	public partial class AuditSwitch_Tests
	{
		[Fact]
		public void IOkV_Input_When_IOk_Does_Nothing()
		{
			// Arrange
			const int state = 7;
			var chain = Chain.Create(state);
			int sideEffect = 1;
			void a(IOkV<bool, int> _) => sideEffect++;

			// Act
			chain.AuditSwitch(isOkV: a);

			// Assert
			Assert.Equal(1, sideEffect);
		}

		[Fact]
		public void IOkV_Input_When_IOkV_Runs_Func()
		{
			// Arrange
			const int value = 18;
			const int state = 7;
			var chain = Chain.CreateV(value, state);
			int sideEffect = 1;
			void a(IOkV<int, int> _) => sideEffect++;

			// Act
			chain.AuditSwitch(isOkV: a);

			// Assert
			Assert.Equal(2, sideEffect);
		}

		[Fact]
		public void IOkV_Input_When_IError_Does_Nothing()
		{
			// Arrange
			const int state = 7;
			var chain = Chain.Create(state).Error();
			int sideEffect = 1;
			void a(IOkV<bool, int> _) => sideEffect++;

			// Act
			chain.AuditSwitch(isOkV: a);

			// Assert
			Assert.Equal(1, sideEffect);
		}
	}
}
