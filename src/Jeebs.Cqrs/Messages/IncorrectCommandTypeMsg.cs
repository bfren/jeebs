// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;
using Jeebs.Messages;

namespace Jeebs.Cqrs.Messages;

/// <summary>The command is an incorrect type for the handler</summary>
/// <param name="ExpectedType">Expected command type</param>
/// <param name="ActualType">Actual command type</param>
public sealed record class IncorrectCommandTypeMsg(Type ExpectedType, Type ActualType) : Msg;
