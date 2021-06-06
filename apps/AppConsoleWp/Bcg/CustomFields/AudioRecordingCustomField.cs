// Jeebs Test Applications
// Copyright (c) bfren.uk - licensed under https://mit.bfren.uk/2013

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
