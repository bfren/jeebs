using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Jeebs.AuditTests.WithState.Async
{
	public partial class AuditSwitchAsync_Tests
	{
		[Fact]
		public void IOkV_Input_When_IOk_Does_Nothing()
		{
			// Arrange
			const int state = 7;
			var chain = Chain.Create(state);
			int sideEffect = 1;
			async Task a(IOkV<bool, int> _) => sideEffect++;

			// Act
			chain.AuditSwitchAsync(isOkV: a).Await();

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
			async Task a(IOkV<int, int> _) => sideEffect++;

			// Act
			chain.AuditSwitchAsync(isOkV: a).Await();

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
			async Task a(IOkV<bool, int> _) => sideEffect++;

			// Act
			chain.AuditSwitchAsync(isOkV: a).Await();

			// Assert
			Assert.Equal(1, sideEffect);
		}

		[Fact]
		public void IOkV_Input_Catches_Exception()
		{
			// Arrange
			const int value = 18;
			const int state = 7;
			var chain = Chain.CreateV(value, state);
			static async Task a(IOkV<int, int> _) => throw new Exception();

			// Act
			var next = chain.AuditSwitchAsync(isOkV: a).Await();

			// Assert
			Assert.Equal(1, next.Messages.Count);
			Assert.True(next.Messages.Contains<Jm.AuditAsync.AuditSwitchAsyncExceptionMsg>());
		}
	}
}
