using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Jeebs.AuditTests.WithState.Async
{
	public class AuditAsync_Tests : IAudit_Audit
	{
		[Fact]
		public void Returns_Original_Object()
		{
			// Arrange
			const int state = 7;
			var chain = Chain.Create(state);
			static async Task a<TValue, TState>(IR<TValue, TState> _) { }

			// Act
			var next = chain.AuditAsync(a).Await();

			// Assert
			Assert.StrictEqual(chain, next);
		}

		[Fact]
		public void Runs_Audit_Action()
		{
			// Arrange
			const int state = 7;
			var chain = Chain.Create(state);

			static async Task<IR<int, int>> l0(IOk<bool, int> r) => r.OkV(18);
			static async Task<IR<TValue, int>> l1<TValue>(IOkV<TValue, int> r) => r.Error();

			var log = new List<string>();
			async Task a<TValue>(IR<TValue, int> r)
			{
				if (r is IError<TValue, int> error)
				{
					log.Add("Error!");
				}
				else if (r is IOkV<TValue, int> ok)
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
			chain
				.AuditAsync(a).Await()
				.Link().MapAsync(l0).Await()
				.AuditAsync(a).Await()
				.Link().MapAsync(l1).Await()
				.AuditAsync(a).Await();

			// Assert
			Assert.Equal(expected, log);
		}

		[Fact]
		public void Catches_Exception()
		{
			// Arrange
			const int state = 7;
			var chain = Chain.Create(state);
			static async Task a(IR _) => throw new Exception();

			// Act
			var next = chain.AuditAsync(a).Await();

			// Assert
			Assert.Equal(1, next.Messages.Count);
			Assert.True(next.Messages.Contains<Jm.AuditAsync.AuditAsyncExceptionMsg>());
		}
	}
}
