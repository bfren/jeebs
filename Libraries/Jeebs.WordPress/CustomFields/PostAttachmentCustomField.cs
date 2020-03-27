using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Jeebs.Data;
using Jeebs.WordPress.Enums;

namespace Jeebs.WordPress
{
	/// <summary>
	/// Post Attachment Custom Field
	/// </summary>
	public abstract partial class PostAttachmentCustomField : CustomField<PostAttachmentCustomField.Attachment>
	{
		/// <summary>
		/// Attachment value
		/// </summary>
		public override Attachment ValueObj { get; protected set; }

		/// <summary>
		/// Setup object
		/// </summary>
		/// <param name="key">Meta key</param>
		/// <param name="isRequired">[Optional] Whether or not this custom field is required (default: false)</param>
		protected PostAttachmentCustomField(string key, bool isRequired = false) : base(key, isRequired) => ValueObj = new Attachment();

		/// <summary>
		/// Hydrate this Field
		/// </summary>
		/// <param name="db">IWpDb</param>
		/// <param name="unitOfWork">IUnitOfWork</param>
		/// <param name="meta">MetaDictionary</param>
		public override async Task<Result> Hydrate(IWpDb db, IUnitOfWork unitOfWork, MetaDictionary meta)
		{
			// If meta doesn't contain the key and this is a required field, return failure
			// Otherwise return success
			if (!meta.ContainsKey(Key))
			{
				if (IsRequired)
				{
					return Result.Failure($"Key not found in meta dictionary: '{Key}'.");
				}

				return Result.Success();
			}

			ValueStr = meta[Key];

			// Get meta value as post ID
			if (!long.TryParse(ValueStr, out var postId))
			{
				return Result.Failure($"'{ValueStr}' is not a valid Post ID.");
			}

			// Create new query
			using var q = db.Query;

			// Get matching posts
			var exec = q.QueryPosts<Attachment>(opt =>
			{
				opt.Id = postId;
				opt.Type = PostType.Attachment;
				opt.Status = PostStatus.Inherit;
				opt.Limit = 1;
			});

			var result = await exec.Retrieve();

			// Check result
			if (result.Err is ErrorList)
			{
				return Result.Failure(result.Err);
			}

			var attachments = result.Val;

			// Add meta
			var metaResult = await q.AddMetaAndCustomFieldsToPosts(attachments, a => a.Meta);
			if (metaResult.Err is ErrorList)
			{
				return Result.Failure(metaResult.Err);
			}

			// Get attachment (there should be only one)
			ValueObj = attachments.Single();

			if (ValueObj.Meta.TryGetValue(Constants.AttachedFile, out var urlPath))
			{
				ValueObj.UrlPath = urlPath;
			}

			if (ValueObj.Meta.TryGetValue(Constants.AttachmentMetadata, out var info))
			{
				ValueObj.Info = info;
			}

			// Return success
			return Result.Success();
		}

		/// <summary>
		/// Return URL Path
		/// </summary>
		public override string ToString() => ValueObj?.UrlPath ?? (ValueStr ?? Key);
	}
}
