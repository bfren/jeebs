module Jeebs.NotifyF.Slack.Notifier

open System
open System.Net.Http
open System.Text
open Jeebs.NotifyF
open Jeebs.Util



type private SlackAttachment =
    { Text : string
      Color : string }



type private SlackMessage =
    { Username : string
      Attachments : SlackAttachment list }



let private getColour kind =
    match kind with
    | OK -> "good"
    | Warning -> "warning"
    | Error -> "danger"



let private getMessage app (message : Message) =
    { Username = app
      Attachments =
          [ { Text = message.Text
              Color = getColour message.Kind } ] }



let internal send app (factory : IHttpClientFactory) uri text =
    async {
        use client = factory.CreateClient()
        let json = getMessage app text |> Json.Serialise
        use content = new StringContent(json, Encoding.UTF8, "application/json")
        client.PostAsync(Uri(uri), content) |> Async.AwaitTask |> ignore
    }
