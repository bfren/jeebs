using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jeebs.Mvc.Models
{
	/// <summary>
	/// User feedback alert
	/// </summary>
	/// <param name="Type">Alert type</param>
	/// <param name="Text">Alert text</param>
	public sealed record Alert(AlertType Type, string Text);
}
