using Xunit;

namespace Jeebs.Config.VerificationsConfig_Tests
{
	public class Google_Tests
	{
		[Fact]
		public void Code_Not_Set_Returns_Null()
		{
			// Arrange
			var config = new VerificationConfig();

			// Act
			var result = config.Google;

			// Assert
			Assert.Null(result);
		}

		[Fact]
		public void Returns_Google_Html()
		{
			// Arrange
			var google = F.Rnd.Str;
			var config = new VerificationConfig { Google = google };

			// Act
			var result = config.Google;

			// Assert
			Assert.Equal($"google{google}.html", result);
		}
	}
}
