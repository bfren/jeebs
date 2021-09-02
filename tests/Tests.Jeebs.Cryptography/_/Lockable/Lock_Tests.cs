﻿// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Xunit;
using static Jeebs.Cryptography.Lockable.Msg;

namespace Jeebs.Cryptography.Lockable_Tests
{
	public class Lock_Tests
	{
		[Fact]
		public void Incorrect_Key_Length_Returns_None_With_InvalidKeyLengthMsg()
		{
			// Arrange
			var box = new Lockable<string>(F.Rnd.Str);
			var key = F.Rnd.ByteF.Get(20);

			// Act
			var result = box.Lock(key);

			// Assert
			var none = result.AssertNone();
			Assert.IsType<InvalidKeyLengthMsg>(none);
		}

		[Fact]
		public void Byte_Key_Returns_Locked()
		{
			// Arrange
			var box = new Lockable<string>(F.Rnd.Str);
			var key = F.Rnd.ByteF.Get(32);

			// Act
			var result = box.Lock(key);

			// Assert
			var some = result.AssertSome();
			Assert.NotNull(some.EncryptedContents);
		}

		[Fact]
		public void String_Key_Returns_Locked()
		{
			// Arrange
			var box = new Lockable<string>(F.Rnd.Str);
			var key = F.Rnd.Str;

			// Act
			var result = box.Lock(key);

			// Assert
			Assert.NotNull(result.EncryptedContents);
		}
	}
}
