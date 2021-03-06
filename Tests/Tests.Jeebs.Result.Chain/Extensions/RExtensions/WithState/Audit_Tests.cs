using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Jeebs.RExtensions_Tests.WithState
{
	public class Audit_Tests //: IAudit_Audit_WithState
	{
		[Fact]
		public void Returns_Original_Object()
		{
			// Arrange
			var state = F.Rnd.Int;
			var chain = Chain.Create(state);
			static void a(IR _) { }

			// Act
			var next = chain.Audit(a);

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

			IR<int, int> l0(IOk<bool, int> r) => r.OkV(value);
			static IR<TValue, int> l1<TValue>(IOkV<TValue, int> r) => r.Error();

			var log = new List<string>();
			var a0 = F.Rnd.Str;
			var a1 = F.Rnd.Str;
			var a2 = F.Rnd.Str;

			void a<TValue>(IR<TValue, int> r)
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
			var next = chain
				.Audit(a)
				.Link().Map(l0)
				.Audit(a)
				.Link().Map(l1)
				.Audit(a);

			// Assert
			Assert.Equal(expected, log);
			Assert.Equal(state, next.State);
		}

		[Fact]
		public void Catches_Exception()
		{
			// Arrange
			var state = F.Rnd.Int;
			var chain = Chain.Create(state);
			static void a(IR _) => throw new Exception();

			// Act
			var next = chain.Audit(a);

			// Assert
			Assert.Equal(1, next.Messages.Count);
			Assert.True(next.Messages.Contains<Jm.Audit.AuditExceptionMsg>());
			Assert.Equal(state, next.State);
		}
	}
}
