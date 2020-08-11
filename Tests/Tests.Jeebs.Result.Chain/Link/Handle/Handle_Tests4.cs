using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using NSubstitute;
using Xunit;

namespace Jeebs.LinkTests
{
	public partial class Handle_Tests
	{
		[Fact]
		public void No_Handler_With_Generic_Custom_ExceptionMsg_Adds_Msg()
		{
			// Arrange
			var chain = Chain.Create();
			static void throwGeneric() => throw new Exception();
			static void throwOther() => throw new DivideByZeroException();

			// Act
			chain.Link().Handle().With<CustomExceptionMsg>().Run(throwGeneric);
			chain.Link().Handle().With<CustomExceptionMsg>().Run(throwOther);
			var msg = chain.Messages.Get<CustomExceptionMsg>();

			// Assert
			Assert.Equal(2, msg.Count);
		}

		[Fact]
		public void No_Handler_With_Specific_Custom_ExceptionMsg_Adds_Msg()
		{
			// Arrange
			var chain = Chain.Create();
			static void throwGeneric() => throw new Exception();
			static void throwOther() => throw new DivideByZeroException();

			// Act
			chain.Link().Handle<DivideByZeroException>().With<CustomExceptionMsg>().Run(throwGeneric);
			chain.Link().Handle<DivideByZeroException>().With<CustomExceptionMsg>().Run(throwOther);
			chain.Link().Handle<DivideByZeroException>().With<CustomExceptionMsg>().Run(throwOther);
			var msg = chain.Messages.Get<CustomExceptionMsg>();

			// Assert
			Assert.Equal(2, msg.Count);
		}

		public class CustomExceptionMsg : Jm.ExceptionMsg { }
	}
}
