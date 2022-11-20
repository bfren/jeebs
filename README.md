# Jeebs

![GitHub release (latest SemVer including pre-releases)](https://img.shields.io/github/v/release/bfren/jeebs?include_prereleases&label=Version) ![Nuget](https://img.shields.io/nuget/dt/Jeebs?label=Downloads) ![GitHub](https://img.shields.io/github/license/bfren/jeebs?label=Licence)

[![Test](https://github.com/bfren/jeebs/actions/workflows/test.yml/badge.svg)](https://github.com/bfren/jeebs/actions/workflows/test.yml) ![Publish](https://github.com/bfren/jeebs/workflows/Publish/badge.svg)

[![Codacy Badge](https://app.codacy.com/project/badge/Grade/a21b1f1909dd44fbbdea712cddc76266)](https://www.codacy.com/gh/bfren/jeebs/dashboard)

Libraries for use in .NET Core projects for rapid application development.  The most basic ASP.NET app - with full Jeebs library support - begins like this:

```csharp
Jeebs.Apps.Web.WebApp.Run(args);
```

Please [view the book](https://docs.bfren.dev/jeebs) for information on how to use these libraries.

## History

I am definitely a backend developer at heart, although like everyone I have to write front-facing things sometimes!  However I am happiest when I'm writing code that makes frontend development easier - and more beautiful.

The code in these libraries has been under active development and use for over a decade, powering all my own websites, and some for other people as well.

Jeebs v5 came from a) rewriting the entire codebase to make use of improvements in .NET 5.0, and C# 8 &amp; 9, not least to null handling, and b) a COVID lockdown project of learning to write in F#.  I thought for a while I might completely switch, but I decided I would prefer to bring some of the things I loved about F# into my C#.

Jeebs v6 brought a full rewrite of the WordPress library to take advantage of the new (improved for v6) Data libraries, a new Calendar library, some nifty new functions, many (many) more unit tests.

Jeebs v7 took advantage of new features and optimisations in .NET 6, including support for the new minimal API, file-scoped namespaces, record structs, sealed `ToString()` methods, a new SkiaSharp driver (as System.Drawing was deprecated).

Jeebs v8 targets .NET 7, applies StyleCop conventions and best practices, and removes two utility projects (`Maybe` and `Random`) to separate repos / packages (see [here](https://github.com/bfren/maybe) and [here](https://github.com/bfren/rnd)).

## License

> [MIT](https://mit.bfren.dev/2013)

## Copyright

> Copyright (c) 2013-2022 [bfren](https://bfren.dev)
> Unless otherwise stated
