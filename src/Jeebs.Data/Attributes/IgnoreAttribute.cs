﻿// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;

namespace Jeebs.Data.Attributes;

/// <summary>
/// Mark property as to be ignored when mapping entity properties to table columns
/// </summary>
[AttributeUsage(AttributeTargets.Property | AttributeTargets.Parameter, Inherited = true, AllowMultiple = false)]
public sealed class IgnoreAttribute : Attribute { }
