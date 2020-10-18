using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Jeebs.RExtensions_Tests.WithState
{
	public class AuditAsync_Tests : IAudit_Audit
	{
		[Fact]
		public void Returns_Original_Object()
		{
			// Arrange
			var state = F.Rnd.Int;
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
			var state = F.Rnd.Int;
			var value = F.Rnd.Int;
			var chain = Chain.Create(state);

			async Task<IR<int, int>> l0(IOk<bool, int> r) => r.OkV(value);
			static async Task<IR<TValue, int>> l1<TValue>(IOkV<TValue, int> r) => r.Error();

			var log = new List<string>();
			var a0 = F.Rnd.Str;
			var a1 = F.Rnd.Str;
			var a2 = F.Rnd.Str;

			async Task a<TValue>(IR<TValue, int> r)
			{
				if (r is IError<TValue, int> error)
				{
					log.Add(a0);
				}
				else if (r is IOkV<TValue, int> ok)
				{
					log.Add($"{a1}: {ok.Value}");
				}
				else
				{
					log.Add(a2);
				}
			}

			var expected = new[] { a2, $"{a1}: {value}", a0 }.ToList();

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
			var state = F.Rnd.Int;
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
