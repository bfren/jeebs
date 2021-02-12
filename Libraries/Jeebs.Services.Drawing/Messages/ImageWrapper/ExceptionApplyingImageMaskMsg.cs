using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Jm;

namespace Jm.Services.Drawing.ImageWrapper
{
	/// <summary>
	/// Exception while applying image mask - see <see cref="Jeebs.Services.Drawing.ImageWrapper.ApplyMask(int, int, Func{Jeebs.Services.Drawing.Geometry.Size, Jeebs.Services.Drawing.Geometry.Rectangle, Jeebs.Services.Drawing.IImageWrapper})"/>
	/// </summary>
	public sealed class ExceptionApplyingImageMaskMsg : ExceptionMsg
	{
		/// <summary>
		/// Create message
		/// </summary>
		/// <param name="e">Exception</param>
		public ExceptionApplyingImageMaskMsg(Exception e) : base(e) { }
	}
}
