using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Jeebs;
using Xunit;

namespace Jeebs_old.LinkMapAsync
{
	public class Task2_InputWithValue : ILinkMapAsync_Task2_InputWithValue
	{
		[Fact]
		public async Task StartSync_Successful_Returns_OkV()
		{
			// Arrange
			const int value = 18;
			const string str = "18";
			var chain = R.Chain.OkV(value);
			async Task<IR<string>> t(IOkV<int> r) => await Task.Run(() => r.OkV(r.Val.ToString()));

			// Act
			var r = await chain.LinkMapAsync(t);

			// Assert
			var cast = Assert.IsAssignableFrom<IOkV<string>>(r);
			Assert.Equal(str, cast.Val);
		}

		[Fact]
		public async Task StartSync_Unsuccessful_Returns_Error()
		{
			// Arrange
			var chain = R.Chain.OkV(18);
			static async Task<IR<string>> t(IOkV<int> _) => throw new Exception("Something went wrong.");

			// Act
			var r = await chain.LinkMapAsync(t);

			// Assert
			Assert.IsAssignableFrom<IError<string>>(r);
		}

		[Fact]
		public async Task StartSync_Unsuccessful_Adds_Exception_Message()
		{
			// Arrange
			var chain = R.Chain.OkV(18);
			const string msg = "Something went wrong.";
			var exMsg = $"System.Exception: {msg}";
			static async Task<IR<string>> t(IOkV<int> _) => throw new Exception(msg);

			// Act
			var r = await chain.LinkMapAsync(t);

			// Assert
			Assert.True(r.Messages.Contains<Jm.AsyncException>());
			Assert.Equal(exMsg, r.Messages.ToString());
		}

		[Fact]
		public async Task StartSync_Unsuccessful_Then_SkipsAhead()
		{
			// Arrange
			int value = 18;
			var chain = R.Chain.Ok();
			static async Task<IR<string>> t0(IOkV<bool> _) => throw new Exception("Something went wrong.");
			async Task<IR<int>> t1(IOkV<string> r) { value = 0; return r.OkV(value); }

			// Act
			var r = await chain.LinkMapAsync(t0).LinkMapAsync(t1);

			// Assert
			Assert.Equal(18, value);
		}

		[Fact]
		public async Task StartAsync_Successful_Returns_OkV()
		{
			// Arrange
			const int value = 18;
			const string str = "18";
			var chain = R<int>.ChainVAsync(value);
			async Task<IR<string>> t(IOkV<int> r) => await Task.Run(() => r.OkV(r.Val.ToString()));

			// Act
			var r = await chain.LinkMapAsync(t);

			// Assert
			var cast = Assert.IsAssignableFrom<IOkV<string>>(r);
			Assert.Equal(str, cast.Val);
		}

		[Fact]
		public async Task StartAsync_Unsuccessful_Returns_Error()
		{
			// Arrange
			const int value = 18;
			var chain = R<int>.ChainVAsync(value);
			static async Task<IR<string>> t(IOkV<int> _) => throw new Exception("Something went wrong.");

			// Act
			var r = await chain.LinkMapAsync(t);

			// Assert
			Assert.IsAssignableFrom<IError<string>>(r);
		}

		[Fact]
		public async Task StartAsync_Unsuccessful_Adds_Exception_Message()
		{
			// Arrange
			const int value = 18;
			var chain = R<int>.ChainVAsync(value);
			const string msg = "Something went wrong.";
			var exMsg = $"System.Exception: {msg}";
			static async Task<IR<string>> t(IOkV<int> _) => throw new Exception(msg);

			// Act
			var r = await chain.LinkMapAsync(t);

			// Assert
			Assert.True(r.Messages.Contains<Jm.AsyncException>());
			Assert.Equal(exMsg, r.Messages.ToString());
		}

		[Fact]
		public async Task StartAsync_Unsuccessful_Then_SkipsAhead()
		{
			// Arrange
			int value = 18;
			var chain = R.ChainAsync;
			static async Task<IR<string>> t0(IOkV<bool> _) => throw new Exception("Something went wrong.");
			async Task<IR<int>> t1(IOkV<string> r) { value = 0; return r.OkV(value); }

			// Act
			var r = await chain.LinkMapAsync(t0).LinkMapAsync(t1);

			// Assert
			Assert.Equal(18, value);
		}
	}
}
