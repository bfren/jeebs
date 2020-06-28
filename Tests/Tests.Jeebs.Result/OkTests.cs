using System;
using System.Collections.Generic;
using System.Text;
using Jeebs;
using Xunit;

namespace Tests.Jeebs.Result
{
	public class OkTests
	{
		#region Ok

		[Fact]
		public void Ok_Adds_Messages_Params_And_Returns_Ok()
		{
			// Arrange
			var chain = R.Chain;
			var m0 = new Jm.WithInt32(0);
			var m1 = new Jm.WithString("1");

			// Act
			var result = chain.Ok(m0, m1);

			// Assert
			Assert.Equal(2, result.Messages.Count);
			Assert.True(result.Messages.Contains<Jm.WithInt32>());
			Assert.True(result.Messages.Contains<Jm.WithString>());
			Assert.IsAssignableFrom<IOk>(result);
			Assert.StrictEqual(chain, result);
		}

		[Fact]
		public void Ok_Adds_Message_ByType_And_Returns_Ok()
		{
			// Arrange
			var chain = R.Chain;

			// Act
			var result = chain.Ok<TestMessage>();

			// Assert
			Assert.Equal(1, result.Messages.Count);
			Assert.True(result.Messages.Contains<TestMessage>());
			Assert.IsAssignableFrom<IOk>(result);
			Assert.StrictEqual(chain, result);
		}

		[Fact]
		public void Ok_Adds_Message_OfType_And_Returns_Ok()
		{
			// Arrange
			var chain = R.Chain;

			// Act
			var result = chain.Ok(new TestMessage());

			// Assert
			Assert.Equal(1, result.Messages.Count);
			Assert.True(result.Messages.Contains<TestMessage>());
			Assert.IsAssignableFrom<IOk>(result);
			Assert.StrictEqual(chain, result);
		}

		#endregion

		#region OkNew - Same Type

		[Fact]
		public void OkNew_Same_Type_Adds_Messages_Params_And_Returns_Ok()
		{
			// Arrange
			var chain = R.Chain;
			var m0 = new Jm.WithInt32(0);
			var m1 = new Jm.WithString("1");

			// Act
			var result = chain.OkNew<bool>(m0, m1);

			// Assert
			Assert.Equal(2, result.Messages.Count);
			Assert.True(result.Messages.Contains<Jm.WithInt32>());
			Assert.True(result.Messages.Contains<Jm.WithString>());
			Assert.IsAssignableFrom<IOk>(result);
			Assert.StrictEqual(chain, result);
		}

		[Fact]
		public void OkNew_Same_Type_Adds_Message_ByType_And_Returns_Ok()
		{
			// Arrange
			var chain = R.Chain;

			// Act
			var result = chain.OkNew<bool, TestMessage>();

			// Assert
			Assert.Equal(1, result.Messages.Count);
			Assert.True(result.Messages.Contains<TestMessage>());
			Assert.IsAssignableFrom<IOk>(result);
			Assert.StrictEqual(chain, result);
		}

		[Fact]
		public void OkNew_Same_Type_Adds_Message_OfType_And_Returns_Ok()
		{
			// Arrange
			var chain = R.Chain;

			// Act
			var result = chain.OkNew<bool>(new TestMessage());

			// Assert
			Assert.Equal(1, result.Messages.Count);
			Assert.True(result.Messages.Contains<TestMessage>());
			Assert.IsAssignableFrom<IOk>(result);
			Assert.StrictEqual(chain, result);
		}

		#endregion

		#region OkNew - Different Type

		[Fact]
		public void OkNew_Different_Type_Adds_Messages_Params_And_Returns_New_Ok()
		{
			// Arrange
			var chain = R<int>.Chain;
			var m0 = new Jm.WithInt32(0);
			var m1 = new Jm.WithString("1");

			// Act
			var result = chain.OkNew<string>(m0, m1);

			// Assert
			Assert.Equal(2, result.Messages.Count);
			Assert.True(result.Messages.Contains<Jm.WithInt32>());
			Assert.True(result.Messages.Contains<Jm.WithString>());
			Assert.IsAssignableFrom<IOk<string>>(result);
		}

		[Fact]
		public void OkNew_Different_Type_Adds_Message_ByType_And_Returns_Ok()
		{
			// Arrange
			var chain = R<int>.Chain;

			// Act
			var result = chain.OkNew<string, TestMessage>();

			// Assert
			Assert.Equal(1, result.Messages.Count);
			Assert.True(result.Messages.Contains<TestMessage>());
			Assert.IsAssignableFrom<IOk<string>>(result);
		}

