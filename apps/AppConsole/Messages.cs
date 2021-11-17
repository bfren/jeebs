// Jeebs Test Applications
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs;

namespace AppConsole.Messages;

public record BasicMsg : Msg;

public record WithGenericMsg<T> : Msg;

public record FormattedMsg : Msg
{
	public override string Format =>
		"Values {One} and {Two}";

	public override object[]? Args =>
		new object[] { F.Rnd.Int, F.Rnd.Guid };
}

public record WithValue(string Value) : WithValueMsg<string>;
