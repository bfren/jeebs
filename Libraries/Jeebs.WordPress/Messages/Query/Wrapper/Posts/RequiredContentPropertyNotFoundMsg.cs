using System;
using System.Collections.Generic;
using System.Text;
using Jeebs.WordPress.Entities;

namespace Jm.WordPress.Query.Wrapper.Posts
{
	/// <summary>
	/// Required Content property not found when querying posts
	/// </summary>
	/// <typeparam name="TModel">Post Model</typeparam>
	public class RequiredContentPropertyNotFoundMsg<TModel> : WithValueMsg<string>
	{
		/// <summary>
		/// Create message
		/// </summary>
		public RequiredContentPropertyNotFoundMsg() : base(typeof(TModel).GetType().FullName) { }

		/// <summary>
		/// Return error message
		/// </summary>
		public override string ToString()
			=> $"Cannot find the {nameof(WpPostEntity.Content)} property of {Value}.";
	}
}
