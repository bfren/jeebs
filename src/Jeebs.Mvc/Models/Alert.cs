// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Mvc.Enums;

namespace Jeebs.Mvc.Models;

/// <summary>
/// User feedback alert
/// </summary>
/// <param name="Type">Alert type</param>
/// <param name="Text">Alert text</param>
public readonly record struct Alert(AlertType Type, string Text);
