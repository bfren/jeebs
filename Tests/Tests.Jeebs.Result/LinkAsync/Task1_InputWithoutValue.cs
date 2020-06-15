using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Jeebs;
using Xunit;

namespace Tests.Jeebs.Result.LinkAsync
{
	public class Task1_InputWithoutValue
	{
		[Fact]
		public async Task StartSync_Successful_Returns_Ok()
		{
			// Arrange
			var chain = R.Chain;
			var index = 0;
			async Task t(Ok<object> _) => await Task.Run(() => index++);

			// Act
			var r = await chain.LinkAsync(t);

			// Assert
			Assert.IsType<Ok>(r);
			Assert.Equal(1, index);
		}

		[Fact]
		public async Task StartSync_Unsuccessful_Returns_Error()
		{
			// Arrange
			var chain = R.Chain;
			static async Task t(Ok<object> _) => await Task.Run(() => throw new Exception("Something went wrong."));

			// Act
			var r = await chain.LinkAsync(t);

			// Assert
			Assert.IsType<Error<object>>(r);
		}

		[Fact]
		public async Task StartSync_Unsuccessful_Adds_AsyncException_Message()
		{
			// Arrange
			var chain = R.Chain;
			const string msg = "Something went wrong.";
			var exMsg = $"System.Exception: {msg}";
			static async Task t(Ok<object> _) => await Task.Run(() => throw new Exception(msg));

			// Act
			var r = await chain.LinkAsync(t);

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
			static async Task t0(Ok<object> _) => await Task.Run(() => throw new Exception("Something went wrong."));
			async Task t1(Ok<object> _) => await Task.Run(() => index++);

			// Act
			var r = await chain.LinkAsync(t0).LinkAsync(t1);

			// Assert
			Assert.Equal(0, index);
		}

		[Fact]
		public async Task StartAsync_Successful_Returns_Ok()
		{
			// Arrange
			var chain = R.ChainAsync;
			var index = 0;
			async Task t(Ok<object> _) => await Task.Run(() => index++);

			// Act
			var r = await chain.LinkAsync(t);

			// Assert
			Assert.IsType<Ok>(r);
			Assert.Equal(1, index);
		}

		[Fact]
		public async Task StartAsync_Unsuccessful_Returns_Error()
		{
			// Arrange
			var chain = R.ChainAsync;
			static async Task t(Ok<object> _) => await Task.Run(() => throw new Exception("Something went wrong."));

			// Act
			var r = await chain.LinkAsync(t);

			// Assert
			Assert.IsType<Error<object>>(r);
		}

		[Fact]
		public async Task StartAsync_Unsuccessful_Adds_AsyncException_Message()
		{
			// Arrange
			var chain = R.ChainAsync;
			const string msg = "Something went wrong.";
			var exMsg = $"System.Exception: {msg}";
			static async Task t(Ok<object> _) => await Task.Run(() => throw new Exception(msg));

			// Act
			var r = await chain.LinkAsync(t);

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
			static async Task t0(Ok<object> _) => await Task.Run(() => throw new Exception("Something went wrong."));
			async Task t1(Ok<object> _) => await Task.Run(() => index++);

			// Act
			var r = await chain.LinkAsync(t0).LinkAsync(t1);

			// Assert
			Assert.Equal(0, index);
		}
	}
}
