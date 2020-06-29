using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Jeebs;
using Xunit;

namespace Tests.Jeebs.Result.LinkMapAsync
{
	public class Task2_InputWithValue_WithState : ILinkMapAsync_Task2_InputWithValue
	{
		[Fact]
		public async Task StartSync_Successful_Returns_OkV()
		{
			// Arrange
			const int value = 18;
			const string str = "18";
			var chain = R<int, bool>.ChainV(value, false);
			async Task<IR<string, bool>> t(IOkV<int, bool> r) => await Task.Run(() => r.OkV(r.Val.ToString()));

			// Act
			var r = await chain.LinkMapAsync(t);

			// Assert
			Assert.IsAssignableFrom<IOkV<string, bool>>(r);
			Assert.Equal(str, ((IOkV<string, bool>)r).Val);
		}

		[Fact]
		public async Task StartSync_Unsuccessful_Returns_Error()
		{
			// Arrange
			var chain = R<int, bool>.ChainV(18, false);
			static async Task<IR<string, bool>> t(IOkV<int, bool> _) => throw new Exception("Something went wrong.");

			// Act
			var r = await chain.LinkMapAsync(t);

			// Assert
			Assert.IsAssignableFrom<IError<string, bool>>(r);
		}

		[Fact]
		public async Task StartSync_Unsuccessful_Adds_Exception_Message()
		{
			// Arrange
			var chain = R<int, bool>.ChainV(18, false);
			const string msg = "Something went wrong.";
			var exMsg = $"System.Exception: {msg}";
			static async Task<IR<string, bool>> t(IOkV<int, bool> _) => throw new Exception(msg);

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
			var chain = R.Chain.WithState(false);
			static async Task<IR<string, bool>> t0(IOkV<bool, bool> _) => throw new Exception("Something went wrong.");
			async Task<IR<int, bool>> t1(IOkV<string, bool> r) { value = 0; return r.OkV(value); }

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
			var chain = R<int, bool>.ChainVAsync(value, false);
			async Task<IR<string, bool>> t(IOkV<int, bool> r) => await Task.Run(() => r.OkV(r.Val.ToString()));

			// Act
			var r = await chain.LinkMapAsync(t);

			// Assert
			Assert.IsAssignableFrom<IOkV<string, bool>>(r);
			Assert.Equal(str, ((IOkV<string, bool>)r).Val);
		}

		[Fact]
		public async Task StartAsync_Unsuccessful_Returns_Error()
		{
			// Arrange
			const int value = 18;
			var chain = R<int, bool>.ChainVAsync(value, false);
			static async Task<IR<string, bool>> t(IOkV<int, bool> _) => throw new Exception("Something went wrong.");

			// Act
			var r = await chain.LinkMapAsync(t);

			// Assert
			Assert.IsAssignableFrom<IError<string, bool>>(r);
		}

		[Fact]
		public async Task StartAsync_Unsuccessful_Adds_Exception_Message()
		{
			// Arrange
			const int value = 18;
			var chain = R<int, bool>.ChainVAsync(value, false);
			const string msg = "Something went wrong.";
			var exMsg = $"System.Exception: {msg}";
			static async Task<IR<string, bool>> t(IOkV<int, bool> _) => throw new Exception(msg);

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
			var chain = R.ChainAsync.WithState(false);
			static async Task<IR<string, bool>> t0(IOkV<bool, bool> _) => throw new Exception("Something went wrong.");
			async Task<IR<int, bool>> t1(IOkV<string, bool> r) { value = 0; return r.OkV(value); }

			// Act
			var r = await chain.LinkMapAsync(t0).LinkMapAsync(t1);

			// Assert
			Assert.Equal(18, value);
		}
	}
}
