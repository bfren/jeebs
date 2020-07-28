using System;
using System.Collections.Generic;
using System.Text;
using Jeebs.WordPress.Entities;

namespace Jm.WordPress.Query.Wrapper.Posts
{
	public class RequiredContentPropertyNotFound<TModel> : WithStringMsg
	{
		public RequiredContentPropertyNotFound() : base(typeof(TModel).GetType().FullName) { }

		public override string ToString()
			=> $"Cannot find the {nameof(WpPostEntity.Content)} property of {Value}.";
	}
}
