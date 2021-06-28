﻿// Jeebs Unit Tests
// Copyright (c) bfren.uk - licensed under https://mit.bfren.uk/2013

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Jeebs;
using Jeebs.WordPress.Data;
using Jeebs.WordPress.Data.Entities;
using NSubstitute;
using Xunit;
using static F.WordPressF.DataF.QueryAttachmentsF;
using static F.WordPressF.DataF.QueryAttachmentsF.Msg;

namespace F.WordPressF.DataF.QueryAttachmentsF_Tests
{
	public class GetQuery_Tests
	{
		[Fact]
		public void No_FileIds_Returns_None_With_NoFileIdsMsg()
		{
			// Arrange
			var schema = Substitute.For<IWpDbSchema>();
			var fileIds = ImmutableList.Empty<WpPostId>();

			// Act
			var result = GetQuery(schema, fileIds, Rnd.Str);

			// Assert
			var none = result.AssertNone();
			Assert.IsType<NoFileIdsMsg>(none);
		}

		[Fact]
		public void Returns_Valid_Query()
		{
			// Arrange
			var prefix = Rnd.Str;
			var schema = new WpDbSchema(prefix);
			var i0 = Rnd.Lng;
			var i1 = Rnd.Lng;
			var fileIds = ImmutableList.Create<WpPostId>(new(i0), new(i1));
			var virtualUploadsUrl = Rnd.Str;
			var expected =
				"SELECT " +
					"`p`.`post_title` AS 'Title', " +
					"`p`.`post_excerpt` AS 'Description', " +
					$"CONCAT('{virtualUploadsUrl.EndWith('/')}', `pm`.`meta_value`) AS 'Url' " +
				$"FROM `{schema.Post}` AS `p` " +
					$"LEFT JOIN `{schema.PostMeta}` AS `pm` ON `p`.`ID` = `pm`.`post_id` " +
				$"WHERE `p`.`ID` IN ({i0},{i1}) " +
					"AND `pm`.`meta_key` = '_wp_attached_file';"
			;

			// Act
			var result = GetQuery(schema, fileIds, virtualUploadsUrl);

			// Assert
			var some = result.AssertSome();
			Assert.Equal(expected, some);
		}
	}
}