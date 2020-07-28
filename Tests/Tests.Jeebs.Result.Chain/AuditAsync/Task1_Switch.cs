using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Jeebs;
using Xunit;

namespace Tests.Jeebs.Result.AuditAsync
{
	public class Task1_Switch : IAuditAsync_Task1_Switch
	{
		[Fact]
		public async Task AuditSwitchAsync_Returns_Original_Object_Without_Parameters()
		{
			// Arrange
			var chain = Chain.Create();

			// Act
			var result = await chain.AuditSwitchAsync();

			// Assert
			Assert.StrictEqual(chain, result);
		}

		[Fact]
		public async Task AuditSwitchAsync_Returns_Original_Object_With_Parameters()
		{
			// Arrange
			var chain = Chain.Create();
			static async Task ok<TResult>(IOk<TResult> _) { }
			static async Task okV<TResult>(IOkV<TResult> _) { }
			static async Task error<TResult>(IError<TResult> _) { }

			// Act
			var result = await chain.AuditSwitchAsync(ok, okV, error);

			// Assert
			Assert.StrictEqual(chain, result);
		}

		[Fact]
		public async Task AuditSwitchAsync_Catches_Exception_Adds_Message()
		{
			// Arrange
			var chain = Chain.Create();
			static async Task fail<TResult>(IOk<TResult> _) => throw new Exception();

			// Act
			var result = await chain.AuditSwitchAsync(isOk: fail);

			// Assert
			Assert.True(result.Messages.Contains<Jm.AuditAsyncExceptionMsg>());
		}

		[Fact]
		public async Task AuditSwitchAsync_Runs_Ok_When_Ok()
		{
			// Arrange
			var chain = Chain.Create();
			var run = false;
			async Task ok<TResult>(IOk<TResult> _) { run = true; }

			// Act
			await chain.AuditSwitchAsync(isOk: ok);

			// Assert
			Assert.True(run);
		}

		[Fact]
		public async Task AuditSwitchAsync_DoesNot_Run_OkV_When_Ok()
		{
			// Arrange
			var chain = Chain.Create();
			var run = false;
			async Task okV<TResult>(IOkV<TResult> _) { run = true; }

			// Act
			await chain.AuditSwitchAsync(isOkV: okV);

			// Assert
			Assert.False(run);
		}

		[Fact]
		public async Task AuditSwitchAsync_DoesNot_Run_Error_When_Ok()
		{
			// Arrange
			var chain = Chain.Create();
			var run = false;
			async Task error<TResult>(IError<TResult> _) { run = true; }

			// Act
			await chain.AuditSwitchAsync(isError: error);

			// Assert
			Assert.False(run);
		}

		[Fact]
		public async Task AuditSwitchAsync_Runs_OkV_When_OkV()
		{
			// Arrange
			var chain = Chain.CreateV(18);
			var run = false;
			async Task okV<TResult>(IOkV<TResult> _) { run = true; }

			// Act
			await chain.AuditSwitchAsync(isOkV: okV);

			// Assert
			Assert.True(run);
		}

		[Fact]
		public async Task AuditSwitchAsync_DoesNot_Run_Ok_When_OkV()
		{
			// Arrange
			var chain = Chain.CreateV(18);
			var run = false;
			async Task ok<TResult>(IOk<TResult> _) { run = true; }

			// Act
			await chain.AuditSwitchAsync(isOk: ok);

			// Assert
			Assert.False(run);
		}

		[Fact]
		public async Task AuditSwitchAsync_DoesNot_Run_Error_When_OkV()
		{
			// Arrange
			var chain = Chain.CreateV(18);
			var run = false;
			async Task error<TResult>(IError<TResult> _) { run = true; }

			// Act
			await chain.AuditSwitchAsync(isError: error);

			// Assert
			Assert.False(run);
		}

		[Fact]
		public async Task AuditSwitchAsync_Runs_Error_When_Error()
		{
			// Arrange
			var chain = Chain.CreateV(18);
			var run = false;
			static async Task<IR<TResult>> fail<TResult>(IOk<TResult> r) => r.Error();
			async Task error<TResult>(IError<TResult> _) { run = true; }

			// Act
			await chain
				.Link().MapAsync(fail).Await()
				.AuditSwitchAsync(isError: error);

			// Assert
			Assert.True(run);
		}

		[Fact]
		public async Task AuditSwitchAsync_DoesNot_Run_Ok_When_Error()
		{
			// Arrange
			var chain = Chain.Create();
			var run = false;
			static async Task<IR<TResult>> fail<TResult>(IOk<TResult> r) => r.Error();
			async Task ok<TResult>(IOk<TResult> _) { run = true; }

			// Act
			await chain
				.Link().MapAsync(fail).Await()
				.AuditSwitchAsync(isOk: ok);

			// Assert
			Assert.False(run);
		}

		[Fact]
		public async Task AuditSwitchAsync_DoesNot_Run_OkV_When_Error()
		{
			// Arrange
			var chain = Chain.Create();
			var run = false;
			static async Task<IR<TResult>> fail<TResult>(IOk<TResult> r) => r.Error();
			async Task okV<TResult>(IOkV<TResult> _) { run = true; }

			// Act
			await chain
				.Link().MapAsync(fail).Await()
				.AuditSwitchAsync(isOkV: okV);

			// Assert
			Assert.False(run);
		}
	}
}
