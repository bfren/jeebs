# Audit fix plan

From the audit of 2026-11-07 (v9.4.4). Numbers match the audit findings.
Work top to bottom: each phase is one branch / issue, and each ends with:

```bash
dotnet test --solution "Jeebs (v9).slnx" --configuration Test
```

Mark items `[x]` as they land.

## Status

| Phase | Issue | Merged |
|---|---|---|
| 1 — one-line bugs | #602 | #603 |
| 2 — string encryption | #602 | #603 |
| 3 — transactions | #604 | #605 |
| 4 — query builders + paging | #606 | #607 |
| 5 — concurrency and crash risks | #610 | #611 |
| 6 — auth hardening | #608 | #609 |
| 7 — small cleanups | #612 | not yet |
| 8 — web app pipeline (second audit) | #624 | not yet |
| 9 — encoding and secrets (second audit) | — | — |
| 10 — PHP and WordPress data (second audit) | — | — |
| 11 — log levels and small fixes (second audit) | — | — |

### Still open

First audit: nothing. Every item is done, or marked won't do below.

Second audit (2026-11-07, v10.0.0-beta.1): phases 8–11, items B1–B12. The solution is now
`Jeebs (v10).slnx`, so those phases end with:

```bash
dotnet test --solution "Jeebs (v10).slnx" --configuration Test
```

Won't do: **#12** reads without a transaction (speed only), and rate limiting on the sign-in route.

### Breaking changes for the major version release notes

- **Unit of work:** dispose rolls back; apps that call `StartWork` / `StartWorkAsync` themselves
  must call `Commit` / `CommitAsync`. `Commit` / `CommitAsync` now return `Result<bool>`.
- **Query builder:** custom `DbClient` subclasses must implement `RandomSort` and
  `GetLimitAndOffset(ulong? maximum, ulong skip, bool sorted)`.
- **`SortOrderExtensions.ToOperator`** moved to the `Jeebs.Data` namespace.
- **`ThreadF.FireAndForget(Action)`** removed; use `FireAndForget(Func<Task>)` or the
  `onError` overload.
- **`Menu.LoadUri`** delegate now returns `ValueTask<string>` and has no `StringBuilder` parameter.
- **Encryption:** string-key `Locked` boxes made before phase 6 can't be opened (new Argon2
  key derivation). Byte-key boxes are unaffected.
- **Passwords:** new hashes use Argon2 Interactive; old hashes still verify and are re-hashed
  on the next sign-in.
- **Rqlite:** `ExecuteAsync` returns `false` when no rows change (including DDL).
- **SQLite / Rqlite:** count queries use `COUNT(*)`; SQL Server paging without a sort adds
  `ORDER BY (SELECT NULL)`.
- **MySQL:** `MySqlDbClient` (data and auth) constructors take an `ILoggerFactory`, which DI
  supplies in Jeebs apps. The `Jeebs.Logging.Serilog.MySql` package is gone: MySqlConnector
  logs through the app's `ILoggerFactory` (Serilog in Jeebs apps) instead, with no package to
  add. It shows warnings only unless `jeebs:logging:overrides` sets `"MySqlConnector"` to
  another level (e.g. `"Debug"`).
- **Web apps:** `MvcApp` / `ApiApp` / `RazorApp` now get request logging, site verification,
  the exception handler, status-code pages and HSTS, which they silently lacked. Production
  errors in MVC and Razor apps now go to `/Error` (the `ErrorController`), and error views are
  sent with their real status code instead of 200.

---

## Phase 1 — one-line bugs — issue #602

- [x] **#3 PostgreSQL random sort** — `src/Jeebs.Data.Clients.PostgreSql/PostgreSqlDbClient.Queries.Custom.cs:107`
  change `RAND()` to `random()`. Fix the expected SQL in
  `tests/Tests.Jeebs.Data.Clients.PostgreSql/_/PostgreSqlDbClient/GetQuery_Tests.cs:344`.
- [x] **#5 Image save corrupts files** — `src/Jeebs.Services.Drivers.Drawing.Skia/ImageWrapper.cs:34`
  `File.OpenWrite(path)` → `File.Create(path)`.
- [x] **#4 Blank password locks user out**
  - `src/Jeebs.Auth.Data/AuthDataProvider.cs` `ChangeUserPasswordAsync`: fail first when
    `string.IsNullOrWhiteSpace(model.NewPassword)`.
  - `src/Jeebs.Auth.Data/AuthUserRepository.cs` `CreateAsync`: same check on `plainTextPassword`.
  - Tests: `tests/Tests.Jeebs.Auth.Data` (new project; still needs full coverage of Auth.Data).

