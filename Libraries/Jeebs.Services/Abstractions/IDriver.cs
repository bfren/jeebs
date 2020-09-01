﻿using System;
using System.Collections.Generic;
using System.Text;
using Jeebs.Config;

namespace Jeebs.Services
{
	/// <summary>
	/// Service driver
	/// </summary>
	/// <typeparam name="TConfig">Service configuration type</typeparam>
	public interface IDriver<TConfig> where TConfig : ServiceConfig { }
}
