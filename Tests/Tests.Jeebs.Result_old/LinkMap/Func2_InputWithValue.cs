using System;
using System.Collections.Generic;
using System.Text;
using Jeebs;
using Xunit;

namespace Jeebs_old.LinkMap
{
	public class Func2_InputWithValue : ILinkMap_Func2_InputWithValue
	{
		[Fact]
		public void Successful_Returns_OkV()
		{
			// Arrange
			const int value = 18;
			const string str = "18";
			var chain = R<int>.ChainV(value);
			static IR<string> f(IOkV<int> r) => r.OkV(r.Val.ToString());

			// Act
			var r = chain.LinkMap(f);

			// Assert
			var cast = Assert.IsAssignableFrom<IOkV<string>>(r);
			Assert.Equal(str, cast.Val);
		}

		[Fact]
		public void Unsuccessful_Returns_Error()
		{
			// Arrange
			var chain = R<int>.ChainV(18);
			static IR<string> f(IOkV<int> _) => throw new Exception("Something went wrong.");

			// Act
			var r = chain.LinkMap(f);

			// Assert
			Assert.IsAssignableFrom<IError<string>>(r);
		}

		[Fact]
		public void Unsuccessful_Adds_Exception_Message()
		{
			// Arrange
			var chain = R<int>.ChainV(18);
			const string msg = "Something went wrong.";
			var exMsg = $"System.Exception: {msg}";
			static IR<string> f(IOkV<int> _) => throw new Exception(msg);

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
			int value = 18;
			var chain = R.Chain;
			static IR<string> f0(IOkV<bool> _) => throw new Exception("Something went wrong.");
			IR<int> f1(IOkV<string> r) { value = 0; return r.OkV(value); }

			// Act
			var r = chain.LinkMap(f0).LinkMap(f1);

			// Assert
			Assert.Equal(18, value);
		}
	}
}

