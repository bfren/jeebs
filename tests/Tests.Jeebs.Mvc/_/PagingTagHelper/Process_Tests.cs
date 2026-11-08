// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Collections;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Jeebs.Mvc.TagHelpers.PagingTagHelper_Tests;

public class Process_Tests
{
	[Fact]
	public void Encodes_Url_And_Query()
	{
		// Arrange
		var helper = new PagingTagHelper
		{
			Values = new PagingValues(100, 1, 10, 5),
			Url = "/search\"><b>",
			Query = "\"><script>alert(1)</script>&x=1"
		};
		var context = new TagHelperContext([], new Dictionary<object, object>(), Rnd.Str);
		var output = new TagHelperOutput("paging", [], (_, _) => Task.FromResult<TagHelperContent>(new DefaultTagHelperContent()));

		// Act
		helper.Process(context, output);

		// Assert
		var html = output.Content.GetContent();
		Assert.DoesNotContain("<script>", html);
		Assert.DoesNotContain("<b>", html);
		Assert.Contains("href=\"/search&quot;&gt;&lt;b&gt;?p=1&amp;q=%22%3E%3Cscript%3Ealert%281%29%3C%2Fscript%3E%26x%3D1\"", html);
	}
}
