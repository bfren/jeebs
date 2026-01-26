// Jeebs Test Applications
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Services.Drawing;
using Microsoft.AspNetCore.Mvc;
using Wrap.Extensions;
using Wrap.Linq;

namespace AppMvc.Controllers;

public class ImageController(IImageDriver image) : Controller
{
	private readonly IImageDriver image = image;

	private readonly string path = @"Q:\Personal\Images\Photos\DSC_5425.jpg";

	public IActionResult Image(int width, int height)
	{
		var r = from i in image.FromFile(path)
				from m in i.ApplyMask(width, height)
				from a in m.ToPngByteArray()
				select a;

		return r.Match(
			ok: x => File(x, "image/jpg") as IActionResult,
			fail: _ => NotFound()
		);
	}
}
