// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Jeebs.Data;
using Jeebs.WordPress.Enums;
using Jm.WordPress.CustomFields;
using Jm.WordPress.CustomFields.Attachment;

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
		public override async Task<IR<bool>> HydrateAsync(IOk r, IWpDb db, IUnitOfWork unitOfWork, MetaDictionary meta)
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
					return r.Error<bool>().AddMsg(new MetaKeyNotFoundMsg(GetType(), Key));
				}

				return r.OkFalse();
			}

			// If we're here we have an Attachment Post ID, so get it and hydrate the custom field
			return await r
				.Link()
					.Catch().AllUnhandled().With<ParsePostIdExceptionMsg>()
					.Map(parseAttachmentPostId)
				.Link()
					.Catch().AllUnhandled().With<GetAttachmentExceptionMsg>()
					.MapAsync(getAttachment).Await()
				.Link()
					.Catch().AllUnhandled().With<HydrateExceptionMsg>()
					.MapAsync(hydrate);

			//
			// Parse the Attachment Post ID
			//
			IR<long> parseAttachmentPostId(IOk r)
			{
				if (!long.TryParse(ValueStr, out var attachmentPostId))
				{
					return r.Error<long>().AddMsg(new ValueIsInvalidPostIdMsg(GetType(), ValueStr));
				}

				return r.OkV(attachmentPostId);
			}

			//
			// Get the Attachment by Post ID
			//
			async Task<IR<Attachment>> getAttachment(IOkV<long> r)
			{
				// Create new query
				using var w = db.GetQueryWrapper();

				// Get matching posts
				var attachments = await w.QueryPostsAsync<Attachment>(r, modify: opt =>
				{
					opt.Id = r.Value;
					opt.Type = PostType.Attachment;
					opt.Status = PostStatus.Inherit;
					opt.Limit = 1;
				});

				// If there is more than one attachment, return an error
				return attachments switch
				{
					IOkV<List<Attachment>> x when x.Value.Count == 1 =>
						x.OkV(x.Value.Single()),

					{ } x =>
						x.Error<Attachment>().AddMsg().OfType<MultipleAttachmentsFoundMsg>()
				};
			}

			//
			// Hydrate the custom field using Attachment info
			//
			async Task<IR<bool>> hydrate(IOkV<Attachment> r)
			{
				ValueObj = r.Value;

				if (ValueObj.Meta.TryGetValue(Constants.AttachedFile, out var urlPath))
				{
					ValueObj.UrlPath = urlPath;
				}

				if (ValueObj.Meta.TryGetValue(Constants.AttachmentMetadata, out var info))
				{
					ValueObj.Info = info;
				}

				return r.OkTrue();
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
	}
}
