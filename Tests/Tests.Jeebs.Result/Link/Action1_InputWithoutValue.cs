using System;
using System.Collections.Generic;
using System.Text;
using Jeebs;
using Xunit;

namespace Tests.Jeebs.Result.Link
{
	public class Action1_InputWithoutValue
	{
		[Fact]
		public void Successful_Returns_Ok()
		{
			// Arrange
			var chain = R.Chain;
			var index = 0;
			void a(Ok<object> _) => index++;

			// Act
			var r = chain.Link(a);

			// Assert
			Assert.IsType<Ok>(r);
			Assert.Equal(1, index);
		}

		[Fact]
		public void Unsuccessful_Returns_Error()
		{
			// Arrange
			var chain = R.Chain;
			static void a(Ok<object> _) => throw new Exception("Something went wrong.");

			// Act
			var r = chain.Link(a);

			// Assert
			Assert.IsType<Error<object>>(r);
		}

		[Fact]
		public void Unsuccessful_Adds_Exception_Message()
		{
			// Arrange
			var chain = R.Chain;
			const string msg = "Something went wrong.";
			var exMsg = $"System.Exception: {msg}";
			static void a(Ok<object> _) => throw new Exception(msg);

			// Act
			var r = chain.Link(a);

			// Assert
			Assert.True(r.Messages.Contains<Jm.Exception>());
			Assert.Equal(exMsg, r.Messages.ToString());
		}

		[Fact]
		public void Unsuccessful_Then_SkipsAhead()
		{
			// Arrange
			var chain = R.Chain;
			var index = 0;
			static void a0(Ok<object> _) => throw new Exception("Something went wrong.");
			void a1(Ok<object> _) => index++;

			// Act
			var r = chain.Link(a0).Link(a1);

			// Assert
			Assert.Equal(0, index);
		}
	}
}
