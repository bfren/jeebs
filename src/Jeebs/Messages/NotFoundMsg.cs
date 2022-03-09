// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

namespace Jeebs.Messages;

/// <inheritdoc cref="INotFoundMsg"/>
public abstract record class NotFoundMsg<T> : WithValueMsg<T>, INotFoundMsg<T>;
