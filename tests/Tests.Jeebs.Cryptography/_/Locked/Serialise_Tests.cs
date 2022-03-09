// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Cryptography.Functions;
using Jeebs.Functions;

namespace Jeebs.Cryptography.Locked_Tests;

public class Serialise_Tests
{
	[Fact]
	public void No_EncryptedContents_Returns_Empty_Json()
	{
		// Arrange
		var box = new Locked<string>();

		// Act
		var result = box.Serialise();

		// Assert
		var some = result.AssertSome();
		Assert.Equal(JsonF.Empty, some);
	}

	[Fact]
	public void Returns_Json()
	{
		// Arrange
		var value = Rnd.Str;
		var key = CryptoF.GenerateKey().UnsafeUnwrap();
		var box = new Locked<string>(value, key);
		var json = string.Format("{{\"encryptedContents\":\"{0}\",\"salt\":\"{1}\",\"nonce\":\"{2}\"}}",
			Convert.ToBase64String(box.EncryptedContents.UnsafeUnwrap()),
			Convert.ToBase64String(box.Salt),
			Convert.ToBase64String(box.Nonce)
		);

		// Act
		var result = box.Serialise();

		// Assert
		var some = result.AssertSome();
		Assert.Equal(json, some);
	}
}
