// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

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
			GetType().Name;
	}
}
