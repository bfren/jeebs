namespace Jeebs.NotifyF



type internal Kind = OK | Warning | Error



type internal Message =
    { Text : string
      Kind : Kind }
