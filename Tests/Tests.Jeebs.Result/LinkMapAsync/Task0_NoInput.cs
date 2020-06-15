using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Jeebs;
using Xunit;

namespace Tests.Jeebs.Result.LinkMapAsync
{
	public class Task0_NoInput
	{
		[Fact]
		public async Task StartSync_Successful_Returns_OkWithValue()
		{
			// Arrange
			const string msg = "Hello, world!";
			var chain = R.Chain;
			static async Task<string> t() => await Task.Run(() => msg);

			// Act
			var r = await chain.LinkMapAsync(t);

			// Assert
			Assert.IsType<OkV<string>>(r);
			Assert.Equal(msg, ((OkV<string>)r).Val);
		}

		[Fact]
		public async Task StartSync_Unsuccessful_Returns_Error()
		{
			// Arrange
			var chain = R.Chain;
			static async Task<string> t() => throw new Exception("Something went wrong.");

			// Act
			var r = await chain.LinkMapAsync(t);

			// Assert
			Assert.IsType<Error<string>>(r);
		}

		[Fact]
		public async Task StartSync_Unsuccessful_Adds_Exception_Message()
		{
			// Arrange
			var chain = R.Chain;
			const string msg = "Something went wrong.";
			var exMsg = $"System.Exception: {msg}";
			static async Task<string> t() => throw new Exception(msg);

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
			var chain = R.Chain;
			var index = 0;
			static async Task<string> t0() => throw new Exception("Something went wrong.");
			async Task<int> t1() => await Task.Run(() => index++);

			// Act
			var r = await chain.LinkMapAsync(t0).LinkMapAsync(t1);

			// Assert
			Assert.Equal(0, index);
		}

		[Fact]
		public async Task StartAsync_Successful_Returns_OkWithValue()
		{
			// Arrange
			const string msg = "Hello, world!";
			var chain = R.ChainAsync;
			static async Task<string> t() => await Task.Run(() => msg);

			// Act
			var r = await chain.LinkMapAsync(t);

			// Assert
			Assert.IsType<OkV<string>>(r);
			Assert.Equal(msg, ((OkV<string>)r).Val);
		}

		[Fact]
		public async Task StartAsync_Unsuccessful_Returns_Error()
		{
			// Arrange
			var chain = R.ChainAsync;
			static async Task<string> t() => throw new Exception("Something went wrong.");

			// Act
			var r = await chain.LinkMapAsync(t);

			// Assert
			Assert.IsType<Error<string>>(r);
		}

		[Fact]
		public async Task StartAsync_Unsuccessful_Adds_Exception_Message()
		{
			// Arrange
			var chain = R.ChainAsync;
			const string msg = "Something went wrong.";
			var exMsg = $"System.Exception: {msg}";
			static async Task<string> t() => throw new Exception(msg);

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
			var chain = R.ChainAsync;
			var index = 0;
			static async Task<string> t0() => throw new Exception("Something went wrong.");
			async Task<int> t1() => await Task.Run(() => index++);

			// Act
			var r = await chain.LinkMapAsync(t0).LinkMapAsync(t1);

			// Assert
			Assert.Equal(0, index);
		}
	}
}
