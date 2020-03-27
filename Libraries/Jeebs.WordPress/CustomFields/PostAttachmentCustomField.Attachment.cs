using System;
using System.Collections.Generic;
using System.Text;
using Jeebs.Data;
using Jeebs.Util;

namespace Jeebs.WordPress
{
	/// <summary>
	/// Post Attachment Custom Field
	/// </summary>
	public abstract partial class PostAttachmentCustomField : CustomField<PostAttachmentCustomField.Attachment>
	{
		/// <summary>
		/// Attachment class
		/// </summary>
		public sealed class Attachment : IEntity
		{
			/// <summary>
			/// Id
			/// </summary>
			public long Id { get => PostId; set => PostId = value; }

			/// <summary>
			/// PostId
			/// </summary>
			public long PostId { get; set; }

			/// <summary>
			/// Title
			/// </summary>
			public string Title { get; set; }

			/// <summary>
			/// MimeType
			/// </summary>
			public MimeType MimeType { get; set; }

			/// <summary>
			/// Meta
			/// </summary>
			public MetaDictionary Meta { get; set; }

			/// <summary>
			/// UrlPath
			/// </summary>
			public string UrlPath { get; set; }

			/// <summary>
			/// Info
			/// </summary>
			public string Info
			{
				get => Json.Serialise(new PhpSerialiser().Deserialise(info));
				set => info = value;
			}
			private string info = string.Empty;

			/// <summary>
			/// Setup object
			/// </summary>
			public Attachment()
			{
				Title = string.Empty;
				MimeType = MimeType.Blank;
				Meta = new MetaDictionary();
				UrlPath = string.Empty;
			}
		}
	}
}
