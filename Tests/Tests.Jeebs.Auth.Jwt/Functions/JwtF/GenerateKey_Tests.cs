// Jeebs Unit Tests
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System.Linq;
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
			Assert.False(result.All(c => StringF.LowercaseChars.Contains(c)));
			Assert.False(result.All(c => StringF.UppercaseChars.Contains(c)));
			Assert.False(result.All(c => StringF.NumberChars.Contains(c)));
			Assert.False(result.All(c => StringF.SpecialChars.Contains(c)));
			Assert.True(result.All(c => StringF.AllChars.Contains(c)));
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
			Assert.False(result.All(c => StringF.LowercaseChars.Contains(c)));
			Assert.False(result.All(c => StringF.UppercaseChars.Contains(c)));
			Assert.False(result.All(c => StringF.NumberChars.Contains(c)));
			Assert.False(result.All(c => StringF.SpecialChars.Contains(c)));
			Assert.True(result.All(c => StringF.AllChars.Contains(c)));
		}
	}
}
