using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Jeebs;

namespace Jm.Services.Drawing.ImageWrapper
{
	/// <summary>
	/// Mask Height or Width not defined - see <see cref="Jeebs.Services.Drawing.ImageWrapper.ApplyMask(int, int, Func{Jeebs.Services.Drawing.Geometry.Size, Jeebs.Services.Drawing.Geometry.Rectangle, Jeebs.Services.Drawing.IImageWrapper})"/>
	/// </summary>
	public sealed class MaskHeightOrWidthRequiredMsg : IMsg { }
}
