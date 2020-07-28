using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Jeebs;
using Xunit;

namespace Tests.Jeebs.Result_old.AuditAsync
{
	public class Task1_Switch : IAuditAsync_Task1_Switch
	{
		#region Start Sync

		[Fact]
		public async Task StartSync_AuditSwitchAsync_Returns_Original_Object_Without_Parameters()
		{
			// Arrange
			var chain = R.Chain;

			// Act
			var result = await chain.AuditSwitchAsync();

			// Assert
			Assert.StrictEqual(chain, result);
		}

		[Fact]
		public async Task StartSync_AuditSwitchAsync_Returns_Original_Object_With_Parameters()
		{
			// Arrange
			var chain = R.Chain;
			static async Task ok<TResult>(IOk<TResult> _) { }
			static async Task okV<TResult>(IOkV<TResult> _) { }
			static async Task error<TResult>(IError<TResult> _) { }
			static async Task unknown() { }

			// Act
			var result = await chain.AuditSwitchAsync(ok, okV, error, unknown);

			// Assert
			Assert.StrictEqual(chain, result);
		}

		[Fact]
		public async Task StartSync_AuditSwitchAsync_Catches_Exception_Adds_Message()
		{
			// Arrange
			var chain = R.Chain;
			static async Task fail<TResult>(IOk<TResult> _) => throw new Exception();

			// Act
			var result = await chain.AuditSwitchAsync(isOk: fail);

			// Assert
			Assert.True(result.Messages.Contains<Jm.AuditAsyncException>());
		}

		[Fact]
		public async Task StartSync_AuditSwitchAsync_Runs_Ok_When_Ok()
		{
			// Arrange
			var chain = R.Chain;
			var run = false;
			async Task ok<TResult>(IOk<TResult> _) { run = true; }

			// Act
			await chain.AuditSwitchAsync(isOk: ok);

			// Assert
			Assert.True(run);
		}

		[Fact]
		public async Task StartSync_AuditSwitchAsync_DoesNot_Run_OkV_When_Ok()
		{
			// Arrange
			var chain = R.Chain;
			var run = false;
			async Task okV<TResult>(IOkV<TResult> _) { run = true; }

			// Act
			await chain.AuditSwitchAsync(isOkV: okV);

			// Assert
			Assert.False(run);
		}

		[Fact]
		public async Task StartSync_AuditSwitchAsync_DoesNot_Run_Error_When_Ok()
		{
			// Arrange
			var chain = R.Chain;
			var run = false;
			async Task error<TResult>(IError<TResult> _) { run = true; }

			// Act
			await chain.AuditSwitchAsync(isError: error);

			// Assert
			Assert.False(run);
		}

		[Fact]
		public async Task StartSync_AuditSwitchAsync_Runs_OkV_When_OkV()
		{
			// Arrange
			var chain = R<int>.ChainV(10);
			var run = false;
			async Task okV<TResult>(IOkV<TResult> _) { run = true; }

			// Act
			await chain.AuditSwitchAsync(isOkV: okV);

			// Assert
			Assert.True(run);
		}

		[Fact]
		public async Task StartSync_AuditSwitchAsync_DoesNot_Run_Ok_When_OkV()
		{
			// Arrange
			var chain = R<int>.ChainV(10);
			var run = false;
			async Task ok<TResult>(IOk<TResult> _) { run = true; }

			// Act
			await chain.AuditSwitchAsync(isOk: ok);

			// Assert
			Assert.False(run);
		}

		[Fact]
		public async Task StartSync_AuditSwitchAsync_DoesNot_Run_Error_When_OkV()
		{
			// Arrange
			var chain = R<int>.ChainV(10);
			var run = false;
			async Task error<TResult>(IError<TResult> _) { run = true; }

			// Act
			await chain.AuditSwitchAsync(isError: error);

			// Assert
			Assert.False(run);
		}

		[Fact]
		public async Task StartSync_AuditSwitchAsync_Runs_Error_When_Error()
		{
			// Arrange
			var chain = R.Chain;
			var run = false;
			static async Task<IR<TResult>> fail<TResult>(IOk<TResult> r) => r.Error();
			async Task error<TResult>(IError<TResult> _) { run = true; }

			// Act
			await chain.LinkMapAsync(fail).AuditSwitchAsync(isError: error);

			// Assert
			Assert.True(run);
		}

		[Fact]
		public async Task StartSync_AuditSwitchAsync_DoesNot_Run_Ok_When_Error()
		{
			// Arrange
			var chain = R.Chain;
			var run = false;
			static async Task<IR<TResult>> fail<TResult>(IOk<TResult> r) => r.Error();
			async Task ok<TResult>(IOk<TResult> _) { run = true; }

			// Act
			await chain.LinkMapAsync(fail).AuditSwitchAsync(isOk: ok);

			// Assert
			Assert.False(run);
		}

		[Fact]
		public async Task StartSync_AuditSwitchAsync_DoesNot_Run_OkV_When_Error()
		{
			// Arrange
			var chain = R.Chain;
			var run = false;
			static async Task<IR<TResult>> fail<TResult>(IOk<TResult> r) => r.Error();
			async Task okV<TResult>(IOkV<TResult> _) { run = true; }

			// Act
			await chain.LinkMapAsync(fail).AuditSwitchAsync(isOkV: okV);

			// Assert
			Assert.False(run);
		}

		[Fact]
		public async Task StartSync_AuditSwitchAsync_Runs_Unknown_When_Unknown()
		{
			// Arrange
			var chain = new OtherR();
			var run = false;
			async Task unknown() { run = true; }

			// Act
			await chain.AuditSwitchAsync(isUnknown: unknown);

			// Assert
			Assert.True(run);
		}

		#endregion

		#region Start Async

