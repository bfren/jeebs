﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Jeebs
{
	/// <inheritdoc cref="ILink"/>
	public partial class Link : ILink
	{
		private readonly IR result;

		internal Link(IR result)
			=> this.result = result;
	}
}
