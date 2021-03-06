// Copyright (c) bcg|design.
// Licensed under https://mit.bcgdesign.com/2013.

namespace Jm.WordPress.Query.Wrapper.Posts
{
	/// <summary>
	/// Required Custom Field not found when querying posts
	/// </summary>
	public class RequiredCustomFieldNotFoundMsg : WithValueMsg<(long postId, string propertyName, string metaKey)>
	{
		internal RequiredCustomFieldNotFoundMsg(long postId, string propertyName, string metaKey) : base((postId, propertyName, metaKey)) { }

		/// <summary>
		/// Return error message
		/// </summary>
		public override string ToString() =>
			$"Custom field {Value.propertyName} is required and is not set for Post {Value.postId} (meta key: {Value.metaKey}).";
	}
}
