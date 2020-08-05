using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace Jeebs.AuditTests
{
	public class Audit_Tests : IAudit_Audit
	{
		[Fact]
		public void Returns_Original_Object()
		{
			// Arrange
			var chain = Chain.Create();
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
			var chain = Chain.Create();

			static IR<int> l0(IOk r) => r.OkV(18);
			static IR<TValue> l1<TValue>(IOkV<TValue> r) => r.Error();

			var log = new List<string>();
			void a<TValue>(IR<TValue> r)
			{
				if (r is IError<TValue> error)
				{
					log.Add("Error!");
				}
				else if (r is IOkV<TValue> ok)
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
				.Audit(a)
				.Link().Map(l0)
				.Audit(a)
				.Link().Map(l1)
				.Audit(a);

			// Assert
			Assert.Equal(expected, log);
		}

		[Fact]
		public void Catches_Exception()
		{
			// Arrange
			var chain = Chain.Create();
			static void a(IR _) => throw new Exception();

			// Act
			var next = chain.Audit(a);

			// Assert
			Assert.Equal(1, next.Messages.Count);
			Assert.True(next.Messages.Contains<Jm.Audit.AuditExceptionMsg>());
		}
	}
}
