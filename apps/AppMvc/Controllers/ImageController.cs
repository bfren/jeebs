// Jeebs Test Applications
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Services.Drawing;
using Microsoft.AspNetCore.Mvc;
using Wrap.Extensions;

namespace AppMvc.Controllers;

public class ImageController(IImageDriver image, IConfiguration config) : Controller
{
	// Width and height come from the query string, so cap them before allocating an image
	private const int MaxSize = 4000;

	private readonly IImageDriver image = image;

	private readonly IConfiguration config = config;

	public IActionResult Image(int width, int height)
	{
		if (width is < 0 or > MaxSize || height is < 0 or > MaxSize)
		{
			return BadRequest();
		}

		if (config["ImagePath"] is not string path)
		{
			return NotFound();
		}

		// Dispose each image once the next step has used it
		var r = image.FromFile(path)
			.Bind(i => { using (i) { return i.ApplyMask(width, height); } })
			.Bind(m => { using (m) { return m.ToPngByteArray(); } });

		return r.Match(
			fOk: x => File(x, "image/png") as IActionResult,
			fFail: _ => NotFound()
		);
	}
}
