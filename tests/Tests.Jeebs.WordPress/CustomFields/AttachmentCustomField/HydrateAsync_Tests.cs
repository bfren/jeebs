// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Data;
using Jeebs.WordPress.Query;
using static Jeebs.WordPress.CustomFields.AttachmentCustomField.M;
using Attachment = Jeebs.WordPress.CustomFields.AttachmentCustomField.Attachment;

namespace Jeebs.WordPress.CustomFields.AttachmentCustomField_Tests;

public class HydrateAsync_Tests
{
	[Fact]
	public async Task Meta_Does_Not_Contain_Key_IsRequired_True_Returns_None_With_MetaKeyNotFoundMsg()
	{
		// Arrange
		var db = Substitute.For<IWpDb>();
		var unitOfWork = Substitute.For<IUnitOfWork>();
		var meta = new MetaDictionary { { Rnd.Str, Rnd.Str } };
		var key = Rnd.Str;
		var field = new TestCustomField(Substitute.For<IQueryPosts>(), key);

		// Act
		var result = await field.HydrateAsync(db, unitOfWork, meta, true);

		// Assert
		var none = result.AssertNone().AssertType<MetaKeyNotFoundMsg>();
		Assert.Equal(typeof(TestCustomField), none.Type);
		Assert.Equal(key, none.Value);
	}

	[Fact]
	public async Task Meta_Does_Not_Contain_Key_IsRequired_False_Returns_False_Option()
	{
		// Arrange
		var db = Substitute.For<IWpDb>();
		var unitOfWork = Substitute.For<IUnitOfWork>();
		var meta = new MetaDictionary { { Rnd.Str, Rnd.Str } };
		var field = new TestCustomField(Substitute.For<IQueryPosts>(), Rnd.Str);

		// Act
		var result = await field.HydrateAsync(db, unitOfWork, meta, false);

		// Assert
		result.AssertFalse();
	}

	[Fact]
	public async Task Meta_Contains_Key_Multiple_Terms_Found_Returns_None_With_MultipleTermsFoundMsg()
	{
		// Arrange
		var a0 = new Attachment();
		var a1 = new Attachment();
		var attachments = new[] { a0, a1 };

		var db = Substitute.For<IWpDb>();
		var unitOfWork = Substitute.For<IUnitOfWork>();

		var key = Rnd.Str;
		var value = Rnd.Lng;
		var meta = new MetaDictionary { { key, value.ToString() } };

		var queryPosts = Substitute.For<IQueryPosts>();
		queryPosts.ExecuteAsync<Attachment>(db, unitOfWork, Arg.Any<GetPostsOptions>()).Returns(attachments);

		var field = new TestCustomField(queryPosts, key);

		// Act
		var result = await field.HydrateAsync(db, unitOfWork, meta, true);

		// Assert
		var none = result.AssertNone().AssertType<MultipleAttachmentsFoundMsg>();
		Assert.Equal(value.ToString(), none.Value);
	}

	[Fact]
	public async Task Meta_Contains_Key_Single_Item_Found_Sets_ValueObj_Returns_True()
	{
		// Arrange
		var attachment = new Attachment();
		var attachments = new[] { attachment };

		var db = Substitute.For<IWpDb>();
		var unitOfWork = Substitute.For<IUnitOfWork>();

		var key = Rnd.Str;
		var meta = new MetaDictionary { { key, Rnd.Lng.ToString() } };

		var queryPosts = Substitute.For<IQueryPosts>();
		queryPosts.ExecuteAsync<Attachment>(db, unitOfWork, Arg.Any<GetPostsOptions>()).Returns(attachments);

		var field = new TestCustomField(queryPosts, key);

		// Act
		var result = await field.HydrateAsync(db, unitOfWork, meta, true);

		// Assert
		result.AssertTrue();
		Assert.Same(attachment, field.ValueObj);
	}

	[Fact]
	public async Task Meta_Contains_Key_Single_Item_Found_Sets_ValueObj_Properties_Returns_True()
	{
		// Arrange
		var attachment = new Attachment();
		var urlPath = Rnd.Str;
		attachment.Meta.Add(Constants.Attachment, urlPath);
		var info = Rnd.Str;
		attachment.Meta.Add(Constants.AttachmentMetadata, "s:6:\"" + info + "\";");
		var attachments = new[] { attachment };

		var db = Substitute.For<IWpDb>();
		var unitOfWork = Substitute.For<IUnitOfWork>();

		var key = Rnd.Str;
		var meta = new MetaDictionary { { key, Rnd.Lng.ToString() } };

		var queryPosts = Substitute.For<IQueryPosts>();
		queryPosts.ExecuteAsync<Attachment>(db, unitOfWork, Arg.Any<GetPostsOptions>()).Returns(attachments);

		var field = new TestCustomField(queryPosts, key);

		// Act
		var result = await field.HydrateAsync(db, unitOfWork, meta, true);

		// Assert
		result.AssertTrue();
		Assert.Equal(urlPath, field.ValueObj.UrlPath);
		Assert.Contains(info, field.ValueObj.Info);
	}

	public class TestCustomField : AttachmentCustomField
	{
		public TestCustomField(IQueryPosts queryPosts, string key) : base(queryPosts, key) { }
	}
}
