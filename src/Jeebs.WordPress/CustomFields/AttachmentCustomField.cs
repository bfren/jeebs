// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;
using System.Threading.Tasks;
using Jeebs.Data;
using Jeebs.Messages;
using Jeebs.WordPress.Entities;
using Jeebs.WordPress.Entities.StrongIds;
using Jeebs.WordPress.Enums;
using Jeebs.WordPress.Query;

namespace Jeebs.WordPress.CustomFields;

/// <summary>
/// Post Attachment Custom Field
/// </summary>
public abstract class AttachmentCustomField : CustomField<AttachmentCustomField.Attachment>
{
	/// <summary>
	/// IQueryPosts
	/// </summary>
	protected IQueryPosts QueryPosts { get; private init; }

	/// <inheritdoc cref="CustomField{T}.CustomField(string, T)"/>
	protected AttachmentCustomField(string key) : this(new Posts(), key) { }

	/// <summary>
	/// Create object from posts
	/// </summary>
	/// <param name="queryPosts">IQueryPosts</param>
	/// <param name="key">Meta key (for post_meta table)</param>
	protected AttachmentCustomField(IQueryPosts queryPosts, string key) : base(key, new Attachment()) =>
		QueryPosts = queryPosts;

	/// <inheritdoc/>
	public override Task<Maybe<bool>> HydrateAsync(IWpDb db, IUnitOfWork w, MetaDictionary meta, bool isRequired)
	{
		// First, get the Attachment Post ID from the meta dictionary
		// If meta doesn't contain the key and this is a required field, return failure
		// Otherwise return success
		if (meta.TryGetValue(Key, out var value) && !string.IsNullOrWhiteSpace(value))
		{
			ValueStr = value;
		}
		else
		{
			if (isRequired)
			{
				return F.None<bool>(new M.MetaKeyNotFoundMsg(GetType(), Key)).AsTask;
			}

			return F.False.AsTask;
		}

		// If we're here we have an Attachment Post ID, so get it and hydrate the custom field
		return
			F.Some(
				ValueStr
			)
			.Bind(
				x => ParseAttachmentPostId(GetType(), x)
			)
			.BindAsync(
				x => QueryPosts.ExecuteAsync<Attachment>(db, w, opt => opt with
				{
					Id = x,
					Type = PostType.Attachment,
					Status = PostStatus.Inherit,
					Maximum = 1
				})
			)
			.UnwrapAsync(
				x => x.SingleValue<Attachment>(
					tooMany: () => new M.MultipleAttachmentsFoundMsg(ValueStr)
				)
			)
			.MapAsync(
				x =>
				{
					ValueObj = x;

					if (ValueObj.Meta.TryGetValue(Constants.Attachment, out var urlPath))
					{
						ValueObj = ValueObj with { UrlPath = urlPath };
					}

					if (ValueObj.Meta.TryGetValue(Constants.AttachmentMetadata, out var info))
					{
						ValueObj = ValueObj with { Info = info };
					}

					return true;
				},
				F.DefaultHandler
			);
	}

	/// <summary>
	/// Parse the Attachment Post ID
	/// </summary>
	/// <param name="type">AttachmentCustomField type</param>
	/// <param name="value">Post ID value</param>
	internal static Maybe<WpPostId> ParseAttachmentPostId(Type type, string value) =>
		F.ParseInt64(value).Switch(
			some: x => F.Some(new WpPostId { Value = x }),
			none: _ => F.None<WpPostId>(new M.ValueIsInvalidPostIdMsg(type, value))
		);

	/// <inheritdoc/>
	protected override string GetValueAsString() =>
		ValueObj.Title;

	internal string GetValueAsStringTest() =>
		GetValueAsString();

	/// <summary>
	/// Attachment file
	/// </summary>
	public sealed record class Attachment : PostAttachment { }

	/// <summary>Messages</summary>
	public static class M
	{
		/// <summary>Meta key not found in MetaDictionary</summary>
		/// <param name="Type">Custom Field type</param>
		/// <param name="Value">Meta Key</param>
		public sealed record class MetaKeyNotFoundMsg(Type Type, string Value) : WithValueMsg<string>;

		/// <summary>Multiple matching attachments were found (should always be 1)</summary>
		/// <param name="Value">Attachment (Post) ID</param>
		public sealed record class MultipleAttachmentsFoundMsg(string Value) : WithValueMsg<string>;

		/// <summary>The value in the meta dictionary is not a valid ID</summary>
		/// <param name="Type">Custom Field type</param>
		/// <param name="Value">Meta Key</param>
		public sealed record class ValueIsInvalidPostIdMsg(Type Type, string Value) : WithValueMsg<string>;
	}
}
