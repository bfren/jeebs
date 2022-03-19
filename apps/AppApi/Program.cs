// Jeebs Test Applications
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using AppApi;
using Jeebs.Cqrs;
using MaybeF;
using Microsoft.AspNetCore.Mvc;

var app = Jeebs.Apps.WebApps.WebApplication.Create(args,
	(_, services) => services.AddCqrs()
);

app.MapGet("/", () => "Hello, world!");
app.MapGet("/hello/{name}", HandleSayHello);

app.Run();

static async Task<IResult> HandleSayHello(
	[FromServices] IQueryDispatcher query,
	[FromRoute] string name
)
{
	var text = await query
		.DispatchAsync<SayHelloQuery, string>(
			new(name)
		)
		.UnwrapAsync(
			x => x.Value(ifNone: "Nothing to say.")
		)
		.ConfigureAwait(false);

	return Results.Text(text);
}
