using System;
using System.Threading.Tasks;
using Xunit;

namespace Jeebs.RExtensions_Tests
{
	public partial class AuditSwitchAsync_Tests
	{
		[Fact]
		public void IOkV_Input_When_IOk_Does_Nothing()
		{
			// Arrange
			var chain = Chain.Create();
			int sideEffect = 1;
			async Task a(IOkV<bool> _) => sideEffect++;

			// Act
			chain.AuditSwitchAsync(isOkV: a).Await();

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
			async Task a(IOkV<int> _) => sideEffect++;

			// Act
			chain.AuditSwitchAsync(isOkV: a).Await();

			// Assert
			Assert.Equal(2, sideEffect);
		}

		[Fact]
		public void IOkV_Input_When_IError_Does_Nothing()
		{
			// Arrange
			var chain = Chain.Create().Error();
			int sideEffect = 1;
			async Task a(IOkV<bool> _) => sideEffect++;

			// Act
			chain.AuditSwitchAsync(isOkV: a).Await();

			// Assert
			Assert.Equal(1, sideEffect);
		}

		[Fact]
		public void IOkV_Input_Catches_Exception()
		{
			// Arrange
			var value = F.Rnd.Int;
			var chain = Chain.CreateV(value);
			static async Task a(IOkV<int> _) => throw new Exception();

			// Act
			var next = chain.AuditSwitchAsync(isOkV: a).Await();

			// Assert
			Assert.Equal(1, next.Messages.Count);
			Assert.True(next.Messages.Contains<Jm.AuditAsync.AuditSwitchAsyncExceptionMsg>());
		}
	}
}
