using System;
using System.Collections.Generic;
using System.Text;
using Jeebs;

namespace Jm
{
	/// <summary>
	/// Resouce cannot be found message
	/// </summary>
	public class NotFoundMsg : IMsg
	{
		private readonly string? details;

		public NotFoundMsg() { }

		public NotFoundMsg(string details)
			=> this.details = details;

		public override string ToString()
			=> details switch
			{
				string x => $"Not found: {x}",
				_ => base.ToString()
			};
	}
}
