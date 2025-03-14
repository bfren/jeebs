﻿// Jeebs Test Applications
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Messages;
using RndF;

namespace AppConsole.Messages;

public record Basic : Msg;

public record WithGeneric<T> : Msg;

public record FormattedMsg : Msg
{
	public override string Format =>
		"Values {One} and {Two}";

	public override object[]? Args =>
		[Rnd.Int, Rnd.Guid];
}

public record WithValue(string Value) : WithValueMsg<string>;

public record WithException(Exception Value) : ExceptionMsg;
