// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Functions;
using Jeebs.WordPress.Entities.Ids;

namespace Jeebs.WordPress.Functions.QueryAttachmentsF_Tests;

public class GetQuery_Tests
{
	[Fact]
	public void No_FileIds_Returns_None_With_NoFileIdsMsg()
	{
		// Arrange
		var schema = Substitute.For<IWpDbSchema>();
		var fileIds = ListF.Empty<WpPostId>();

		// Act
		var result = QueryAttachmentsF.GetQuery(schema, fileIds, Rnd.Str);

		// Assert
		_ = result.AssertFailure("No File IDs were provided.");
	}

	[Fact]
	public void Returns_Valid_Query()
	{
		// Arrange
		var prefix = Rnd.Str;
		var schema = new WpDbSchema(prefix);
		var i0 = Rnd.UInt64;
		var i1 = Rnd.UInt64;
		var fileIds = ListF.Create<WpPostId>(new() { Value = i0 }, new() { Value = i1 });
		var virtualUploadsUrl = Rnd.Str;
		var expected =
			"SELECT " +
				"`p`.`ID` AS 'Id', " +
				"`p`.`post_title` AS 'Title', " +
				"`p`.`post_excerpt` AS 'Description', " +
				"`p`.`guid` AS 'Url', " +
				"`pm`.`meta_value` AS 'UrlPath', " +
				$"CONCAT('{virtualUploadsUrl}/', `pm`.`meta_value`) AS 'Url' " +
			$"FROM `{schema.Posts}` AS `p` " +
				$"LEFT JOIN `{schema.PostsMeta}` AS `pm` ON `p`.`ID` = `pm`.`post_id` " +
			$"WHERE `p`.`ID` IN ({i0},{i1}) " +
				"AND `pm`.`meta_key` = '_wp_attached_file';"
		;

		// Act
		var result = QueryAttachmentsF.GetQuery(schema, fileIds, virtualUploadsUrl);

		// Assert
		var ok = result.AssertOk();
		Assert.Equal(expected, ok);
	}
}
