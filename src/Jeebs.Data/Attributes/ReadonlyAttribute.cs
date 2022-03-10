// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;

namespace Jeebs.Data.Attributes;

/// <summary>
/// Mark property as Readonly
/// </summary>
[AttributeUsage(AttributeTargets.Property | AttributeTargets.Parameter, Inherited = true, AllowMultiple = false)]
public sealed class ReadonlyAttribute : Attribute { }
