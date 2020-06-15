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
	public abstract class AttachmentCustomField : CustomField<AttachmentCustomField.Attachment>
	{
		/// <inheritdoc/>
		protected AttachmentCustomField(string key, bool isRequired = false) : base(key, isRequired) => ValueObj = new Attachment();

		/// <inheritdoc/>
		public override async Task<IResult<bool>> HydrateAsync(IWpDb db, IUnitOfWork unitOfWork, MetaDictionary meta)
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
			using var w = db.GetQueryWrapper();

			// Get matching posts
			var result = await w.QueryPostsAsync<Attachment>(modify: opt =>
			{
				opt.Id = postId;
				opt.Type = PostType.Attachment;
				opt.Status = PostStatus.Inherit;
				opt.Limit = 1;
			}).ConfigureAwait(false);

			if (result.Err is IErrorList)
			{
				return Result.Failure(result.Err);
			}

			// Get attachment (there should be only one)
			ValueObj = result.Val.Single();

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
		/// Return term Title
		/// </summary>
		public override string ToString() => ValueObj?.Title ?? base.ToString();

		/// <summary>
		/// Attachment class
		/// </summary>
		public sealed class Attachment : Entities.Attachment { }
	}
}
