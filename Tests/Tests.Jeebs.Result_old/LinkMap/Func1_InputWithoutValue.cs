using System;
using System.Collections.Generic;
using System.Text;
using Jeebs;
using Xunit;

namespace Tests.Jeebs.Result_old.LinkMap
{
	public class Func1_InputWithoutValue : ILinkMap_Func1_InputWithoutValue
	{
		[Fact]
		public void Successful_Returns_Ok()
		{
			// Arrange
			var chain = R.Chain;
			var index = 0;
			IR<string> f(IOk<bool> r) { index++; return r.OkNew<string>(); }

			// Act
			var r = chain.LinkMap(f);

			// Assert
			Assert.IsAssignableFrom<IOk<string>>(r);
			Assert.Equal(1, index);
		}

		[Fact]
		public void Unsuccessful_Returns_Error()
		{
			// Arrange
			var chain = R.Chain;
			static IR<string> f(IOk<bool> _) => throw new Exception("Something went wrong.");

			// Act
			var r = chain.LinkMap(f);

			// Assert
			Assert.IsType<Error<string>>(r);
		}

		[Fact]
		public void Unsuccessful_Adds_Exception_Message()
		{
			// Arrange
			var chain = R.Chain;
			const string msg = "Something went wrong.";
			var exMsg = $"System.Exception: {msg}";
			static IR<string> f(IOk<bool> _) => throw new Exception(msg);

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
			var chain = R.Chain;
			var index = 0;
			static IR<string> f0(IOk<bool> _) => throw new Exception("Something went wrong.");
			IR<int> f1(IOk<string> r) { index++; return r.OkNew<int>(); }

			// Act
			var r = chain.LinkMap(f0).LinkMap(f1);

			// Assert
			Assert.Equal(0, index);
		}
	}
}
