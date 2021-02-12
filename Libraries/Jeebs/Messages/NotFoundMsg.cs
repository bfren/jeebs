using System;
using System.Collections.Generic;
using System.Text;
using Jeebs;

namespace Jm
{
	/// <summary>
	/// Resouce cannot be found message
	/// </summary>
	public abstract class NotFoundMsg : IMsg
	{
		private readonly string? details;

		/// <summary>
		/// Create message
		/// </summary>
		protected NotFoundMsg() { }

		/// <summary>
		/// Create message with details of what has not been found
		/// </summary>
		/// <param name="details"></param>
		protected NotFoundMsg(string details) =>
			this.details = details;

		/// <summary>
		/// Return details of what has not been found, or class type
		/// </summary>
		public override string ToString() =>
			details switch
			{
				string x =>
					$"Not found: {x}",

				_ =>
					GetType().ToString()
			};
	}
}
