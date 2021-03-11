// Jeebs Unit Tests
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Jeebs.OptionAbstractions.IMsg_Tests
{
	public class ToString_Tests
	{
		[Fact]
		public void Returns_Type_Name()
		{
			// Arrange
			var message = new TestMsg();

			// Act
			var result = message.ToString();

			// Assert
			Assert.Equal(typeof(TestMsg).FullName, result);
		}

		public class TestMsg : IMsg { }
	}
}
