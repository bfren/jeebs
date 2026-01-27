// Jeebs Test Applications
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.WordPress.CustomFields;

namespace AppConsoleWp.Bcg;

/// <summary>
/// Audio recording of sermon.
/// </summary>
public sealed class AudioRecordingCustomField() : FileCustomField("audio");
