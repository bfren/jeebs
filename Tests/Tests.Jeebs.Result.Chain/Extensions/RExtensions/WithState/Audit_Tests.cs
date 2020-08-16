using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace Jeebs.RExtensions_Tests.WithState
{
	public class Audit_Tests //: IAudit_Audit_WithState
	{
		[Fact]
		public void Returns_Original_Object()
		{
			// Arrange
			const int state = 7;
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
			const int state = 7;
			var chain = Chain.Create(state);

			static IR<int, int> l0(IOk<bool, int> r) => r.OkV(18);
			static IR<TValue, int> l1<TValue>(IOkV<TValue, int> r) => r.Error();

			var log = new List<string>();
			void a<TValue>(IR<TValue, int> r)
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
			const int state = 7;
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
