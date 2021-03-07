// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System;

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
