using System;
using System.Collections.Generic;
using System.Text;
using Jeebs.WordPress;

namespace AppConsoleWordPress.Bcg
{
	/// <summary>
	/// Audio recording of sermon
	/// </summary>
	public sealed class AudioRecordingCustomField : AttachmentCustomField
	{
		/// <summary>
		/// This is not a required field
		/// </summary>
		public AudioRecordingCustomField() : base("audio") { }
	}
}
