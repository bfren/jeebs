// Jeebs Unit Tests
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System;
using System.Threading.Tasks;
using Xunit;

namespace Jeebs.RExtensions_Tests
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
			var value = F.Rnd.Int;
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

		[Fact]
		public void IError_Input_Catches_Exception()
		{
			// Arrange
			var chain = Chain.Create().Error();
			static async Task a(IError<bool> _) => throw new Exception();

			// Act
			var next = chain.AuditSwitchAsync(isError: a).Await();

			// Assert
			Assert.Equal(1, next.Messages.Count);
			Assert.True(next.Messages.Contains<Jm.AuditAsync.AuditSwitchAsyncExceptionMsg>());
		}
	}
}
