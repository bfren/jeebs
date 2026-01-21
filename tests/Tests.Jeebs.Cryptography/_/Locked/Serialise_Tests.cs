// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

namespace Jeebs.Cryptography.Locked_Tests;

public class Serialise_Tests
{

	[Fact]
	public void Returns_Json()
	{
		// Arrange
		var value = Rnd.Str;
		var key = Rnd.ByteF.Get(32);
		var box = new Locked<string>(value, key);
		var json = string.Format("{{\"encryptedContents\":\"{0}\",\"salt\":\"{1}\",\"nonce\":\"{2}\"}}",
			Convert.ToBase64String(box.EncryptedContents.Unsafe().Unwrap()),
			Convert.ToBase64String(box.Salt),
			Convert.ToBase64String(box.Nonce)
		);

		// Act
		var result = box.Serialise();

		// Assert
		result.AssertOk(json);
	}
}
