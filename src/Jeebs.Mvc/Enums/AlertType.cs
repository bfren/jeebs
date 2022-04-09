// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

namespace Jeebs.Mvc.Enums;

/// <summary>
/// Alert Types
/// </summary>
public enum AlertType
{
	/// <summary>
	/// Info
	/// </summary>
	Info = 1 << 0,

	/// <summary>
	/// Success
	/// </summary>
	Success = 1 << 1,

	/// <summary>
	/// Error
	/// </summary>
	Error = 1 << 2,

	/// <summary>
	/// Warning
	/// </summary>
	Warning = 1 << 3
}
