// Jeebs Unit Tests
// Copyright (c) bfren.uk - licensed under https://mit.bfren.uk/2013

using Jeebs.Auth.Constants;
using Xunit;

namespace F.JwtF_Tests
{
	public class GenerateKey_Tests
	{
		[Fact]
		public void GenerateSigningKey_Returns_Key_Of_Signing_Key_Length()
		{
			// Arrange

			// Act
			var result = JwtF.GenerateSigningKey();

			// Assert
			Assert.Equal(JwtSecurity.SigningKeyBytes, result.Length);
		}

		[Fact]
		public void GenerateSigningKey_Returns_Key_With_All_Character_Classes()
		{
			// Arrange

			// Act
			var result = JwtF.GenerateSigningKey();

			// Assert
			Assert.False(result.All(c => Rnd.StringF.LowercaseChars.Contains(c)));
			Assert.False(result.All(c => Rnd.StringF.UppercaseChars.Contains(c)));
			Assert.False(result.All(c => Rnd.StringF.NumberChars.Contains(c)));
			Assert.False(result.All(c => Rnd.StringF.SpecialChars.Contains(c)));
			Assert.True(result.All(c => Rnd.StringF.AllChars.Contains(c)));
		}

		[Fact]
		public void GenerateEncryptingKey_Returns_Key_Of_Encrypting_Key_Length()
		{
			// Arrange

			// Act
			var result = JwtF.GenerateEncryptingKey();

			// Assert
			Assert.Equal(JwtSecurity.EncryptingKeyBytes, result.Length);
		}

		[Fact]
		public void GenerateEncryptingKey_Returns_Key_With_All_Character_Classes()
		{
			// Arrange

			// Act
			var result = JwtF.GenerateEncryptingKey();

			// Assert
			Assert.False(result.All(c => Rnd.StringF.LowercaseChars.Contains(c)));
			Assert.False(result.All(c => Rnd.StringF.UppercaseChars.Contains(c)));
			Assert.False(result.All(c => Rnd.StringF.NumberChars.Contains(c)));
			Assert.False(result.All(c => Rnd.StringF.SpecialChars.Contains(c)));
			Assert.True(result.All(c => Rnd.StringF.AllChars.Contains(c)));
		}
	}
}
