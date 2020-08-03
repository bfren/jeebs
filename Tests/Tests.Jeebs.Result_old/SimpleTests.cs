using System;
using System.Collections.Generic;
using System.Text;
using Jeebs;
using Xunit;

namespace Jeebs_old
{
	public class SimpleTests
	{
		[Fact]
		public void OkSimple_Adds_Messages_And_Returns_Ok()
		{
			// Arrange
			var chain = R<int>.Chain;
			var m0 = new Jm.WithInt32(0);
			var m1 = new Jm.WithString("1");

			// Act
			var result = chain.OkSimple(m0, m1);

			// Assert
			Assert.Equal(2, result.Messages.Count);
			Assert.True(result.Messages.Contains<Jm.WithInt32>());
			Assert.True(result.Messages.Contains<Jm.WithString>());
			Assert.IsAssignableFrom<IOk>(result);
		}

		[Fact]
		public void OkSimple_Adds_Message_ByType_And_Returns_Ok()
		{
			// Arrange
			var chain = R<int>.Chain;

			// Act
			var result = chain.OkSimple<TestMessage>();

			// Assert
			Assert.Equal(1, result.Messages.Count);
			Assert.True(result.Messages.Contains<TestMessage>());
			Assert.IsAssignableFrom<IOk>(result);
		}

		[Fact]
		public void OkSimple_Adds_Message_OfType_And_Returns_Ok()
		{
			// Arrange
			var chain = R<int>.Chain;

			// Act
			var result = chain.OkSimple(new TestMessage());

			// Assert
			Assert.Equal(1, result.Messages.Count);
			Assert.True(result.Messages.Contains<TestMessage>());
			Assert.IsAssignableFrom<IOk>(result);
		}

		[Fact]
		public void ErrorSimple_Adds_Messages_And_Returns_Error()
		{
			// Arrange
			var chain = R<int>.Chain;
			var m0 = new Jm.WithInt32(0);
			var m1 = new Jm.WithString("1");

			// Act
			var result = chain.ErrorSimple(m0, m1);

			// Assert
			Assert.Equal(2, result.Messages.Count);
			Assert.True(result.Messages.Contains<Jm.WithInt32>());
			Assert.True(result.Messages.Contains<Jm.WithString>());
			Assert.IsAssignableFrom<IError>(result);
		}

		[Fact]
		public void ErrorSimple_Adds_Message_ByType_And_Returns_Error()
		{
			// Arrange
			var chain = R<int>.Chain;

			// Act
			var result = chain.ErrorSimple<TestMessage>();

			// Assert
			Assert.Equal(1, result.Messages.Count);
			Assert.True(result.Messages.Contains<TestMessage>());
			Assert.IsAssignableFrom<IError>(result);
		}

		[Fact]
		public void ErrorSimple_Adds_Message_OfType_And_Returns_Error()
		{
			// Arrange
			var chain = R<int>.Chain;

			// Act
			var result = chain.ErrorSimple(new TestMessage());

			// Assert
			Assert.Equal(1, result.Messages.Count);
			Assert.True(result.Messages.Contains<TestMessage>());
			Assert.IsAssignableFrom<IError>(result);
		}

		private class TestMessage : IMessage { }
	}
}
