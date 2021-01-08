using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Jeebs;

namespace Jm.Services.Drawing.ImageWrapper
{
	/// <summary>
	/// Mask Height or Width not defined - see <see cref="Jeebs.Services.Drawing.ImageWrapper.ApplyMask(int, int, Func{Jeebs.Services.Drawing.Structs.Size, Jeebs.Services.Drawing.Structs.Rectangle, Jeebs.Services.Drawing.IImageWrapper})"/>
	/// </summary>
	public sealed class MaskHeightOrWidthRequiredMsg : IMsg { }
}
