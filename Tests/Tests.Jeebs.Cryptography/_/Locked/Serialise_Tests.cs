// Jeebs Unit Tests
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System;
using Xunit;

namespace Jeebs.Cryptography.Locked_Tests
{
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
			Assert.Equal(F.JsonF.Empty, result);
		}

		[Fact]
		public void Returns_Json()
		{
			// Arrange
			var value = F.Rnd.Str;
			var key = F.CryptoF.GenerateKey();
			var box = new Locked<string>(value, key);
			var json = string.Format("{{\"encryptedContents\":\"{0}\",\"salt\":\"{1}\",\"nonce\":\"{2}\"}}",
				Convert.ToBase64String(box.EncryptedContents!),
				Convert.ToBase64String(box.Salt),
				Convert.ToBase64String(box.Nonce)
			);

			// Act
			var result = box.Serialise();

			// Assert
			Assert.Equal(json, result);
		}
	}
}
