using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Jeebs;
using Xunit;

namespace Tests.Jeebs.Result.LinkMapAsync
{
	public class Task1_InputWithoutValue_WithState : ILinkMapAsync_Task1_InputWithoutValue
	{
		[Fact]
		public async Task StartSync_Successful_Returns_Ok()
		{
			// Arrange
			var chain = R.Chain.AddState(false);
			var index = 0;
			async Task<IR<string, bool>> t(IOk<bool, bool> r) { index++; return r.OkNew<string>(); }

			// Act
			var r = await chain.LinkMapAsync(t);

			// Assert
			Assert.IsAssignableFrom<IOk<string, bool>>(r);
			Assert.Equal(1, index);
		}

		[Fact]
		public async Task StartSync_Unsuccessful_Returns_Error()
		{
			// Arrange
			var chain = R.Chain.AddState(false);
			static async Task<IR<string, bool>> t(IOk<bool, bool> _) => throw new Exception("Something went wrong.");

			// Act
			var r = await chain.LinkMapAsync(t);

			// Assert
			Assert.IsAssignableFrom<IError<string, bool>>(r);
		}

		[Fact]
		public async Task StartSync_Unsuccessful_Adds_Exception_Message()
		{
			// Arrange
			var chain = R.Chain.AddState(false);
			const string msg = "Something went wrong.";
			var exMsg = $"System.Exception: {msg}";
			static async Task<IR<string, bool>> t(IOk<bool, bool> _) => throw new Exception(msg);

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
			var chain = R.Chain.AddState(false);
			var index = 0;
			static async Task<IR<string, bool>> t0(IOk<bool, bool> _) => throw new Exception("Something went wrong.");
			async Task<IR<int, bool>> t1(IOk<string, bool> r) { index++; return r.OkNew<int>(); }

			// Act
			var r = await chain.LinkMapAsync(t0).LinkMapAsync(t1);

			// Assert
			Assert.Equal(0, index);
		}

		[Fact]
		public async Task StartAsync_Successful_Returns_Ok()
		{
			// Arrange
			var chain = R.ChainAsync.AddState(false);
			var index = 0;
			async Task<IR<string, bool>> t(IOk<bool, bool> r) { index++; return r.OkNew<string>(); }

			// Act
			var r = await chain.LinkMapAsync(t);

			// Assert
			Assert.IsAssignableFrom<IOk<string, bool>>(r);
			Assert.Equal(1, index);
		}

		[Fact]
		public async Task StartAsync_Unsuccessful_Returns_Error()
		{
			// Arrange
			var chain = R.ChainAsync.AddState(false);
			static async Task<IR<string, bool>> t(IOk<bool, bool> _) => throw new Exception("Something went wrong.");

			// Act
			var r = await chain.LinkMapAsync(t);

			// Assert
			Assert.IsAssignableFrom<IError<string, bool>>(r);
		}

		[Fact]
		public async Task StartAsync_Unsuccessful_Adds_Exception_Message()
		{
			// Arrange
			var chain = R.ChainAsync.AddState(false);
			const string msg = "Something went wrong.";
			var exMsg = $"System.Exception: {msg}";
			static async Task<IR<string, bool>> t(IOk<bool, bool> _) => throw new Exception(msg);

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
			var chain = R.ChainAsync.AddState(false);
			var index = 0;
			static async Task<IR<string, bool>> t0(IOk<bool, bool> _) => throw new Exception("Something went wrong.");
			async Task<IR<int, bool>> t1(IOk<string, bool> r) { index++; return r.OkNew<int>(); }

			// Act
			var r = await chain.LinkMapAsync(t0).LinkMapAsync(t1);

			// Assert
			Assert.Equal(0, index);
		}
	}
}
