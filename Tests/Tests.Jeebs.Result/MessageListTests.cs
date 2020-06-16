using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Jeebs;
using NSubstitute;
using Xunit;

namespace Tests.Jeebs.Result
{
	public class MessageListTests
	{
		[Fact]
		public void Count_Returns_Zero_For_No_Messages()
		{
			// Arrange
			var l = new MessageList();

			// Act

			// Assert
			Assert.Equal(0, l.Count);
		}

		[Fact]
		public void Add_Adds_Message_To_List()
		{
			// Arrange
			var l = new MessageList();

			// Act
			l.Add<MessageTest>();
			l.Add(new MessageTest());

			// Assert
			Assert.Equal(2, l.Count);
		}

		[Fact]
		public void AddRange_Does_Nothing_When_No_Messages_Given()
		{
			// Arrange
			var l = new MessageList();

			// Act
			var exception = Record.Exception(() => l.AddRange());

			// Assert
			Assert.Null(exception);
			Assert.Equal(0, l.Count);
		}

		[Fact]
		public void AddRange_Adds_Messages_To_List()
		{
			// Arrange
			var l = new MessageList();

			var m0 = new Jm.WithString("zero");
			var m1 = new Jm.WithString("one");
			var ms = new List<string>(new[] { "zero", "one" });

			// Act
			l.AddRange(m0, m1);

			// Assert
			Assert.Equal(2, l.Count);
		}

		[Fact]
		public void Contains_Returns_False_When_No_Message()
		{
			// Arrange
			var l0 = new MessageList();
			var l1 = new MessageList();

			// Act
			l1.Add<MessageTest>();

			// Assert
			Assert.False(l0.Contains<MessageTest>());
			Assert.False(l1.Contains<Jm.WithString>());
		}

		[Fact]
		public void Contains_Matches_Message_Added_As_Object()
		{
			// Arrange
			var l = new MessageList();
			var m = new MessageTest();

			// Act
			l.Add(m);

			// Assert
			Assert.True(l.Contains<MessageTest>());
		}

		[Fact]
		public void Contains_Matches_Message_Added_As_Type()
		{
			// Arrange
			var l = new MessageList();

			// Act
			l.Add<MessageTest>();

			// Assert
			Assert.True(l.Contains<MessageTest>());
		}

		[Fact]
		public void Get_Returns_EmptyList_When_No_Matching_Messages()
		{
			// Arrange
			var l = new MessageList();

			// Act
			l.Add<MessageTest>();

			// Assert
			Assert.Empty(l.Get<Jm.WithString>());
		}

		[Fact]
		public void Get_Returns_Only_Matching_Messages()
		{
			// Arrange
			var l = new MessageList();
			var m = new Jm.WithString("Hello, world!");

			// Act
			l.Add<MessageTest>();
			l.Add<MessageTest>();
			l.Add(m);

			// Assert
			Assert.Equal(3, l.Count);
			Assert.Equal(2, l.Get<MessageTest>().Count);
		}

		[Fact]
		public void GetAll_Returns_EmptyList_When_No_Messages()
		{
			// Arrange
			var l = new MessageList();

			// Act

			// Assert
			Assert.Empty(l.GetAll());
		}

		[Fact]
		public void GetAll_Returns_List_Of_Message_Strings()
		{
			// Arrange
			var l = new MessageList();

			var m0 = new Jm.WithString("zero");
			var m1 = new Jm.WithString("one");
			var ms = new List<string>(new[] { "zero", "one" });

			// Act
			l.AddRange(m0, m1);

			// Assert
			Assert.Equal(ms, l.GetAll());
		}

		[Fact]
		public void ToString_Returns_TypeName_When_Empty()
		{
			// Arrange
			var l = new MessageList();
			var t = typeof(MessageList).FullName;

			// Act

			// Assert
			Assert.Equal(t, l.ToString());
		}

		[Fact]
		public void ToString_Returns_Message_Strings_On_NewLines()
		{
			// Arrange
			var l = new MessageList();

			var m0 = new Jm.WithString("zero");
			var m1 = new Jm.WithString("one");
			const string str = "zero\none";

			// Act
			l.AddRange(m0, m1);

			// Assert
			Assert.Equal(str, l.ToString());
		}

		private class MessageTest : IMessage { }
	}
}
