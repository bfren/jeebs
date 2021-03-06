﻿// Copyright (c) bcg|design.
// Licensed under https://mit.bcgdesign.com/2013.

using System;

namespace Jeebs.Data.Mapping
{
	/// <summary>
	/// Mark property as Version
	/// </summary>
	[AttributeUsage(AttributeTargets.Property, Inherited = true, AllowMultiple = false)]
	public sealed class VersionAttribute : Attribute { }
}
