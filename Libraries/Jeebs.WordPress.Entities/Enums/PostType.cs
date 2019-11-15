using System.Collections.Generic;
using System.Linq;

namespace Jeebs.WordPress.Enums
{
	/// <summary>
	/// PostType enumeration
	/// </summary>
	public sealed class PostType : Enum
	{
		/// <summary>
		/// Create new value
		/// </summary>
		/// <param name="name">Value name</param>
		public PostType(in string name) : base(name) { }

		#region Default Post Types

		/// <summary>
		/// Post
		/// </summary>
		public static readonly PostType Post = new PostType("post");

		/// <summary>
		/// Page
		/// </summary>
		public static readonly PostType Page = new PostType("page");

		/// <summary>
		/// Revision
		/// </summary>
		public static readonly PostType Revision = new PostType("revision");

		/// <summary>
		/// Attachment
		/// </summary>
		public static readonly PostType Attachment = new PostType("attachment");

		/// <summary>
		/// Menu Item
		/// </summary>
		public static readonly PostType MenuItem = new PostType("nav_menu_item");

		/// <summary>
		/// Advanced Custom Field
		/// </summary>
		public static readonly PostType AdvancedCustomField = new PostType("acf");

		#endregion

		/// <summary>
		/// List of all post types
		/// Must be public static so it is thread safe
		/// </summary>
		private static readonly HashSet<PostType> all;

		/// <summary>
		/// Populate set of post types
		/// </summary>
		static PostType() => all = new HashSet<PostType>(new[] { Post, Page, Revision, Attachment, MenuItem, AdvancedCustomField });

		/// <summary>
		/// Add a custom post type
		/// </summary>
		/// <param name="type">PostType to add</param>
		/// <returns>False if the post type already exists</returns>
		public static bool AddCustomPostType(in PostType type) => all.Add(type);

		/// <summary>
		/// Parse PostType value name
		/// </summary>
		/// <param name="name">Value name</param>
		/// <returns>PostType object</returns>
		public static PostType Parse(in string name) => Parse(name, all.ToArray());
	}
}
