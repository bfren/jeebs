using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Runtime.InteropServices;
using System.Text;
using Microsoft.VisualStudio.TestPlatform.ObjectModel;
using NSubstitute;
using Xunit;

namespace Jeebs.RExtensions_Tests
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
			Keeps_Base_Interface<IOk, string>(Result.Ok());
			Keeps_Base_Interface<IOk, string>(Result.Ok<int>());
			Keeps_Base_Interface<IOk, string>(Result.OkV(F.Rnd.Integer));
		}

		[Fact]
		public void To_IError_Returns_IOk_New_Type()
		{
			Keeps_Base_Interface<IError, string>(Result.Error());
			Keeps_Base_Interface<IError, string>(Result.Error<int>());
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
			Keeps_State<IOk, bool, int>(Result.Ok(state), state);
			Keeps_State<IOk, int, int>(Result.OkV(F.Rnd.Integer, state), state);
		}

		[Fact]
		public void To_IError_With_State_Returns_IError_New_Type_Same_State()
		{
			const int state = 7;
			Keeps_State<IError, bool, int>(Result.Error(state), state);
		}

		private void Keeps_Messages<TResult>(TResult result)
			where TResult : IR
		{
			// Arrange
			var m0 = new IntMsg(18);
			var m1 = new StringMsg(F.Rnd.String);
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
			Keeps_Messages(Result.Ok());
			Keeps_Messages(Result.Ok<int>());
			Keeps_Messages(Result.OkV(F.Rnd.Integer));
		}

		[Fact]
		public void To_IError_Keeps_Messages()
		{
			Keeps_Messages(Result.Error());
			Keeps_Messages(Result.Error<int>());
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
