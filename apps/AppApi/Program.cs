// Jeebs Test Applications
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using AppApi;
using Jeebs.Cqrs;
using Microsoft.AspNetCore.Mvc;
using Wrap.Extensions;

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
			ifFail: _ => "Nothing to say."
		);

	return Results.Text(text);
}
