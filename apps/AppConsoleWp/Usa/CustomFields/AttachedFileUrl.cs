// Jeebs Test Applications
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.WordPress;
using Jeebs.WordPress.CustomFields;

namespace AppConsoleWp.Usa;

/// <summary>
/// URL of attached file.
/// </summary>
public sealed class AttachedFileUrl() : TextCustomField(Constants.Attachment);
