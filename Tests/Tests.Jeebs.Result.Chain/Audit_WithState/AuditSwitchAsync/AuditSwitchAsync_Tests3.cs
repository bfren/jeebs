﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Jeebs.AuditTests.WithState.Async
{
	public partial class AuditSwitchAsync_Tests
	{
		[Fact]
		public void IError_Input_When_IOk_Does_Nothing()
		{
			// Arrange
			const int state = 7;
			var chain = Chain.Create(state);
			int sideEffect = 1;
			async Task a(IError<bool, int> _) => sideEffect++;

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
			const int state = 7;
			var chain = Chain.CreateV(value, state);
			int sideEffect = 1;
			async Task a(IError<int, int> _) => sideEffect++;

			// Act
			chain.AuditSwitchAsync(isError: a).Await();

			// Assert
			Assert.Equal(1, sideEffect);
		}

		[Fact]
		public void IError_Input_When_IError_Runs_Func()
		{
			// Arrange
			const int state = 7;
			var chain = Chain.Create(state).Error();
			int sideEffect = 1;
			async Task a(IError<bool, int> _) => sideEffect++;

			// Act
			chain.AuditSwitchAsync(isError: a).Await();

			// Assert
			Assert.Equal(2, sideEffect);
		}
	}
}
