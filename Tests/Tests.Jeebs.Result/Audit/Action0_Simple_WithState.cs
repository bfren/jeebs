using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Jeebs;
using Xunit;

namespace Tests.Jeebs.Result.Audit
{
	public class Action0_Simple_WithState : IAudit_Action0_Simple
	{
		[Fact]
		public void Audit_Returns_Original_Object()
		{
			// Arrange
			var chain = R.Chain.WithState(false);
			static void audit<TResult, TState>(IR<TResult, TState> _) { }

			// Act
			var result = chain.Audit(audit);

			// Assert
			Assert.StrictEqual(chain, result);
		}

		[Fact]
		public void Successful_Audit_Writes_To_Log()
		{
			// Arrange
			var chain = R.Chain.WithState(false);

			static IR<int, TState> l0<TResult, TState>(IOk<TResult, TState> r) => r.OkV(18);
			static IR<TResult, TState> l1<TResult, TState>(IOkV<TResult, TState> r) => r.Error();

			var log = new List<string>();
			void audit<TResult, TState>(IR<TResult, TState> r)
			{
				if (r is IError<TResult, TState> error)
				{
					log.Add("Error!");
				}
				else if (r is IOkV<TResult, TState> ok)
				{
					log.Add($"Value: {ok.Val}");
				}
				else
				{
					log.Add("Unknown state.");
				}
			}

			var expected = new[] { "Unknown state.", "Value: 18", "Error!" }.ToList();

			// Act
			chain
				.Audit(audit)
				.LinkMap(l0)
				.Audit(audit)
				.LinkMap(l1)
				.Audit(audit);

			// Assert
			Assert.Equal(expected, log);
		}

		[Fact]
		public void Unsuccessful_Audit_Captures_Exception()
		{
			// Arrange
			var chain = R.Chain.WithState(false);
			static void audit<TResult, TState>(IR<TResult, TState> _) => throw new Exception();

			// Act
			var result = chain.Audit(audit);

			// Assert
			Assert.Equal(1, result.Messages.Count);
			Assert.True(result.Messages.Contains<Jm.AuditException>());
		}
	}
}
