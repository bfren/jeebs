// Copyright (c) bcg|design.
// Licensed under https://mit.bcgdesign.com/2013.

using System.Threading.Tasks;
using Jeebs.Auth;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Jeebs.Mvc.Auth
{
	/// <summary>
	/// JSON Web Token TagHelper
	/// </summary>
	[HtmlTargetElement("jwt", TagStructure = TagStructure.WithoutEndTag)]
	public class JwtTagHelper : TagHelper
	{
		/// <summary>
		/// ViewContext object
		/// </summary>
		[ViewContext]
		public ViewContext? ViewContext { get; set; }

		private IJwtAuthProvider Provider { get; init; }

		/// <summary>
		/// Inject dependencies
		/// </summary>
		/// <param name="provider">IJwtAuthenticationProvider</param>
		public JwtTagHelper(IJwtAuthProvider provider) =>
			Provider = provider;

		/// <summary>
		/// Output a JSON web token for the current user
		/// </summary>
		/// <param name="context">TagHelperContext</param>
		/// <param name="output">TagHelperOutput</param>
		public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
		{
			// ViewContext is required or we can't get the current user
			if (ViewContext != null && Provider.CreateToken(ViewContext.HttpContext.User) is Some<string> token)
			{
				output.TagName = "input";
				output.TagMode = TagMode.SelfClosing;
				output.Attributes.Add("type", "hidden");
				output.Attributes.Add("id", "token");
				output.Attributes.Add("name", "token");
				output.Attributes.Add("value", token.Value);
			}
			else
			{
				output.SuppressOutput();
			}
		}
	}
}
