// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

namespace Jeebs
{
	/// <summary>
	/// Represents a framework message
	/// </summary>
	public interface IMsg
	{
		/// <summary>
		/// Return the message type name
		/// </summary>
		string? ToString() =>
			GetType().FullName;
	}
}
