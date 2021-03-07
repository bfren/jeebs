// Jeebs Unit Tests
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Jeebs.RExtensions_Tests
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
			var value = F.Rnd.Int;

			IR<int> l0(IOk r) => r.OkV(value);
			static IR<TValue> l1<TValue>(IOkV<TValue> r) => r.Error();

			var log = new List<string>();
			var a0 = F.Rnd.Str;
			var a1 = F.Rnd.Str;
			var a2 = F.Rnd.Str;

			void a<TValue>(IR<TValue> r)
			{
				if (r is IError<TValue> error)
				{
					log.Add(a0);
				}
				else if (r is IOkV<TValue> ok)
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
