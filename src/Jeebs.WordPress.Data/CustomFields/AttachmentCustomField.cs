﻿// Jeebs Rapid Application Development
// Copyright (c) bfren.uk - licensed under https://mit.bfren.uk/2013

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using F.WordPressF.DataF;
using Jeebs.WordPress.Data.Entities;
using Jeebs.WordPress.Data.Enums;
using static F.OptionF;

namespace Jeebs.WordPress.Data
{
	/// <summary>
	/// Post Attachment Custom Field
	/// </summary>
	public abstract class AttachmentCustomField : CustomField<AttachmentCustomField.Attachment>
	{
		/// <inheritdoc/>
		protected AttachmentCustomField(string key) : base(key, new Attachment()) { }

		/// <inheritdoc/>
		public override Task<Option<bool>> HydrateAsync(IWpDb db, MetaDictionary meta, bool isRequired)
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
					return None<bool>(new Msg.MetaKeyNotFoundMsg(GetType(), Key)).AsTask;
				}

				return False.AsTask;
			}

			// If we're here we have an Attachment Post ID, so get it and hydrate the custom field
			return
				Return(
					ValueStr
				)
				.Bind(
					x => ParseAttachmentPostId(GetType(), x)
				)
				.BindAsync(
					x => GetAttachment(db, x)
				)
				.UnwrapAsync(
					x => x.Single<Attachment>(tooMany: () => new Msg.MultipleAttachmentsFoundMsg(ValueStr))
				)
				.MapAsync(
					x =>
					{
						ValueObj = x;

						if (ValueObj.Meta.TryGetValue(Constants.Attachment, out var urlPath))
						{
							ValueObj.UrlPath = urlPath;
						}

						if (ValueObj.Meta.TryGetValue(Constants.AttachmentMetadata, out var info))
						{
							ValueObj.Info = info;
						}

						return true;
					},
					DefaultHandler
				);
		}

		/// <summary>
		/// Parse the Attachment Post ID
		/// </summary>
		/// <param name="type">AttachmentCustomField type</param>
		/// <param name="value">Post ID value</param>
		internal static Option<WpPostId> ParseAttachmentPostId(Type type, string value)
		{
			if (!long.TryParse(value, out var attachmentPostId))
			{
				return None<WpPostId>(new Msg.ValueIsInvalidPostIdMsg(type, value));
			}

			return new WpPostId(attachmentPostId);
		}

		/// <summary>
		/// Get the Attachment by Post ID
		/// </summary>
		/// <param name="db">IWpDb</param>
		/// <param name="attachmentPostId">Post ID</param>
		internal static Task<Option<IEnumerable<Attachment>>> GetAttachment(IWpDb db, WpPostId attachmentPostId) =>
			QueryPostsF.ExecuteAsync<Attachment>(db, opt => opt with
			{
				Id = attachmentPostId,
				Type = PostType.Attachment,
				Status = PostStatus.Inherit,
				Maximum = 1
			});

		/// <summary>
		/// Return Attachment Title
		/// </summary>
		protected override string GetValueAsString() =>
			ValueObj.Title;

		/// <summary>
		/// Attachment class
		/// </summary>
		public sealed record Attachment : WpPostAttachmentEntity { }

		/// <summary>Messages</summary>
		public static class Msg
		{
			/// <summary>Meta key not found in MetaDictionary</summary>
			/// <param name="Type">Custom Field type</param>
			/// <param name="Value">Meta Key</param>
			public sealed record MetaKeyNotFoundMsg(Type Type, string Value) : WithValueMsg<string> { }

			/// <summary>Multiple matching attachments were found (should always be 1)</summary>
			/// <param name="Value">Attachment (Post) ID</param>
			public sealed record MultipleAttachmentsFoundMsg(string Value) : WithValueMsg<string> { }

			/// <summary>The value in the meta dictionary is not a valid ID</summary>
			/// <param name="Type">Custom Field type</param>
			/// <param name="Value">Meta Key</param>
			public sealed record ValueIsInvalidPostIdMsg(Type Type, string Value) : WithValueMsg<string> { }
		}
	}
}
