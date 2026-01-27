// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

namespace Jeebs.WordPress.Entities.PostAttachment_Tests;

public class Info_Tests
{
	public static readonly string SamplePhp = "a:5:{s:5:\"width\";i:770;s:6:\"height\";i:513;s:4:\"file\";s:48:\"2013/07/colorado-river-photo_6383564-770tall.jpg\";s:5:\"sizes\";a:2:{s:9:\"thumbnail\";a:4:{s:4:\"file\";s:48:\"colorado-river-photo_6383564-770tall-150x150.jpg\";s:5:\"width\";i:150;s:6:\"height\";i:150;s:9:\"mime-type\";s:10:\"image/jpeg\";}s:6:\"medium\";a:4:{s:4:\"file\";s:48:\"colorado-river-photo_6383564-770tall-300x199.jpg\";s:5:\"width\";i:300;s:6:\"height\";i:199;s:9:\"mime-type\";s:10:\"image/jpeg\";}}s:10:\"image_meta\";a:10:{s:8:\"aperture\";i:0;s:6:\"credit\";s:0:\"\";s:6:\"camera\";s:0:\"\";s:7:\"caption\";s:0:\"\";s:17:\"created_timestamp\";i:0;s:9:\"copyright\";s:0:\"\";s:12:\"focal_length\";i:0;s:3:\"iso\";i:0;s:13:\"shutter_speed\";i:0;s:5:\"title\";s:0:\"\";}}";

	[Fact]
	public void Returns_Php_Serialised_Object_As_Json()
	{
		// Arrange
		var attachment = new Test { Info = SamplePhp };
		var expected =
			"{" +
				"\"width\":770," +
				"\"height\":513," +
				"\"file\":\"2013/07/colorado-river-photo_6383564-770tall.jpg\"," +
				"\"sizes\":{" +
					"\"thumbnail\":{" +
						"\"file\":\"colorado-river-photo_6383564-770tall-150x150.jpg\"," +
						"\"width\":150," +
						"\"height\":150," +
						"\"mime-type\":\"image/jpeg\"" +
					"}," +
					"\"medium\":{" +
						"\"file\":\"colorado-river-photo_6383564-770tall-300x199.jpg\"," +
						"\"width\":300," +
						"\"height\":199," +
						"\"mime-type\":\"image/jpeg\"" +
					"}" +
				"}," +
				"\"image_meta\":{" +
					"\"aperture\":0," +
					"\"credit\":\"\"," +
					"\"camera\":\"\"," +
					"\"caption\":\"\"," +
					"\"created_timestamp\":0," +
					"\"copyright\":\"\"," +
					"\"focal_length\":0," +
					"\"iso\":0," +
					"\"shutter_speed\":0," +
					"\"title\":\"\"" +
				"}" +
			"}";

		// Act
		var result = attachment.Info;

		// Assert
		Assert.Equal(expected, result);
	}

	public sealed record class Test : PostAttachment;
}
