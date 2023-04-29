// Jeebs Test Applications
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using AppApi;
using Jeebs.Cqrs;
using MaybeF;
using Microsoft.AspNetCore.Mvc;

var (app, log) = Jeebs.Apps.Web.ApiApp.CreateMinimal(args,
	(_, services) => services.AddCqrs()
);

app.MapGet("/", () => "Hello, world!");
app.MapGet("/hello/{name}", HandleSayHello);

app.Run();

static async Task<IResult> HandleSayHello(
	[FromServices] IDispatcher query,
	[FromRoute] string name
)
{
	var text = await query
		.SendAsync(
			new SayHelloQuery(name)
		)
		.UnwrapAsync(
			x => x.Value(ifNone: "Nothing to say.")
		)
		.ConfigureAwait(false);

	return Results.Text(text);
}
