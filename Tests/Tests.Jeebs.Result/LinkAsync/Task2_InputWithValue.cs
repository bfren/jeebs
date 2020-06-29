using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Jeebs;
using Xunit;

namespace Tests.Jeebs.Result.LinkAsync
{
	public class Task2_InputWithValue : ILinkAsync_Task2_InputWithValue
	{
		[Fact]
		public async Task StartSync_Successful_Returns_OkV()
		{
			// Arrange
			var chain = R<int>.ChainV(18);
			var index = 10;
			async Task t(IOkV<int> r) => await Task.Run(() => index += r.Val);

			// Act
			var r = await chain.LinkAsync(t);

			// Assert
			Assert.IsAssignableFrom<IOkV<int>>(r);
			Assert.Equal(28, index);
		}

		[Fact]
		public async Task StartSync_Unsuccessful_Returns_Error()
		{
			// Arrange
			var chain = R<int>.ChainV(18);
			static async Task t(IOkV<int> _) => await Task.Run(() => throw new Exception("Something went wrong."));

			// Act
			var r = await chain.LinkAsync(t);

			// Assert
			Assert.IsAssignableFrom<IError<int>>(r);
		}

		[Fact]
		public async Task StartSync_Unsuccessful_Adds_Exception_Message()
		{
			// Arrange
			var chain = R<int>.ChainV(18);
			const string msg = "Something went wrong.";
			var exMsg = $"System.Exception: {msg}";
			static async Task t(IOkV<int> _) => await Task.Run(() => throw new Exception(msg));

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
			var chain = R<int>.ChainV(18);
			var index = 10;
			static async Task t0(IOkV<int> _) => await Task.Run(() => throw new Exception("Something went wrong."));
			async Task t1(IOkV<int> r) => await Task.Run(() => index += r.Val);

			// Act
			var r = await chain.LinkAsync(t0).LinkAsync(t1);

			// Assert
			Assert.Equal(10, index);
		}
		[Fact]
		public async Task StartAsync_Successful_Returns_OkV()
		{
			// Arrange
			var chain = R<int>.ChainVAsync(18);
			var index = 10;
			async Task t(IOkV<int> r) => await Task.Run(() => index += r.Val);

			// Act
			var r = await chain.LinkAsync(t);

			// Assert
			Assert.IsAssignableFrom<IOkV<int>>(r);
			Assert.Equal(28, index);
		}

		[Fact]
		public async Task StartAsync_Unsuccessful_Returns_Error()
		{
			// Arrange
			var chain = R<int>.ChainVAsync(18);
			static async Task t(IOkV<int> _) => await Task.Run(() => throw new Exception("Something went wrong."));

			// Act
			var r = await chain.LinkAsync(t);

			// Assert
			Assert.IsAssignableFrom<IError<int>>(r);
		}

		[Fact]
		public async Task StartAsync_Unsuccessful_Adds_Exception_Message()
		{
			// Arrange
			var chain = R<int>.ChainVAsync(18);
			const string msg = "Something went wrong.";
			var exMsg = $"System.Exception: {msg}";
			static async Task t(IOkV<int> _) => await Task.Run(() => throw new Exception(msg));

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
			var chain = R<int>.ChainVAsync(18);
			var index = 10;
			static async Task t0(IOkV<int> _) => await Task.Run(() => throw new Exception("Something went wrong."));
			async Task t1(IOkV<int> r) => await Task.Run(() => index += r.Val);

			// Act
			var r = await chain.LinkAsync(t0).LinkAsync(t1);

			// Assert
			Assert.Equal(10, index);
		}
	}
}
