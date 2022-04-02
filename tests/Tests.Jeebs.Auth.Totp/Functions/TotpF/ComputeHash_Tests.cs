// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System.Security.Cryptography;

namespace Jeebs.Auth.Totp.Functions.TotpF_Tests;

public class ComputeHash_Tests
{
	[Fact]
	public void Generates_256bit_Hash()
	{
		// Arrange
		var key = Rnd.ByteF.Get(8);
		var counter = Rnd.ByteF.Get(8);
		var expected = new HMACSHA256(key).ComputeHash(counter);

		// Act
		var result = TotpF.ComputeHash(key, counter);

		// Assert
		Assert.Equal(expected, result);
		Assert.Equal(32, result.Length);
	}

	[Fact]
	public void Generates_Same_Hash_For_Same_Inputs()
	{
		// Arrange
		var key = Rnd.ByteF.Get(8);
		var counter = Rnd.ByteF.Get(8);

		// Act
		var r0 = TotpF.ComputeHash(key, counter);
		var r1 = TotpF.ComputeHash(key, counter);

		// Assert
		Assert.Equal(r0, r1);
	}

	[Fact]
	public void Generates_Different_Hash_For_Different_Keys()
	{
		// Arrange
		var k0 = Rnd.ByteF.Get(8);
		var k1 = Rnd.ByteF.Get(8);
		var counter = Rnd.ByteF.Get(8);

		// Act
		var r0 = TotpF.ComputeHash(k0, counter);
		var r1 = TotpF.ComputeHash(k1, counter);

		// Assert
		Assert.NotEqual(r0, r1);
	}

	[Fact]
	public void Generates_Different_Hash_For_Different_Counters()
	{
		// Arrange
		var key = Rnd.ByteF.Get(8);
		var c0 = Rnd.ByteF.Get(8);
		var c1 = Rnd.ByteF.Get(8);

		// Act
		var r0 = TotpF.ComputeHash(key, c0);
		var r1 = TotpF.ComputeHash(key, c1);

		// Assert
		Assert.NotEqual(r0, r1);
	}
}
