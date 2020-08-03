using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Jeebs.AuditTests
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
			const int value = 18;
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
	}
}
