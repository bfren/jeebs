// Jeebs Test Applications
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using Jeebs.WordPress;

namespace AppConsoleWordPress.Bcg
{
	/// <summary>
	/// Audio recording of sermon
	/// </summary>
	public sealed class AudioRecordingCustomField : FileCustomField
	{
		/// <summary>
		/// This is not a required field
		/// </summary>
		public AudioRecordingCustomField() : base("audio") { }
	}
}
