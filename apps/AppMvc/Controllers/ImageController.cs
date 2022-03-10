// Jeebs Test Applications
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Services.Drawing;
using MaybeF.Linq;
using Microsoft.AspNetCore.Mvc;

namespace AppMvc.Controllers;

public class ImageController : Controller
{
	private readonly IImageDriver image;

	private readonly string path = @"Q:\Personal\Images\Photos\DSC_5425.jpg";

	public ImageController(IImageDriver image) =>
		this.image = image;

	public IActionResult Image(int width, int height)
	{
		var r = from i in image.FromFile(path)
				from m in i.ApplyMask(width, height)
				select m.ToPngByteArray();

		return r.Switch<IActionResult>(
			some: x => File(x, "image/jpg"),
			none: () => NotFound()
		);
	}
}
