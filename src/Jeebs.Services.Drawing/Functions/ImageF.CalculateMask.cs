// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.uk/2013

using System;
using Jeebs.Services.Drawing.Geometry;

namespace F
{
	/// <summary>
	/// Image functions
	/// </summary>
	public static partial class ImageF
	{
		/// <summary>
		/// <para>Get the dimensions to resize an image to fill a mask, without distorting the image</para>
		/// <para>Calculations come from:</para>
		/// <para>
		/// TimThumb script created by Ben Gillbanks, originally created by Tim McDaniels and Darren Hoyt (no longer maintained)
		/// https://code.google.com/archive/p/timthumb/source/default/source
		/// </para>
		/// <para>
		/// GNU General Public License, version 2
		/// http://www.gnu.org/licenses/old-licenses/gpl-2.0.html
		/// </para>		///
		/// </summary>
		/// <param name="imgWidth">Original image width</param>
		/// <param name="imgHeight">Original image height</param>
		/// <param name="maskWidth">Mask width</param>
		/// <param name="maskHeight">Mask height</param>
		/// <returns>Rectangle to grab the correct portion of the original image, to be resized</returns>
		public static Rectangle CalculateMask(double imgWidth, double imgHeight, double maskWidth, double maskHeight)
		{
			// Calculation variables
			double x = 0;
			double y = 0;
			double w = imgWidth;
			double h = imgHeight;
			double ratioX = imgWidth / maskWidth;
			double ratioY = imgHeight / maskHeight;

			// Calculate the widths / starting co-ordinates
			if (ratioX > ratioY)
			{
				w = Math.Round(imgWidth / ratioX * ratioY);
				x = Math.Round((imgWidth - (imgWidth / ratioX * ratioY)) / 2);
			}
			else if (ratioY > ratioX)
			{
				h = Math.Round(imgHeight / ratioY * ratioX);
				y = Math.Round((imgHeight - (imgHeight / ratioY * ratioX)) / 2);
			}

			// Return dimensions as a Rectangle object
			return new Rectangle((int)x, (int)y, (int)w, (int)h);
		}
	}
}