## Phase 2 — string encryption round trip (#2) — issue #602

- [x] `src/Jeebs.Cryptography/Functions/CryptoF.Lock.cs` `CheckContents`: delete the
  `string str => str` branch so strings are JSON-serialised like everything else.
- No `Unlock` fallback: no app stores raw-string boxes from v9.0.0–v9.4.x.
- [x] Test: `tests/Tests.Jeebs.Cryptography/Extensions/StringExtensions/Decrypt_Tests4.cs`
  round-trips a string with byte and string keys. Existing fixture tests still pass.

## Phase 3 — transactions (#1) — issue #604

Behaviour change — part of the major version (see breaking changes above).

- [x] `src/Jeebs.Data.Common/UnitOfWork.cs`: dispose rolls back anything not committed;
  `Commit` / `CommitAsync` return `Result<bool>` (failure carries the exception);
  `CommitAsync<T>(T value)` returns `value` on success.
- [x] Writes commit on success with `.BindAsync(w.CommitAsync)`: `Db.ExecuteAsync.cs`,
  `Db.InsertAsync.cs`, `Repository.cs` (Create / Update / Delete).
- [x] `src/Jeebs.WordPress/WpDbQuery.cs` — reads only, no change needed.
- [x] Tests in `tests/Tests.Jeebs.Data.Common`: dispose rolls back / doesn't after commit;
  commit failure returns the exception; Db and Repository writes commit, skip commit on
  failure, and report commit failure.
- Note in the release: apps that call `StartWork` themselves must now call `Commit`.

**#12 won't do** (reads without a transaction): `IAdapter` takes an `IDbTransaction` and the
Dapper adapter gets its connection from `transaction.Connection`, so this needs adapter
methods that take an `IDbConnection` instead (14 files use `IAdapter`). Speed only, not
correctness: reads now roll back on dispose, which is harmless.

## Phase 4 — query builder merge + paging (#13, #11) — issue #606

- [x] **#13** Both `GetQuery` builders (predicates and `IQueryParts`) now live in
  `src/Jeebs.Data/DbClient.CustomQueries.cs`. Each client overrides only `RandomSort`,
  `GetLimitAndOffset` and (SQL Server, Rqlite) `QueryTerminator`. Net -344 lines.
  SQLite / Rqlite count queries now use `COUNT(*)` (same as SQLite's `COUNT()`).
- [x] **#11** `GetLimitAndOffset` writes the offset whenever `Skip > 0`, even with no
  `Maximum`. Test added to each client's `GetQuery_Tests.cs`, and to the new
  `tests/Tests.Jeebs.Data.Clients.Rqlite` project.
- [x] **Review fix:** `GetCountQuery` copied `Sort`, `Maximum` and `Skip` into the count, so a
  count with a skip returned no rows, and a sorted count failed on PostgreSQL / SQL Server.
  It now clears sort and paging. Count test added to every client.
- [x] **New:** SQL Server only allows `OFFSET … FETCH` after an `ORDER BY`, so paging with no
  sort failed there. `GetLimitAndOffset` now gets a `sorted` flag and SQL Server adds
  `ORDER BY (SELECT NULL)` when there's an offset and no sort.
- [x] **Leftover:** the five identical per-client `SortOrderExtensions.ToOperator` are replaced
  by one in `Jeebs.Data` (`src/Jeebs.Data/Extensions/SortOrder/`), used by the base builder.
  **Breaking:** callers must now import `Jeebs.Data` instead of `Jeebs.Data.Clients.*`.
- [x] **Flaky tests** (`MimeType` `Parse_Tests`, WordPress `Taxonomy` `AddCustomTaxonomy_Tests`):
  `MimeType`, `PostType` and `Taxonomy` shared an unlocked static `HashSet` that tests in parallel
  added to and read at once. `Enumerated` now has locked `Add` / `Snapshot` helpers used by all three.
  Also fixed: `Enumerated.Parse` cached failures, so a name parsed before it was registered as a
  custom type stayed unknown. Tests: concurrent add/parse, and register-after-failed-parse.
  Done on branch 618-add-sqlite-query-connection-logging.

## Phase 5 — concurrency and crash risks (#6, #7, #10) — issue #610

- [x] **#7** `ThreadF.FireAndForget` takes `Func<Task>` (runs with `Task.Run`, so it is never
  `async void`), plus an overload with an `onError` handler. Sign-in and the webhook driver pass
  handlers that log. The `Action` overload is removed (**breaking**). Tests in
  `tests/Tests.Jeebs/Functions/_/ThreadF/`.
