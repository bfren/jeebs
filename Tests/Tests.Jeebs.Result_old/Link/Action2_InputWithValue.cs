using System;
using System.Collections.Generic;
using System.Text;
using Jeebs;
using Xunit;

namespace Jeebs_old.Link
{
	public class Action2_InputWithValue : ILink_Action2_InputWithValue
	{
		[Fact]
		public void Successful_Returns_OkV()
		{
			// Arrange
			var chain = R<int>.ChainV(18);
			var index = 10;
			void a(IOkV<int> r) => index += r.Val;

			// Act
			var r = chain.Link(a);

			// Assert
			Assert.IsAssignableFrom<IOkV<int>>(r);
			Assert.Equal(28, index);
		}

		[Fact]
		public void Unsuccessful_Returns_Error()
		{
			// Arrange
			var chain = R<int>.ChainV(18);
			static void a(IOkV<int> _) => throw new Exception("Something went wrong.");

			// Act
			var r = chain.Link(a);

			// Assert
			Assert.IsAssignableFrom<IError<int>>(r);
		}

		[Fact]
		public void Unsuccessful_Adds_Exception_Message()
		{
			// Arrange
			var chain = R<int>.ChainV(18);
			const string msg = "Something went wrong.";
			var exMsg = $"System.Exception: {msg}";
			static void a(IOkV<int> _) => throw new Exception(msg);

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
			var chain = R<int>.ChainV(18);
			var index = 10;
			static void a0(IOkV<int> _) => throw new Exception("Something went wrong.");
			void a1(IOkV<int> r) => index += r.Val;

			// Act
			var r = chain.Link(a0).Link(a1);

			// Assert
			Assert.Equal(10, index);
		}
	}
}
