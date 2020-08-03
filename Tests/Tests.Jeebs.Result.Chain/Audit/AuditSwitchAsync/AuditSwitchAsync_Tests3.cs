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
		public void IError_Input_When_IOk_Does_Nothing()
		{
			// Arrange
			var chain = Chain.Create();
			int sideEffect = 1;
			async Task a(IError<bool> _) => sideEffect++;

			// Act
			chain.AuditSwitchAsync(isError: a).Await();

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
			async Task a(IError<int> _) => sideEffect++;

			// Act
			chain.AuditSwitchAsync(isError: a).Await();

			// Assert
			Assert.Equal(1, sideEffect);
		}

		[Fact]
		public void IError_Input_When_IError_Runs_Func()
		{
			// Arrange
			var chain = Chain.Create().Error();
			int sideEffect = 1;
			async Task a(IError<bool> _) => sideEffect++;

			// Act
			chain.AuditSwitchAsync(isError: a).Await();

			// Assert
			Assert.Equal(2, sideEffect);
		}
	}
}
