using System;
using System.Collections.Generic;
using System.Text;
using Jeebs;
using Xunit;

namespace Tests.Jeebs.Result.Link
{
	public class Action1_InputWithoutValue_WithState : ILink_Action1_InputWithoutValue
	{
		[Fact]
		public void Successful_Returns_Ok()
		{
			// Arrange
			var chain = R.Chain.WithState(false);
			var index = 0;
			void a(IOk<bool, bool> _) => index++;

			// Act
			var r = chain.Link(a);

			// Assert
			Assert.IsAssignableFrom<IOk<bool, bool>>(r);
			Assert.Equal(1, index);
		}

		[Fact]
		public void Unsuccessful_Returns_Error()
		{
			// Arrange
			var chain = R.Chain.WithState(false);
			static void a(IOk<bool, bool> _) => throw new Exception("Something went wrong.");

			// Act
			var r = chain.Link(a);

			// Assert
			Assert.IsAssignableFrom<IError<bool, bool>>(r);
		}

		[Fact]
		public void Unsuccessful_Adds_Exception_Message()
		{
			// Arrange
			var chain = R.Chain.WithState(false);
			const string msg = "Something went wrong.";
			var exMsg = $"System.Exception: {msg}";
			static void a(IOk<bool, bool> _) => throw new Exception(msg);

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
			var chain = R.Chain.WithState(false);
			var index = 0;
			static void a0(IOk<bool, bool> _) => throw new Exception("Something went wrong.");
			void a1(IOk<bool, bool> _) => index++;

			// Act
			var r = chain.Link(a0).Link(a1);

			// Assert
			Assert.Equal(0, index);
		}
	}
}
