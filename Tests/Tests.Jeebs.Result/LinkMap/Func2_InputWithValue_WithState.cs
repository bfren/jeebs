using System;
using System.Collections.Generic;
using System.Text;
using Jeebs;
using Xunit;

namespace Tests.Jeebs.Result.LinkMap
{
	public class Func2_InputWithValue_WithState : ILinkMap_Func2_InputWithValue
	{
		[Fact]
		public void Successful_Returns_OkV()
		{
			// Arrange
			const int value = 18;
			const string str = "18";
			var chain = R<int, bool>.ChainV(value, false);
			static IR<string, bool> f(IOkV<int, bool> r) => r.OkV(r.Val.ToString());

			// Act
			var r = chain.LinkMap(f);

			// Assert
			Assert.IsAssignableFrom<IOkV<string, bool>>(r);
			Assert.Equal(str, ((IOkV<string, bool>)r).Val);
		}

		[Fact]
		public void Unsuccessful_Returns_Error()
		{
			// Arrange
			var chain = R<int, bool>.ChainV(18, false);
			static IR<string, bool> f(IOkV<int, bool> _) => throw new Exception("Something went wrong.");

			// Act
			var r = chain.LinkMap(f);

			// Assert
			Assert.IsAssignableFrom<IError<string, bool>>(r);
		}

		[Fact]
		public void Unsuccessful_Adds_Exception_Message()
		{
			// Arrange
			var chain = R<int, bool>.ChainV(18, false);
			const string msg = "Something went wrong.";
			var exMsg = $"System.Exception: {msg}";
			static IR<string, bool> f(IOkV<int, bool> _) => throw new Exception(msg);

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
			var chain = R.Chain.WithState(false);
			static IR<string, bool> f0(IOkV<bool, bool> _) => throw new Exception("Something went wrong.");
			IR<int, bool> f1(IOkV<string, bool> r) { value = 0; return r.OkV(value); }

			// Act
			var r = chain.LinkMap(f0).LinkMap(f1);

			// Assert
			Assert.Equal(18, value);
		}
	}
}

