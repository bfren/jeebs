using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Jeebs;
using Xunit;

namespace Tests.Jeebs.Result.LinkMapAsync
{
	public class Task2_InputWithValue
	{
		[Fact]
		public async Task StartSync_Successful_Returns_OkV()
		{
			// Arrange
			const int value = 18;
			const string str = "18";
			var chain = R.Chain.OkV(value);
			async Task<R<string>> t(OkV<int> r) => await Task.Run(() => r.OkV(r.Val.ToString()));

			// Act
			var r = await chain.LinkMapAsync(t);

			// Assert
			Assert.IsType<OkV<string>>(r);
			Assert.Equal(str, ((OkV<string>)r).Val);
		}

		[Fact]
		public async Task StartSync_Unsuccessful_Returns_Error()
		{
			// Arrange
			var chain = R.Chain.OkV(18);
			static async Task<R<string>> t(OkV<int> _) => throw new Exception("Something went wrong.");

			// Act
			var r = await chain.LinkMapAsync(t);

			// Assert
			Assert.IsType<Error<string>>(r);
		}

		[Fact]
		public async Task StartSync_Unsuccessful_Adds_Exception_Message()
		{
			// Arrange
			var chain = R.Chain.OkV(18);
			const string msg = "Something went wrong.";
			var exMsg = $"System.Exception: {msg}";
			static async Task<R<string>> t(OkV<int> _) => throw new Exception(msg);

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
			static async Task<R<string>> t0(OkV<object> _) => throw new Exception("Something went wrong.");
			async Task<R<int>> t1(OkV<string> r) { value = 0; return r.OkV(value); }

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
			var chain = R.Chain.OkV(value).LinkAsync(() => Task.CompletedTask);
			async Task<R<string>> t(OkV<int> r) => await Task.Run(() => r.OkV(r.Val.ToString()));

			// Act
			var r = await chain.LinkMapAsync(t);

			// Assert
			Assert.IsType<OkV<string>>(r);
			Assert.Equal(str, ((OkV<string>)r).Val);
		}

		[Fact]
		public async Task StartAsync_Unsuccessful_Returns_Error()
		{
			// Arrange
			var chain = R.Chain.OkV(18).LinkAsync(() => Task.CompletedTask);
			static async Task<R<string>> t(OkV<int> _) => throw new Exception("Something went wrong.");

			// Act
			var r = await chain.LinkMapAsync(t);

			// Assert
			Assert.IsType<Error<string>>(r);
		}

		[Fact]
		public async Task StartAsync_Unsuccessful_Adds_Exception_Message()
		{
			// Arrange
			var chain = R.Chain.OkV(18).LinkAsync(() => Task.CompletedTask);
			const string msg = "Something went wrong.";
			var exMsg = $"System.Exception: {msg}";
			static async Task<R<string>> t(OkV<int> _) => throw new Exception(msg);

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
			var chain = R.Chain.Ok().LinkAsync(() => Task.CompletedTask);
			static async Task<R<string>> t0(OkV<object> _) => throw new Exception("Something went wrong.");
			async Task<R<int>> t1(OkV<string> r) { value = 0; return r.OkV(value); }

			// Act
			var r = await chain.LinkMapAsync(t0).LinkMapAsync(t1);

			// Assert
			Assert.Equal(18, value);
		}
	}
}
