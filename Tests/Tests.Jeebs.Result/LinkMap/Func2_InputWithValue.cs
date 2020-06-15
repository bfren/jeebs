using System;
using System.Collections.Generic;
using System.Text;
using Jeebs;
using Xunit;

namespace Tests.Jeebs.Result.LinkMap
{
	public class Func2_InputWithValue
	{
		[Fact]
		public void Successful_Returns_OkV()
		{
			// Arrange
			const int value = 18;
			const string str = "18";
			var chain = R.Chain.OkV(value);
			static R<string> f(OkV<int> r) => r.OkV(r.Val.ToString());

			// Act
			var r = chain.LinkMap(f);

			// Assert
			Assert.IsType<OkV<string>>(r);
			Assert.Equal(str, ((OkV<string>)r).Val);
		}

		[Fact]
		public void Unsuccessful_Returns_Error()
		{
			// Arrange
			var chain = R.Chain.OkV(18);
			static R<string> f(OkV<int> _) => throw new Exception("Something went wrong.");

			// Act
			var r = chain.LinkMap(f);

			// Assert
			Assert.IsType<Error<string>>(r);
		}

		[Fact]
		public void Unsuccessful_Adds_Exception_Message()
		{
			// Arrange
			var chain = R.Chain.OkV(18);
			const string msg = "Something went wrong.";
			var exMsg = $"System.Exception: {msg}";
			static R<string> f(OkV<int> _) => throw new Exception(msg);

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
			var chain = R.Chain.Ok();
			static R<string> f0(OkV<object> _) => throw new Exception("Something went wrong.");
			R<int> f1(OkV<string> r) { value = 0; return r.OkV(value); }

			// Act
			var r = chain.LinkMap(f0).LinkMap(f1);

			// Assert
			Assert.Equal(18, value);
		}
	}
}

