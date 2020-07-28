using System;
using System.Collections.Generic;
using System.Text;

namespace Jm.WordPress.Query.Wrapper.Posts
{
	public class RequiredCustomFieldNotFound : WithValueMsg<(long postId, string propertyName, string metaKey)>
	{
		public RequiredCustomFieldNotFound(long postId, string propertyName, string metaKey) : base((postId, propertyName, metaKey)) { }

		public override string ToString()
			=> $"Custom field {Value.propertyName} is required and is not set for Post {Value.postId} (meta key: {Value.metaKey}).";
	}
}
