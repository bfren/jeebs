using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Jeebs;
using Xunit;

namespace Tests.Jeebs.Result
{
	public class MsgListTests
	{
		[Fact]
		public void Count_Returns_Zero_For_No_Messages()
		{
			// Arrange
			var l = new MsgList();

			// Act

			// Assert
			Assert.Equal(0, l.Count);
		}

		[Fact]
		public void Add_Adds_Message_To_List()
		{
			// Arrange
			var l = new MsgList();

			// Act
			l.Add<TestMsg>();
			l.Add(new TestMsg());

			// Assert
			Assert.Equal(2, l.Count);
		}

		[Fact]
		public void AddRange_Does_Nothing_When_No_Messages_Given()
		{
			// Arrange
			var l = new MsgList();

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
			var l = new MsgList();

			var m0 = new Jm.WithStringMsg("zero");
			var m1 = new Jm.WithStringMsg("one");
			var ms = new List<string>(new[] { "zero", "one" });

			// Act
			l.AddRange(m0, m1);

			// Assert
			Assert.Equal(2, l.Count);
			Assert.Equal(ms, l.GetAll());
		}

		[Fact]
		public void Contains_Returns_False_When_No_Message()
		{
			// Arrange
			var l0 = new MsgList();
			var l1 = new MsgList();

			// Act
			l1.Add<TestMsg>();

			// Assert
			Assert.False(l0.Contains<TestMsg>());
			Assert.False(l1.Contains<Jm.WithStringMsg>());
		}

		[Fact]
		public void Contains_Matches_Message_Added_As_Object()
		{
			// Arrange
			var l = new MsgList();
			var m = new TestMsg();

			// Act
			l.Add(m);

			// Assert
			Assert.True(l.Contains<TestMsg>());
		}

		[Fact]
		public void Contains_Matches_Message_Added_As_Type()
		{
			// Arrange
			var l = new MsgList();

			// Act
			l.Add<TestMsg>();

			// Assert
			Assert.True(l.Contains<TestMsg>());
		}

		[Fact]
		public void Get_Returns_EmptyList_When_No_Matching_Messages()
		{
			// Arrange
			var l = new MsgList();

			// Act
			l.Add<TestMsg>();

			// Assert
			Assert.Empty(l.Get<Jm.WithStringMsg>());
		}

		[Fact]
		public void Get_Returns_Only_Matching_Messages()
		{
			// Arrange
			var l = new MsgList();
			var m = new Jm.WithStringMsg("Hello, world!");

			// Act
			l.Add<TestMsg>();
			l.Add<TestMsg>();
			l.Add(m);

			// Assert
			Assert.Equal(3, l.Count);
			Assert.Equal(2, l.Get<TestMsg>().Count);
		}

		[Fact]
		public void GetAll_Returns_EmptyList_When_No_Messages()
		{
			// Arrange
			var l = new MsgList();

			// Act

			// Assert
			Assert.Empty(l.GetAll());
		}

		[Fact]
		public void GetAll_Returns_List_Of_Message_Strings()
		{
			// Arrange
			var l = new MsgList();

			var m0 = new Jm.WithStringMsg("zero");
			var m1 = new Jm.WithStringMsg("one");
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
			var l = new MsgList();
			var t = typeof(MsgList).FullName;

			// Act

			// Assert
			Assert.Equal(t, l.ToString());
		}

		[Fact]
		public void ToString_Returns_Message_Strings_On_NewLines()
		{
			// Arrange
			var l = new MsgList();

			var m0 = new Jm.WithStringMsg("zero");
			var m1 = new Jm.WithStringMsg("one");
			const string str = "zero\none";

			// Act
			l.AddRange(m0, m1);

			// Assert
			Assert.Equal(str, l.ToString());
		}

		private class TestMsg : IMsg { }
	}
}
