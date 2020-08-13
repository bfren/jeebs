using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Runtime.InteropServices;
using System.Text;
using Microsoft.VisualStudio.TestPlatform.ObjectModel;
using NSubstitute;
using Xunit;

namespace Jeebs
{
	public class ChangeType_Tests
	{
		private void Keeps_Base_Interface<TBase, TValue>(IR result)
		{
			// Arrange

			// Act
			var next = result.ChangeType().To<TValue>();

			// Assert
			Assert.IsAssignableFrom<TBase>(next);
			Assert.IsAssignableFrom<IR<TValue>>(next);
		}

		[Fact]
		public void To_IOk_Returns_IOk_New_Type()
		{
			Keeps_Base_Interface<IOk, string>(R.Ok());
			Keeps_Base_Interface<IOk, string>(R.Ok<int>());
			Keeps_Base_Interface<IOk, string>(R.OkV(18));
		}

		[Fact]
		public void To_IError_Returns_IOk_New_Type()
		{
			Keeps_Base_Interface<IError, string>(R.Error());
			Keeps_Base_Interface<IError, string>(R.Error<int>());
		}

		private void Keeps_State<TBase, TValue, TState>(IR<TValue, TState> result, TState state)
			where TBase : IR
		{
			// Arrange

			// Act
			var next = result.ChangeType().To<string>();

			// Assert
			Assert.IsAssignableFrom<TBase>(next);
			Assert.IsAssignableFrom<IR<string, TState>>(next);
			Assert.Equal(state, next.State);
		}

		[Fact]
		public void To_IOk_With_State_Returns_IOk_New_Type_Same_State()
		{
			const int state = 7;
			Keeps_State<IOk, bool, int>(R.Ok(state), state);
			Keeps_State<IOk, int, int>(R.OkV(18, state), state);
		}

		[Fact]
		public void To_IError_With_State_Returns_IError_New_Type_Same_State()
		{
			const int state = 7;
			Keeps_State<IError, bool, int>(R.Error(state), state);
		}

		private void Keeps_Messages<TResult>(TResult result)
			where TResult : IR
		{
			// Arrange
			var m0 = new IntMsg(18);
			var m1 = new StringMsg("July");
			var r = result.AddMsg(m0, m1);

			// Act
			var next = r.ChangeType().To<string>();

			// Assert
			Assert.Equal(2, next.Messages.Count);
			Assert.True(next.Messages.Contains<IntMsg>());
			Assert.True(next.Messages.Contains<StringMsg>());
		}

		[Fact]
		public void To_IOk_Keeps_Messages()
		{
			Keeps_Messages(R.Ok());
			Keeps_Messages(R.Ok<int>());
			Keeps_Messages(R.OkV(18));
		}

		[Fact]
		public void To_IError_Keeps_Messages()
		{
			Keeps_Messages(R.Error());
			Keeps_Messages(R.Error<int>());
		}

		[Fact]
		public void To_Unknown_Implementation_Throws_Invalid_Exception()
		{
			// Arrange
			var r0 = Substitute.For<IR>();
			var r1 = Substitute.For<IR<string>>();
			var r2 = Substitute.For<IR<int, bool>>();

			// Act
			Action a0 = () => r0.ChangeType().To<int>();
			Action a1 = () => r1.ChangeType().To<int>();
			Action a2 = () => r2.ChangeType().To<int>();

			// Assert
			Assert.Throws<InvalidOperationException>(a0);
			Assert.Throws<InvalidOperationException>(a1);
			Assert.Throws<InvalidOperationException>(a2);
		}

		public class IntMsg : Jm.WithValueMsg<int>
		{
			public IntMsg(int value) : base(value) { }
		}

		public class StringMsg : Jm.WithValueMsg<string>
		{
			public StringMsg(string value) : base(value) { }
		}
	}
}
