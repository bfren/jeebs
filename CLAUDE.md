# Jeebs v9

.NET 10 libraries (`src/`), their tests (`tests/`) and sample apps (`apps/`), all in `Jeebs (v9).slnx`.
Packages are versioned centrally in `Directory.Packages.props`; the release version is in `Version`.

## Before handing back C# changes

Run both, exactly as CI does, and fix anything they report:

```bash
dotnet test --solution "Jeebs (v9).slnx" --configuration Test
dotnet format style "Jeebs (v9).slnx" --verify-no-changes --include <changed .cs files>
```

CI fails when `dotnet format style` has to change anything (e.g. an unused `using`, IDE0005, or a
lambda that could be a method group, IDE0200). `dotnet build` and `dotnet test` don't report these.

## Line endings

C# and project files are CRLF with a final newline (`.gitattributes`, `.editorconfig`); C# is
indented with tabs. Prefer editor tools over `sed` / shell appends, which have left files LF or
mixed. Check with `git ls-files --eol | grep -E "w/(mixed|lf)"`.

## Branches and commits

- Each piece of work has a GitHub issue; its branch is `<issue>-<short-slug>`, cut from `origin/main`.
- Commit messages say what changed, starting with an -ing verb, and end with ` - #<issue>`,
  e.g. `Fixing SQL Server paging without a sort - #606`.

## Tests

- One test project per `src` project, named `Tests.<Project>` (each `src` project exposes its
  internals to that name). Register it in the `.slnx` under `/tests/` with
  `<Build Solution="Release|*" Project="false" />`.
- Test files mirror the source: types under `_/<Type>/`, extension methods under
  `Extensions/<TypeExtensions>/`. One class per method, `<Method>_Tests`, in namespace
  `<SourceNamespace>.<Type>_Tests`.
- xUnit v3, NSubstitute, `Rnd` for random values, `Wrap.Testing` (`AssertOk`, `AssertFailure`).
  Use `// Arrange`, `// Act`, `// Assert`. Pass `TestContext.Current.CancellationToken` (or
  `Arg.Any<CancellationToken>()` in NSubstitute checks) where a method takes a token (xUnit1051).

## Code

- File headers: `// Jeebs Rapid Application Development` (`src`), `// Jeebs Unit Tests` (`tests`)
  or `// Jeebs Test Applications` (`apps`), then `// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013`.
- Code returns `Result<T>` / `Maybe<T>` from the Wrap package; failures are
  `R.Fail("Message {Value}.", value).Ctx(nameof(Type), nameof(Method))`.
- Data clients share one query builder in `src/Jeebs.Data/DbClient.CustomQueries.cs`; each client
  overrides only what differs (`RandomSort`, `GetLimitAndOffset`, `QueryTerminator`).
- Writes through a unit of work must call `Commit` / `CommitAsync`: disposing it rolls back.
- Logging for a database driver lives in its own `src/Jeebs.Logging.Serilog.<Driver>` project as an
  `ILoggingConnector`, switched on by referencing that package (see `.PostgreSql`, `.Sqlite`).
  The exception is MySQL, whose logger has to be set on its `MySqlDataSource` in `MySqlDbClient`.
