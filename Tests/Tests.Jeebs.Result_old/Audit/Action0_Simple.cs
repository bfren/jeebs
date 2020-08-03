using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Jeebs;
using Xunit;

namespace Jeebs_old.Audit
{
	public class Action0_Simple : IAudit_Action0_Simple
	{
		[Fact]
		public void Audit_Returns_Original_Object()
		{
			// Arrange
			var chain = R.Chain;
			static void audit<TResult>(IR<TResult> _) { }

			// Act
			var result = chain.Audit(audit);

			// Assert
			Assert.StrictEqual(chain, result);
		}

		[Fact]
		public void Successful_Audit_Writes_To_Log()
		{
			// Arrange
			var chain = R.Chain;

			static IR<int> l0<TResult>(IOk<TResult> r) => r.OkV(18);
			static IR<TResult> l1<TResult>(IOkV<TResult> r) => r.Error();

			var log = new List<string>();
			void audit<TResult>(IR<TResult> r)
			{
				if (r is IError<TResult> error)
				{
					log.Add("Error!");
				}
				else if (r is IOkV<TResult> ok)
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
			var chain = R.Chain;
			static void audit<TResult>(IR<TResult> _) => throw new Exception();

			// Act
			var result = chain.Audit(audit);

			// Assert
			Assert.Equal(1, result.Messages.Count);
			Assert.True(result.Messages.Contains<Jm.AuditException>());
		}
	}
}
