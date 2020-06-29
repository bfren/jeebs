using System;
using System.Collections.Generic;
using System.Text;
using Jeebs;
using Xunit;

namespace Tests.Jeebs.Result
{
	public class ErrorTests_WithState : IErrorTests
	{
		#region Error

		[Fact]
		public void Error_Adds_Messages_Params_And_Returns_Error()
		{
			// Arrange
			var chain = R.Chain.WithState(false);
			var m0 = new Jm.WithInt32(0);
			var m1 = new Jm.WithString("1");

			// Act
			var result = chain.Error(m0, m1);

			// Assert
			Assert.Equal(2, result.Messages.Count);
			Assert.True(result.Messages.Contains<Jm.WithInt32>());
			Assert.True(result.Messages.Contains<Jm.WithString>());
			Assert.IsAssignableFrom<IError<bool, bool>>(result);
		}

		[Fact]
		public void Error_Adds_Message_ByType_And_Returns_Error()
		{
			// Arrange
			var chain = R.Chain.WithState(false);

			// Act
			var result = chain.Error<TestMessage>();

			// Assert
			Assert.Equal(1, result.Messages.Count);
			Assert.True(result.Messages.Contains<TestMessage>());
			Assert.IsAssignableFrom<IError<bool, bool>>(result);
		}

		[Fact]
		public void Error_Adds_Message_OfType_And_Returns_Error()
		{
			// Arrange
			var chain = R.Chain.WithState(false);

			// Act
			var result = chain.Error(new TestMessage());

			// Assert
			Assert.Equal(1, result.Messages.Count);
			Assert.True(result.Messages.Contains<TestMessage>());
			Assert.IsAssignableFrom<IError<bool, bool>>(result);
		}

		#endregion

		#region ErrorNew - Same Type

		[Fact]
		public void ErrorNew_Same_Type_Adds_Messages_Params_And_Returns_Error()
		{
			// Arrange
			var chain = R.Chain.WithState(false);
			var m0 = new Jm.WithInt32(0);
			var m1 = new Jm.WithString("1");

			// Act
			var result = chain.ErrorNew<bool>(m0, m1);

			// Assert
			Assert.Equal(2, result.Messages.Count);
			Assert.True(result.Messages.Contains<Jm.WithInt32>());
			Assert.True(result.Messages.Contains<Jm.WithString>());
			Assert.IsAssignableFrom<IError<bool, bool>>(result);
		}

		[Fact]
		public void ErrorNew_Same_Type_Adds_Message_ByType_And_Returns_Error()
		{
			// Arrange
			var chain = R.Chain.WithState(false);

			// Act
			var result = chain.ErrorNew<bool, TestMessage>();

			// Assert
			Assert.Equal(1, result.Messages.Count);
			Assert.True(result.Messages.Contains<TestMessage>());
			Assert.IsAssignableFrom<IError<bool, bool>>(result);
		}

		[Fact]
		public void ErrorNew_Same_Type_Adds_Message_OfType_And_Returns_Error()
		{
			// Arrange
			var chain = R.Chain.WithState(false);

			// Act
			var result = chain.ErrorNew<bool>(new TestMessage());

			// Assert
			Assert.Equal(1, result.Messages.Count);
			Assert.True(result.Messages.Contains<TestMessage>());
			Assert.IsAssignableFrom<IError<bool, bool>>(result);
		}

		#endregion

		#region ErrorNew - Different Type

		[Fact]
		public void ErrorNew_Different_Type_Adds_Messages_Params_And_Returns_Error()
		{
			// Arrange
			var chain = R.Chain.WithState(false);
			var m0 = new Jm.WithInt32(0);
			var m1 = new Jm.WithString("1");

			// Act
			var result = chain.ErrorNew<int>(m0, m1);

			// Assert
			Assert.Equal(2, result.Messages.Count);
			Assert.True(result.Messages.Contains<Jm.WithInt32>());
			Assert.True(result.Messages.Contains<Jm.WithString>());
			Assert.IsAssignableFrom<IError<int, bool>>(result);
		}

		[Fact]
		public void ErrorNew_Different_Type_Adds_Message_ByType_And_Returns_Error()
		{
			// Arrange
			var chain = R.Chain.WithState(false);

			// Act
			var result = chain.ErrorNew<int, TestMessage>();

			// Assert
			Assert.Equal(1, result.Messages.Count);
			Assert.True(result.Messages.Contains<TestMessage>());
			Assert.IsAssignableFrom<IError<int, bool>>(result);
		}

		[Fact]
		public void ErrorNew_Different_Type_Adds_Message_OfType_And_Returns_Error()
		{
			// Arrange
			var chain = R.Chain.WithState(false);

			// Act
			var result = chain.ErrorNew<int>(new TestMessage());

			// Assert
			Assert.Equal(1, result.Messages.Count);
			Assert.True(result.Messages.Contains<TestMessage>());
			Assert.IsAssignableFrom<IError<int, bool>>(result);
		}

		#endregion

		private class TestMessage : IMessage { }
	}
}
