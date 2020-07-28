using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Jeebs;
using Xunit;

namespace Tests.Jeebs.Result.AuditAsync
{
	public class Task0_Simple : IAuditAsync_Task0_Simple
	{
		[Fact]
		public async Task StartSync_AuditAsync_Returns_Original_Object()
		{
			// Arrange
			var chain = Chain.Create();
			static async Task audit<TResult>(IR<TResult> _) { }

			// Act
			var result = await chain.AuditAsync(audit);

			// Assert
			Assert.StrictEqual(chain, result);
		}

		[Fact]
		public async Task StartSync_Successful_AuditAsync_Writes_To_Log()
		{
			// Arrange
			var chain = Chain.Create();

			static async Task<IR<int>> l0(IOk r) => r.OkV(18);
			static async Task<IR<int>> l1(IOkV<int> r) => r.Error();

			var log = new List<string>();
			async Task audit<TResult>(IR<TResult> r)
			{
				if (r is IError error)
				{
					log.Add("Error!");
				}
				else if (r is IOkV<TResult> ok)
				{
					log.Add($"Value: {ok.Value}");
				}
				else
				{
					log.Add("Unknown state.");
				}
			}

			var expected = new[] { "Unknown state.", "Value: 18", "Error!" }.ToList();

			// Act
			await chain
				.AuditAsync(audit).Await()
				.Link().MapAsync(l0).Await()
				.AuditAsync(audit).Await()
				.Link().MapAsync(l1).Await()
				.AuditAsync(audit);

			// Assert
			Assert.Equal(expected, log);
		}

		[Fact]
		public async Task StartSync_Unsuccessful_Audit_Captures_Exception()
		{
			// Arrange
			var chain = Chain.Create();
			static async Task audit<TResult>(IR<TResult> _) => throw new Exception();

			// Act
			var result = await chain.AuditAsync(audit);

			// Assert
			Assert.Equal(1, result.Messages.Count);
			Assert.True(result.Messages.Contains<Jm.AuditAsyncExceptionMsg>());
		}

		[Fact]
		public async Task StartAsync_AuditAsync_Returns_Original_Object()
		{
			// Arrange
			var chain = Chain.CreateAsync();
			static async Task audit<TResult>(IR<TResult> _) { }

			// Act
			var result = await chain.Await().AuditAsync(audit);

			// Assert
			Assert.StrictEqual(await chain, result);
		}

		[Fact]
		public async Task StartAsync_Successful_AuditAsync_Writes_To_Log()
		{
			// Arrange
			var chain = Chain.CreateAsync();

			static async Task<IR<int>> l0(IOk r) => r.OkV(18);
			static async Task<IR<TResult>> l1<TResult>(IOkV<TResult> r) => r.Error();

			var log = new List<string>();
			async Task audit<TResult>(IR<TResult> r)
			{
				if (r is IError<TResult> error)
				{
					log.Add("Error!");
				}
				else if (r is IOkV<TResult> ok)
				{
					log.Add($"Value: {ok.Value}");
				}
				else
				{
					log.Add("Unknown state.");
				}
			}

			var expected = new[] { "Unknown state.", "Value: 18", "Error!" }.ToList();

			// Act
			await chain.Await()
				.AuditAsync(audit).Await()
				.Link().MapAsync(l0).Await()
				.AuditAsync(audit).Await()
				.Link().MapAsync(l1).Await()
				.AuditAsync(audit);

			// Assert
			Assert.Equal(expected, log);
		}

		[Fact]
		public async Task StartAsync_Unsuccessful_Audit_Captures_Exception()
		{
			// Arrange
			var chain = Chain.CreateAsync();
			static async Task audit<TResult>(IR<TResult> _) => throw new Exception();

			// Act
			var result = await chain.Await().AuditAsync(audit);

			// Assert
			Assert.Equal(1, result.Messages.Count);
			Assert.True(result.Messages.Contains<Jm.AuditAsyncExceptionMsg>());
		}
	}
}
