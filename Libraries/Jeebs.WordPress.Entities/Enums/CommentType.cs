using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Jeebs.WordPress.Enums
{
	/// <summary>
	/// CommentType enumeration
	/// </summary>
	public sealed class CommentType : Enum
	{
		/// <summary>
		/// Create new value
		/// </summary>
		/// <param name="name">Value name</param>
		public CommentType(in string name) : base(name) { }

		#region Default Comment Types

		/// <summary>
		/// Blank comment
		/// </summary>
		public static readonly CommentType Blank = new CommentType(string.Empty);

		/// <summary>
		/// Pingback comment
		/// </summary>
		public static readonly CommentType Pingback = new CommentType("pingback");

		#endregion

		/// <summary>
		/// Parse CommentType value name
		/// </summary>
		/// <param name="name">Value name</param>
		/// <returns>CommentType object</returns>
		public static CommentType Parse(in string name) => Parse(name, values: new[] { Blank, Pingback });
	}
}
