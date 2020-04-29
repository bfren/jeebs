namespace Jeebs.NotifyF

open System.Net.Http



type Notifier private (app : string, factory : IHttpClientFactory, uri : string) =

    let app = app;

    let factory = factory

    let uri = uri

    let getMessage kind text =
        { Text = text ; Kind = kind }

    let send =
        Slack.Notifier.send app factory uri

    static member Instance app uri factory =
        Notifier(app, factory, uri)

    member this.Ok =
        getMessage OK >> send >> Async.StartAsTask

    member this.Warning =
        getMessage Warning >> send >> Async.StartAsTask

    member this.Error =
        getMessage Error >> send >> Async.StartAsTask
