using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Jeebs.AuditTests.Async
{
	public partial class AuditSwitchAsync_Tests
	{
		[Fact]
		public void IOk_Input_When_IOk_Runs_Func()
		{
			// Arrange
			var chain = Chain.Create();
			int sideEffect = 1;
			async Task a(IOk<bool> _) => sideEffect++;

			// Act
			chain.AuditSwitchAsync(isOk: a).Await();

			// Assert
			Assert.Equal(2, sideEffect);
		}

		[Fact]
		public void IOk_Input_When_IOkV_Does_Nothing()
		{
			// Arrange
			const int value = 18;
			var chain = Chain.CreateV(value);
			int sideEffect = 1;
			async Task a(IOk<int> _) => sideEffect++;

			// Act
			chain.AuditSwitchAsync(isOk: a).Await();

			// Assert
			Assert.Equal(1, sideEffect);
		}

		[Fact]
		public void IOk_Input_When_IError_Does_Nothing()
		{
			// Arrange
			var chain = Chain.Create().Error();
			int sideEffect = 1;
			async Task a(IOk<bool> _) => sideEffect++;

			// Act
			chain.AuditSwitchAsync(isOk: a).Await();

			// Assert
			Assert.Equal(1, sideEffect);
		}
	}
}