		[Fact]
		public void OkNew_Different_Type_Adds_Message_OfType_And_Returns_Ok()
		{
			// Arrange
			var chain = R<int>.Chain;

			// Act
			var result = chain.OkNew<string>(new TestMessage());

			// Assert
			Assert.Equal(1, result.Messages.Count);
			Assert.True(result.Messages.Contains<TestMessage>());
			Assert.IsAssignableFrom<IOk<string>>(result);
		}

		#endregion

		#region OkV - Same Type

		[Fact]
		public void OkV_Same_Type_Sets_Value_Adds_Messages_Params_And_Returns_OkV()
		{
			// Arrange
			var chain = R<int>.Chain;
			const int value = 18;
			var m0 = new Jm.WithInt32(0);
			var m1 = new Jm.WithString("1");

			// Act
			var result = chain.OkV(value, m0, m1);

			// Assert
			Assert.Equal(2, result.Messages.Count);
			Assert.True(result.Messages.Contains<Jm.WithInt32>());
			Assert.True(result.Messages.Contains<Jm.WithString>());
			Assert.IsAssignableFrom<IOkV<int>>(result);
			Assert.Equal(value, result.Val);
			Assert.NotStrictEqual(chain, result);
		}

		[Fact]
		public void OkV_Same_Type_Sets_Value_Adds_Message_ByType_And_Returns_OkV()
		{
			// Arrange
			var chain = R<int>.Chain;
			const int value = 18;

			// Act
			var result = chain.OkV<int, TestMessage>(value);

			// Assert
			Assert.Equal(1, result.Messages.Count);
			Assert.True(result.Messages.Contains<TestMessage>());
			Assert.IsAssignableFrom<IOkV<int>>(result);
			Assert.Equal(value, result.Val);
			Assert.NotStrictEqual(chain, result);
		}

		[Fact]
		public void OkV_Same_Type_Sets_Value_Adds_Message_OfType_And_Returns_OkV()
		{
			// Arrange
			var chain = R<int>.Chain;
			const int value = 18;

			// Act
			var result = chain.OkV(value, new TestMessage());

			// Assert
			Assert.Equal(1, result.Messages.Count);
			Assert.True(result.Messages.Contains<TestMessage>());
			Assert.IsAssignableFrom<IOkV<int>>(result);
			Assert.Equal(value, result.Val);
			Assert.NotStrictEqual(chain, result);
		}

		#endregion

		#region OkV - Different Type

		[Fact]
		public void OkV_Different_Type_Sets_Value_Adds_Messages_Params_And_Returns_OkV()
		{
			// Arrange
			var chain = R<string>.Chain;
			const int value = 18;
			var m0 = new Jm.WithInt32(0);
			var m1 = new Jm.WithString("1");

			// Act
			var result = chain.OkV(value, m0, m1);

			// Assert
			Assert.Equal(2, result.Messages.Count);
			Assert.True(result.Messages.Contains<Jm.WithInt32>());
			Assert.True(result.Messages.Contains<Jm.WithString>());
			Assert.IsAssignableFrom<IOkV<int>>(result);
			Assert.Equal(value, result.Val);
		}

		[Fact]
		public void OkV_Different_Type_Sets_Value_Adds_Message_ByType_And_Returns_OkV()
		{
			// Arrange
			var chain = R<string>.Chain;
			const int value = 18;

			// Act
			var result = chain.OkV<int, TestMessage>(value);

			// Assert
			Assert.Equal(1, result.Messages.Count);
			Assert.True(result.Messages.Contains<TestMessage>());
			Assert.IsAssignableFrom<IOkV<int>>(result);
			Assert.Equal(value, result.Val);
		}

		[Fact]
		public void OkV_Different_Type_Sets_Value_Adds_Message_OfType_And_Returns_OkV()
		{
			// Arrange
			var chain = R<string>.Chain;
			const int value = 18;

			// Act
			var result = chain.OkV(value, new TestMessage());

			// Assert
			Assert.Equal(1, result.Messages.Count);
			Assert.True(result.Messages.Contains<TestMessage>());
			Assert.IsAssignableFrom<IOkV<int>>(result);
			Assert.Equal(value, result.Val);
		}

		#endregion

		private class TestMessage : IMessage { }
	}
}
