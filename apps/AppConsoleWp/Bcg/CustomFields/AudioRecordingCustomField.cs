// Jeebs Test Applications
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using Jeebs.WordPress.Data;

namespace AppConsoleWp.Bcg
{
	/// <summary>
	/// Audio recording of sermon
	/// </summary>
	public sealed record AudioRecordingCustomField : FileCustomField
	{
		/// <summary>
		/// This is not a required field
		/// </summary>
		public AudioRecordingCustomField() : base("audio") { }
	}
}
