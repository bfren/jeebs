﻿// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Xunit;
using static F.CryptoF;

namespace Jeebs.Cryptography.Locked_Tests;

public class Constructor_Tests
{
	[Fact]
	public void Creates_Random_Salt_And_Nonce()
	{
		// Arrange

		// Act
		var result = new Locked<int>();

		// Assert
		Assert.NotEmpty(result.Salt);
		Assert.NotEmpty(result.Nonce);
	}

	[Fact]
	public void Byte_Key_Fills_With_Encrypted_Contents()
	{
		// Arrange
		var value = F.Rnd.Str;
		var key = GenerateKey().UnsafeUnwrap();

		// Act
		var result = new Locked<string>(value, key);

		// Assert
		Assert.NotEmpty(result.Salt);
		Assert.NotEmpty(result.Nonce);
		var some = result.EncryptedContents.AssertSome();
		Assert.NotEmpty(some);
	}

	[Fact]
	public void String_Key_Fills_With_Encrypted_Contents()
	{
		// Arrange
		var value = F.Rnd.Str;
		var key = F.Rnd.Str;

		// Act
		var result = new Locked<string>(value, key);

		// Assert
		Assert.NotEmpty(result.Salt);
		Assert.NotEmpty(result.Nonce);
		var some = result.EncryptedContents.AssertSome();
		Assert.NotEmpty(some);
	}
}
