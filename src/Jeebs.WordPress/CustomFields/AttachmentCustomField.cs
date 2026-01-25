// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;
using System.Threading.Tasks;
using Jeebs.Data;
using Jeebs.WordPress.Entities;
using Jeebs.WordPress.Entities.Ids;
using Jeebs.WordPress.Enums;
using Jeebs.WordPress.Query;

namespace Jeebs.WordPress.CustomFields;

/// <summary>
/// Post Attachment Custom Field.
/// </summary>
public abstract class AttachmentCustomField : CustomField<AttachmentCustomField.Attachment>
{
	/// <summary>
	/// IQueryPosts.
	/// </summary>
	protected IQueryPosts QueryPosts { get; private init; }

	/// <inheritdoc cref="CustomField{T}.CustomField(string, T)"/>
	protected AttachmentCustomField(string key) : this(new Posts(), key) { }

	/// <summary>
	/// Create object from posts.
	/// </summary>
	/// <param name="queryPosts">IQueryPosts.</param>
	/// <param name="key">Meta key (for post_meta table).</param>
	protected AttachmentCustomField(IQueryPosts queryPosts, string key) : base(key, new Attachment()) =>
		QueryPosts = queryPosts;

	/// <inheritdoc/>
	public override Task<Result<bool>> HydrateAsync(IWpDb db, IUnitOfWork w, MetaDictionary meta, bool isRequired)
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
				return R.Fail("Meta Key '{Key}' not found for Custom Field '{Type}'.", Key, GetType())
					.Ctx(GetType().Name, nameof(HydrateAsync))
					.AsTask<bool>();
			}

			return R.False.AsTask();
		}

		// If we're here we have an Attachment Post ID, so get it and hydrate the custom field
		return
			R.Wrap(
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
			.GetSingleAsync(
				x => x.Value<Attachment>(),
				(msg, args) => R.Fail("Unable to get single '{ValueStr}': " + msg, [ValueStr, .. args])
					.Ctx(GetType().Name, nameof(HydrateAsync))
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
				}
			);
	}

	/// <summary>
	/// Parse the Attachment Post ID.
	/// </summary>
	/// <param name="type">AttachmentCustomField type.</param>
	/// <param name="value">Post ID value.</param>
	internal static Result<WpPostId> ParseAttachmentPostId(Type type, string value) =>
		M.ParseUInt64(value).Match(
			some: x => R.Wrap(new WpPostId { Value = x }),
			none: () => R.Fail("'{Value}' is not a valid Post ID.", value)
				.Ctx(type.Name, nameof(ParseAttachmentPostId))
		);

	/// <inheritdoc/>
	protected override string GetValueAsString() =>
		ValueObj.Title;

	internal string GetValueAsStringTest() =>
		GetValueAsString();

	/// <summary>
	/// Attachment file.
	/// </summary>
	public sealed record class Attachment : PostAttachment { }
}