- [x] `FireAndForget<T>(T state, Action<T> task)` deleted (nothing called it, and an async lambda
  passed to it would have been `async void`).
- [x] **#6** `Menu.LoadUri` returns its line (**breaking** delegate change); `LoadUrisAsync` uses
  `Parallel.ForAsync` (max 3) writing to an array slot per URI, so lines are kept in order.
  Timeouts (`TaskCanceledException` when the load itself wasn't cancelled) report "failed".
- [x] **#10** `RqliteDb.ExecuteAsync`: success = `RowsAffected > 0` only. Tests in
  `tests/Tests.Jeebs.Data.Clients.Rqlite/_/RqliteDb/`.
- [x] Confirmed against a real rqlite (2026-11-07): `apps/AppConsoleRq`'s "Updating deleted data"
  step got `"last_insert_id":5` with no rows affected, and `ExecuteAsync` returned `false`
  (the old check would have returned `true`). Note: DDL such as `CREATE TABLE` now reports
  `false` too, as it already did in the other clients.

## Phase 6 — auth hardening (#9, #8, #14) — issue #608, breaking

- [x] **#9** `HashPassword` uses `StrengthArgon.Interactive` (one shared `PasswordStrength`).
  `ValidateUserAsync` rehashes on sign-in when `PasswordNeedsRehash()` (wraps
  `ArgonPasswordNeedsRehash`), so no database column or migration is needed. A failed rehash
  update doesn't stop the sign-in.
- Rate limiting on the sign-in route: won't do.
- [x] **#14** Unknown emails verify against a dummy hash made with the current settings
  (created once on first use); disabled users verify their real hash before failing.
- [x] **#8** String keys use `ArgonHashBinary` (Interactive) with a 16-byte random salt.
  **Breaking, no version field:** boxes locked with a string key before this can't be opened.
  Byte-key boxes are unchanged. `Decrypt_Tests1` fixture regenerated.
- Tests: `ValidateUserAsync_Tests` (rehash, failed rehash, wrong password, disabled, not found),
  `PasswordNeedsRehash_Tests`, 16-byte salt check in `Locked` `Constructor_Tests`.

## Phase 7 — small cleanups — issue #612

- [x] **#15** `ImageWrapper.cs`: the image is decoded once into a `Lazy<SKImage>` field shared by
  `Width`, `Height`, `Save`, `ToByteArray` and `ApplyMask`, and disposed with the wrapper.
  No test project for the Skia driver; checked with a scratch program (load, size, mask,
  PNG bytes, save over a bigger file, dispose).
- [x] **#16** `JwtF.ValidateToken` catches `SecurityTokenExpiredException`; the existing
  expired-token test still passes.
- [x] **CS0618** MySqlConnector's global `MySqlConnectorLogManager.Provider` is obsolete and has
  no global replacement. `MySqlDbClient` now takes an `ILoggerFactory` and creates connections
  from a `MySqlDataSource` built with `UseLoggerFactory`, one per connection string (shared,
  because clients are transient). `Jeebs.Logging.Serilog.MySql` and the unused
  `MySqlConnector.Logging.Serilog` package are deleted. The build has no warnings now.
  Test: `GetConnection_Tests`. Confirmed against a real MySQL server (2026-11-07).

---

---

# Second audit — 2026-11-07 (v10.0.0-beta.1)

Covers the folders the first audit skipped: `Apps.Web`, `Mvc`, `Mvc.Auth`, `Logging.Serilog`,
`Services`, webhook/Slack, the WordPress query builder and attachments, and spot checks of core
`Jeebs`. Items are numbered B1–B12 so they don't clash with the first audit's numbers.
Must fix: B1–B5. Should fix: B6–B9. Nice to have: B10–B12.

## Phase 8 — web app pipeline (B1, B5, B6, B9) — issue #624

- [x] **B1 MvcApp skips the base setup** — `src/Jeebs.Apps.Web/MvcApp.cs:262`
  `MvcApp.Configure` never calls `base.Configure` (lost in `7c79715a`, the .NET 6 move). So
  `MvcApp` / `ApiApp` / `RazorApp` get no request logging, site verification, exception handler,
  status-code pages (`ErrorController` is never reached) or HSTS.
  Done: the start of `WebApp.Configure` (logging, verification, exception handling, security
  headers) moved into a protected `ConfigureStart`, which `MvcApp.Configure` now calls first.
  `WebApp.Configure` keeps its auth step for plain `WebApp`s; `MvcApp` still runs `ConfigureAuth`
  once, after `UseRouting`.
- [x] **B5 Request logging swallows exceptions** — `src/Jeebs.Apps.Web/Middleware/LoggerMiddleware.cs:71`
  It logs a 500 and doesn't rethrow, so a crash in `ApiApp` (no exception handler) returns an
  empty 200. Done: `throw;` after the log line.
- [x] **B6 Error pages are sent as HTTP 200** —
  `src/Jeebs.Mvc/Extensions/MvcController/MvcControllerExtensions.ExecuteErrorAsync.cs:43`
  Done: `@this.Response.StatusCode = status;` before `return @this.View(...)`.
- [x] **B9 Test:** new `Tests.Jeebs.Apps.Web` with `MvcApp_Tests.Configure_Tests` (a
  Production `MvcApp` on Kestrel: an endpoint that throws returns 500 with the `/Error` body) and
  `LoggerMiddleware_Tests.InvokeAsync_Tests` (rethrows). Both fail without the B1 / B5 fixes.

## Phase 9 — encoding and secrets (B2, B4, B10) — issue #626

- [x] **B2 XSS in paging links** — `src/Jeebs.Mvc/TagHelpers/PagingTagHelper.cs:80`
  `Query` (the search text) goes into the HTML unencoded via `AppendHtml`. Fix:
  `Uri.EscapeDataString(Query)`, and HTML-encode `Url`. Add a test with `"><script>`.
  Done (`QueryPrefix` is escaped too); test in `tests/Tests.Jeebs.Mvc/_/PagingTagHelper/`.
- [x] **B4 Webhook errors log the secret Slack URL** — `src/Jeebs.Services.Webhook/WebhookDriver.cs:91`
  `{@Request}` / `{@Response}` expand the `RequestUri`, which is the webhook secret. Fix: log only
  `(int)response.StatusCode` and the exception; `using` on request and response.
  Done. No test: there is no `Tests.Jeebs.Services.Webhook` project yet.
- [ ] **B10 Email tag turns accents into `?`** — `src/Jeebs/Extensions/String/StringExtensions.ToASCII.cs`
  `Encoding.ASCII` replaces non-ASCII (`José` → `Jos?`). Fix: loop over chars, write `&#{(int)c};`.

## Phase 10 — PHP and WordPress data (B3, B8, B12)

- [ ] **B3 PHP strings cut at the first `;`** — `src/Jeebs/Functions/Php/PhpF.Deserialise.cs:88`
  `getString` ignores the stored length, so `Tom &amp; Jerry` in attachment metadata reads
  wrongly, shifts every later value, or throws from `PostAttachment.Info`. Fix: read length n and
  take n UTF-8 bytes (PHP counts bytes). Tests: a string with `;`, one with `é`; also correct the
  existing `s:15:"Deserialise_Tests"` case (it is 17 long).
- [ ] **B8 PHP numbers use the server culture** — `src/Jeebs/Functions/Php/PhpF.Deserialise.cs:37`
  `double.Parse` / `long.Parse` with no culture: on a German server `d:1.5;` reads as 15. Fix:
  pass `CultureInfo.InvariantCulture`.
- [ ] **B12 WordPress filters miss posts** — `src/Jeebs.WordPress/Query/PostsPartsBuilder.cs:227,290`
  Custom fields: `= 1` drops posts with two matching rows for one key; use `>= 1`. Taxonomies:
  a repeated term ID never matches `= Count`; use `.Distinct()` on the IDs.

## Phase 11 — log levels and small fixes (B7, B11)

- [ ] **B7 `IsEnabled` casts between mismatched enums** —
  `src/Jeebs.Logging.Serilog/SerilogLogger.cs:57`, `src/Jeebs/Logging/Log.cs:96`
  Wrap `LogLevel` is 1, 2, 4, 8, 16, 32; Serilog is 0–5. With minimum Information,
  `IsEnabled(Debug)` is true and `IsEnabled(Warning)` is always true; the Microsoft `LogLevel`
  overload is off the same way. Fix: use `LevelF.ConvertToSerilogLevel`, and a switch for the
  Microsoft levels. One test.
- [ ] **B11 `robots.txt` on one line** — `src/Jeebs.Mvc/Controllers/SimpleController.cs:32`
  Fix: `"User-agent: *\nAllow: /"`.

Left out (small): the blocking `.GetAwaiter().GetResult()` in `MvcController.Process`, and the
app name added to every log message template.

## Not audited yet

Most of WordPress (beyond the query builder and attachments), most of core `Jeebs` (collections,
string helpers), `Calendar`, `Cqrs`, `Config`, `Apps` (non-web), sample apps. The second audit
ran no tests and started no app, so B1, B5 and B6 come from reading the code.