		[Fact]
		public async Task StartAsync_AuditSwitchAsync_Returns_Original_Object_Without_Parameters()
		{
			// Arrange
			var chain = R.ChainAsync;

			// Act
			var result = await chain.AuditSwitchAsync();

			// Assert
			Assert.StrictEqual(await chain, result);
		}

		[Fact]
		public async Task StartAsync_AuditSwitchAsync_Returns_Original_Object_With_Parameters()
		{
			// Arrange
			var chain = R.ChainAsync;
			static async Task ok<TResult>(IOk<TResult> _) { }
			static async Task okV<TResult>(IOkV<TResult> _) { }
			static async Task error<TResult>(IError<TResult> _) { }
			static async Task unknown() { }

			// Act
			var result = await chain.AuditSwitchAsync(ok, okV, error, unknown);

			// Assert
			Assert.StrictEqual(await chain, result);
		}

		[Fact]
		public async Task StartAsync_AuditSwitchAsync_Catches_Exception_Adds_Message()
		{
			// Arrange
			var chain = R.ChainAsync;
			static async Task fail<TResult>(IOk<TResult> _) => throw new Exception();

			// Act
			var result = await chain.AuditSwitchAsync(isOk: fail);

			// Assert
			Assert.True(result.Messages.Contains<Jm.AuditAsyncException>());
		}

		[Fact]
		public async Task StartAsync_AuditSwitchAsync_Runs_Ok_When_Ok()
		{
			// Arrange
			var chain = R.ChainAsync;
			var run = false;
			async Task ok<TResult>(IOk<TResult> _) { run = true; }

			// Act
			await chain.AuditSwitchAsync(isOk: ok);

			// Assert
			Assert.True(run);
		}

		[Fact]
		public async Task StartAsync_AuditSwitchAsync_DoesNot_Run_OkV_When_Ok()
		{
			// Arrange
			var chain = R.ChainAsync;
			var run = false;
			async Task okV<TResult>(IOkV<TResult> _) { run = true; }

			// Act
			await chain.AuditSwitchAsync(isOkV: okV);

			// Assert
			Assert.False(run);
		}

		[Fact]
		public async Task StartAsync_AuditSwitchAsync_DoesNot_Run_Error_When_Ok()
		{
			// Arrange
			var chain = R.ChainAsync;
			var run = false;
			async Task error<TResult>(IError<TResult> _) { run = true; }

			// Act
			await chain.AuditSwitchAsync(isError: error);

			// Assert
			Assert.False(run);
		}

		[Fact]
		public async Task StartAsync_AuditSwitchAsync_Runs_OkV_When_OkV()
		{
			// Arrange
			var chain = R<int>.ChainVAsync(10);
			var run = false;
			async Task okV<TResult>(IOkV<TResult> _) { run = true; }

			// Act
			await chain.AuditSwitchAsync(isOkV: okV);

			// Assert
			Assert.True(run);
		}

		[Fact]
		public async Task StartAsync_AuditSwitchAsync_DoesNot_Run_Ok_When_OkV()
		{
			// Arrange
			var chain = R<int>.ChainVAsync(10);
			var run = false;
			async Task ok<TResult>(IOk<TResult> _) { run = true; }

			// Act
			await chain.AuditSwitchAsync(isOk: ok);

			// Assert
			Assert.False(run);
		}

		[Fact]
		public async Task StartAsync_AuditSwitchAsync_DoesNot_Run_Error_When_OkV()
		{
			// Arrange
			var chain = R<int>.ChainVAsync(10);
			var run = false;
			async Task error<TResult>(IError<TResult> _) { run = true; }

			// Act
			await chain.AuditSwitchAsync(isError: error);

			// Assert
			Assert.False(run);
		}

		[Fact]
		public async Task StartAsync_AuditSwitchAsync_Runs_Error_When_Error()
		{
			// Arrange
			var chain = R.ChainAsync;
			var run = false;
			static async Task<IR<TResult>> fail<TResult>(IOk<TResult> r) => r.Error();
			async Task error<TResult>(IError<TResult> _) { run = true; }

			// Act
			await chain.LinkMapAsync(fail).AuditSwitchAsync(isError: error);

			// Assert
			Assert.True(run);
		}

		[Fact]
		public async Task StartAsync_AuditSwitchAsync_DoesNot_Run_Ok_When_Error()
		{
			// Arrange
			var chain = R.ChainAsync;
			var run = false;
			static async Task<IR<TResult>> fail<TResult>(IOk<TResult> r) => r.Error();
			async Task ok<TResult>(IOk<TResult> _) { run = true; }

			// Act
			await chain.LinkMapAsync(fail).AuditSwitchAsync(isOk: ok);

			// Assert
			Assert.False(run);
		}

		[Fact]
		public async Task StartAsync_AuditSwitchAsync_DoesNot_Run_OkV_When_Error()
		{
			// Arrange
			var chain = R.ChainAsync;
			var run = false;
			static async Task<IR<TResult>> fail<TResult>(IOk<TResult> r) => r.Error();
			async Task okV<TResult>(IOkV<TResult> _) { run = true; }

			// Act
			await chain.LinkMapAsync(fail).AuditSwitchAsync(isOkV: okV);

			// Assert
			Assert.False(run);
		}

		[Fact]
		public async Task StartAsync_AuditSwitchAsync_Runs_Unknown_When_Unknown()
		{
			// Arrange
			var chain = Task.Run(() => (IR<object>)new OtherR());
			var run = false;
			async Task unknown() { run = true; }

			// Act
			await chain.AuditSwitchAsync(isUnknown: unknown);

			// Assert
			Assert.True(run);
		}

		#endregion

		private class OtherR : R<object>
		{
			public override bool Val => false;
		}
	}
}
