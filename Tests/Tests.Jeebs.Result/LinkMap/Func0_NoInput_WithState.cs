using System;
using System.Collections.Generic;
using System.Text;
using Jeebs;
using Xunit;

namespace Tests.Jeebs.Result_old.LinkMap
{
	public class Func0_NoInput_WithState : ILinkMap_Func0_NoInput
	{
		[Fact]
		public void Successful_Returns_OkWithValue()
		{
			// Arrange
			const string msg = "Hello, world!";
			var chain = R<int, bool>.Chain(false);
			static string f() => msg;

			// Act
			var r = chain.LinkMap(f);

			// Assert
			Assert.IsAssignableFrom<IOkV<string, bool>>(r);
			Assert.Equal(msg, ((IOkV<string, bool>)r).Val);
		}

		[Fact]
		public void Unsuccessful_Returns_Error()
		{
			// Arrange
			var chain = R<int, bool>.Chain(false);
			static string f() => throw new Exception("Something went wrong.");

			// Act
			var r = chain.LinkMap(f);

			// Assert
			Assert.IsAssignableFrom<IError<string, bool>>(r);
		}

		[Fact]
		public void Unsuccessful_Adds_Exception_Message()
		{
			// Arrange
			var chain = R.Chain.AddState(false);
			const string msg = "Something went wrong.";
			var exMsg = $"System.Exception: {msg}";
			static string f() => throw new Exception(msg);

			// Act
			var r = chain.LinkMap(f);

			// Assert
			Assert.True(r.Messages.Contains<Jm.Exception>());
			Assert.Equal(exMsg, r.Messages.ToString());
		}

		[Fact]
		public void Unsuccessful_Then_SkipsAhead()
		{
			// Arrange
			var chain = R.Chain.AddState(false);
			var index = 0;
			static string f0() => throw new Exception("Something went wrong.");
			int f1() => index++;

			// Act
			var r = chain.LinkMap(f0).LinkMap(f1);

			// Assert
			Assert.Equal(0, index);
		}
	}
}
