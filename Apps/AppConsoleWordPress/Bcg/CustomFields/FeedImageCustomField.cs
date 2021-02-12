using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Jeebs.WordPress;

namespace AppConsoleWordPress.Bcg
{
	/// <summary>
	/// The place a sermon was first preached
	/// </summary>
	public sealed class FeedImageCustomField : AttachmentCustomField
	{
		/// <summary>
		/// This is not a required field
		/// </summary>
		public FeedImageCustomField() : base("image") { }
	}
}
