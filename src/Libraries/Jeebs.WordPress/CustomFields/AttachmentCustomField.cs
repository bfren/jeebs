// Jeebs Rapid Application Development
// Copyright (c) bfren.uk - licensed under https://mit.bfren.uk/2013

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Jeebs.WordPress.Data;
using Jeebs.WordPress.Enums;
using static F.OptionF;

namespace Jeebs.WordPress
{
	/// <summary>
	/// Post Attachment Custom Field
	/// </summary>
	public abstract class AttachmentCustomField : CustomField<AttachmentCustomField.Attachment>
	{
		/// <inheritdoc/>
		protected AttachmentCustomField(string key, bool isRequired = false) : base(key, new Attachment(), isRequired) { }

		/// <inheritdoc/>
		public override Task<Option<bool>> HydrateAsync(IWpDb db, IUnitOfWork unitOfWork, MetaDictionary meta)
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
				if (IsRequired)
				{
					return None<bool>(new Msg.MetaKeyNotFoundMsg(GetType(), Key)).AsTask;
				}

				return False.AsTask;
			}

			// If we're here we have an Attachment Post ID, so get it and hydrate the custom field
			return Return(ValueStr)
				.Bind(
					parseAttachmentPostId
				)
				.BindAsync(
					getAttachments
				)
				.UnwrapAsync(
					x => x.Single<Attachment>(tooMany: () => new Msg.MultipleAttachmentsFoundMsg(ValueStr))
				)
				.MapAsync(
					hydrate,
					DefaultHandler
				);

			//
			// Parse the Attachment Post ID
			//
			Option<long> parseAttachmentPostId(string value)
			{
				if (!long.TryParse(value, out var attachmentPostId))
				{
					return None<long>(new Msg.ValueIsInvalidPostIdMsg(GetType(), value));
				}

				return attachmentPostId;
			}

			//
			// Get the Attachment by Post ID
			//
			async Task<Option<List<Attachment>>> getAttachments(long attachmentPostId)
			{
				// Create new query
				using var w = db.GetQueryWrapper();

				// Get matching posts
				return await w.QueryPostsAsync<Attachment>(modify: opt =>
				{
					opt.Id = attachmentPostId;
					opt.Type = PostType.Attachment;
					opt.Status = PostStatus.Inherit;
					opt.Limit = 1;
				});
			}

			//
			// Hydrate the custom field using Attachment info
			//
			bool hydrate(Attachment attachment)
			{
				ValueObj = attachment;

				if (ValueObj.Meta.TryGetValue(Constants.AttachedFile, out var urlPath))
				{
					ValueObj.UrlPath = urlPath;
				}

				if (ValueObj.Meta.TryGetValue(Constants.AttachmentMetadata, out var info))
				{
					ValueObj.Info = info;
				}

				return true;
			}
		}

		/// <summary>
		/// Return term Title
		/// </summary>
		public override string ToString() =>
			ValueObj?.Title ?? base.ToString();

		/// <summary>
		/// Attachment class
		/// </summary>
		public sealed record Attachment : Entities.Attachment { }

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
