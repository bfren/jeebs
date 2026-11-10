# Audit fix plan

Three audits so far. Phases 1–11 (first and second audits) are done; phases 12–15 (third audit)
are the plan below. Each phase is one issue / branch, one commit per item, and ends with:

```bash
dotnet test --solution "Jeebs (v10).slnx" --configuration Test
dotnet format style "Jeebs (v10).slnx" --verify-no-changes --include <changed .cs files>
```

Mark items `[x]` as they land.

## Status

| Phases | Audit | Items | Issues / PRs | Done |
|---|---|---|---|---|
| 1–7 | First audit, 2026-11-07 (v9.4.4) | #1–#16 | #602–#612 | yes |
| 8–11 | Second audit, 2026-11-07 (v10.0.0-beta.1) | B1–B12 | #624–#632 | yes |
| 12–15 | Third audit, 2026-11-08 (v10.0.0-beta.1) | C1–C14 | #634 (phase 12), #637 (phase 13) | phases 12–13 |

### Won't do

- **#12 reads without a transaction** (speed only). `IAdapter` takes an `IDbTransaction` and the
  Dapper adapter gets its connection from `transaction.Connection`, so this needs adapter methods
  that take an `IDbConnection` (14 files use `IAdapter`). Reads roll back on dispose, which is harmless.
- **Rate limiting on the sign-in route.**

### Not audited yet

Most of WordPress (beyond the query builder and attachments), `Services.Drawing` / Skia,
`Data.Testing` and the sample apps. None of the audits ran the tests or started an app before
reporting, so findings come from reading the code.

---

## Breaking changes for the major version release notes

- **Unit of work:** async only - `StartWork()`, `Commit()`, `Rollback()` and `IDisposable` are gone,
  and `StartWorkAsync()` is no longer on `IDb`. Use `db.DoWorkAsync(unitOfWork => ...)`: it commits
  if the work returns Ok (like v9's commit-on-dispose, except that a Failure now rolls back instead
  of committing), and rolls back if it throws. `CommitAsync` now
  returns `Result<bool>` (Ok again on a repeat commit, a failure after a rollback).
- **Queries in a unit of work** take the `IUnitOfWork` instead of an `IDbTransaction`
  (`db.ExecuteAsync(sql, param, type, unitOfWork)`, `repo.CreateAsync(entity, unitOfWork)`). Its transaction is only
  begun by the first write, so reads before any write run without one. `IAdapter` methods now take
  the connection as well as the transaction.
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
- **Rqlite:** `RqliteDb.StartWork()` is renamed `Connect()` - it only creates a client and never began a unit of work.
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
- **Controllers:** `MvcController.Process` is removed (it blocked a thread on failure); use
  `ProcessAsync`, which now has a `(Result<T>, Func<T, IActionResult>)` overload.
- **Logging:** `SerilogLogger.ConsoleMessagePrefix` is gone. With `addPrefix` the app name now
  appears in console output only, not in the message stored by Seq, files or database sinks.
- **`string.ToASCII()`** is renamed `ToHtmlEntities()`, and encodes every character (accents,
  emoji) instead of turning non-ASCII into `?`.
- **Fluent queries:** an empty `WhereId` / `WhereIn` list now matches no rows instead of being
  ignored, and `CountAsync` fails (like `QueryAsync`) on query errors or when there is no WHERE.
- **CRUD queries** bind the ID as `@Id` instead of writing it into the SQL, so Dapper apps that
  update or delete need the ID type handlers (`AddIdTypeHandlers`), as reading already did.
  `IDbClient.GetRetrieveQuery` / `GetUpdateQuery` / `GetDeleteQuery` no longer take an `id`.
- **`IDbClient.GetQuery<TEntity, TModel>((string, Compare, dynamic)[])`** and
  `DataF.ConvertPredicatesToColumns` are removed.
- **Auth:** `AuthController.SignOut` is POST-only with an anti-forgery token; sign-out links
  must become forms. A failed sign in now shows the form again with an error alert, keeping the email.

Add these when phases 14–15 land:

- **C6:** only if the replacement validator changes what the config schema accepts.

---

# Third audit — 2026-11-08 (v10.0.0-beta.1)

Items are numbered C1–C14 so they don't clash with the first audit (#1–#16) or the second (B1–B12).
Must fix: C1–C5. Should fix: C6–C11. Nice to have: C12–C14.

## Phase 12 — data query correctness (C1, C2, C5, C10, C11)

- [x] **C1 `CountAsync` ignores query errors** — `src/Jeebs.Data/Repository/FluentQuery.Count.cs:11`
  A bad column alias goes into `Errors`, which `QueryAsync` checks but `CountAsync` doesn't, so it
  runs `SELECT COUNT(*)` over the whole table. Fix: copy `QueryAsync`'s two guards (errors, no
  WHERE) to the top of `CountAsync`. Test: bad alias returns a failure. Done.
- [x] **C2 Empty IN list stops filtering** — `src/Jeebs.Data/Repository/FluentQuery.Where.cs:76-118`,
  `src/Jeebs.Data/Functions/DataF.GetWhereAndParameters.cs:73-92`
  `WhereId()` / `WhereIn(empty)` / `WhereNotIn(empty)` return `Unchanged()`, and an `In` whose
  value isn't a list is dropped, so other filters return all their rows instead of none. Fix: in
  `GetWhereAndParameters`, an empty list adds `1 = 0` for `In` and nothing for `NotIn`; remove the
  `values.Any()` / `ids.Length` short-cuts. Update `WhereIn_Tests` / `WhereNotIn_Tests` /
  `WhereId_Tests`, which assert the old behaviour. **Breaking.**
  Done, except `WhereNotIn(empty)` keeps its short-cut (NOT IN an empty list is every row, so
  skipping it is already right). A non-list `In` / `NotIn` value is now a list of one.
- [x] **C5 Guid IDs break CRUD** — `= {id}` in `src/Jeebs.Data.Clients.*/…Queries.Crud.cs` (15 places)
  The ID is pasted into the SQL, so a `GuidId` gives `WHERE "id" = 3fa85f64-…` (syntax error).
  Fix: `= {GetParamRef("Id")}`; `Repository.RetrieveAsync` passes `new { Id = id.Value }`
  (update and delete already pass the model). Update each client's expected-SQL tests.
  Done (`@Id` via `nameof(IWithId.Id)`). Checked Guid retrieve / update / delete against a real
  SQLite file with a scratch program. The now-unused `id` parameter of `GetRetrieveQuery` /
  `GetUpdateQuery` / `GetDeleteQuery` is removed (**breaking** for callers of `IDbClient`).
- [x] **C10 Delete the unused predicate query builder** — `src/Jeebs.Data/DbClient.CustomQueries.cs:35-68`,
  `src/Jeebs.Data/IDbClient.cs` (`GetQuery<TEntity, TModel>`), `src/Jeebs.Data/Functions/DataF.ConvertPredicatesToColumns.cs`
  No caller in `src` or `apps`; it skips unknown aliases and gives `WHERE ;` with no predicates.
  Delete both `GetQuery` overloads, `GetQueryTest`, `ConvertPredicatesToColumns`, its tests and the
  mock setups in the two `Repository/_Setup.cs`. About -115 lines. **Breaking.** Done.
- [x] **C11 Merge the two paged queries** — `src/Jeebs.Data.Common/Db.QueryAsync.cs:67-85`,
  `src/Jeebs.Data.Clients.Rqlite/RqliteDb.QueryAsync.cs`
  Same count → paging → page logic in both. Move one copy into `Jeebs.Data.Db` on top of the
  abstract `QuerySingleAsync` / `QueryAsync`; keep the transaction overload in `Data.Common`.
  Done: a protected `Db.QueryAsync(page, parts, count, items)` runs it; the paged
  `QueryAsync` is now virtual, and `Data.Common` passes its transaction through. Tests in
  `tests/Tests.Jeebs.Data/_/Db/QueryAsync_Tests.cs`.

## Phase 13 — auth and web (C4, C7, C8, C9, C12)

- [x] **C4 Sign-out redirects to nowhere** — `src/Jeebs.Mvc.Auth/Controllers/AuthController.cs:66`
  `Url.Action("Auth", "SignOut")` has action and controller swapped, so it is always null.
  Fix: `Url.Action("SignIn", "Auth")`. Test: the redirect is not empty.
- [x] **C7 Redirects trust the Host header** — `src/Jeebs.Apps.Web/Middleware/RedirectExactMiddleware.cs:64-68`
  The 301 target is built from `req.Host`, so `Host: evil.example` gets a cached permanent redirect
  to another site. Fix: `context.Response.Redirect(redirectTo, permanent: true)`.
- [x] **C8 Authorization header logged in full** — `src/Jeebs.Mvc.Auth/Functions/HttpF/HttpF.GetToken.cs`
  A non-Bearer header (e.g. `Basic` with a username and password) goes into the log.
  Fix: drop `{Header}` from the failure message. Update `GetToken_Tests` if it checks the args.
- [x] **C9 Wrong password shows no message** — `src/Jeebs.Mvc.Auth/Functions/AuthF/AuthF.DoSignInAsync.cs:78-84`,
  `src/Jeebs.Mvc.Auth/Controllers/AuthController.cs:93`
  The comment says an alert is added but none is, and `SignIn()` shows a new empty model.
  Fix: in the `TryAgain` branch add an error alert ("Unknown email or password.") and return
  `View(model with { Password = string.Empty })`.
- [x] **C12 Sign-out works from a GET** — `src/Jeebs.Mvc.Auth/Controllers/AuthController.cs:104`
  Any site can sign users out with an `<img>` tag. Fix: `[HttpPost, ValidateAntiForgeryToken]`;
  change `apps/AppMvc/Views/Auth/Index.cshtml` to a form. **Breaking.**

## Phase 14 — calendar output (C3, C14)

- [ ] **C3 Calendar text not escaped, lines end with LF** — `src/Jeebs.Calendar/VCalendar.cs:89-91`,
  `src/Jeebs.Calendar/Extensions/StringBuilder/StringBuilderExtensions.AppendMax75.cs`
  Newlines, `;` and `,` in summary / description / location break the `.ics`, and `AppendLine`
  writes `\n` on Linux where RFC 5545 needs `\r\n`. Fix: one escape helper (`\` → `\\`, `;` → `\;`,
  `,` → `\,`, newline → `\n`) on the three fields; write `\r\n`. Test: a description with a
  newline and a location with a comma.
- [ ] **C14 Lines folded by characters, not bytes** — same `AppendMax75`
  RFC 5545 folds at 75 octets; accented text makes longer lines and a split can cut an emoji.
  Fix: fold by UTF-8 byte count without splitting a rune. Test with accents and an emoji.

## Phase 15 — dependencies and small fixes (C6, C13)

- [ ] **C6 AGPL dependency in an MIT library** — `src/Jeebs.Config/ConfigValidator.cs`,
  `Newtonsoft.Json.Schema` 4.0.2 in `Directory.Packages.props`
  The package is AGPL-3.0 or paid, and every `Jeebs.Apps` app pulls it in. Fix: switch to an
  MIT-licensed validator (e.g. `JsonSchema.Net`), only used in this file. Check the existing
  config tests still pass against the same schema.
- [ ] **C13 Webhook failure details thrown away** — `src/Jeebs.Services.Webhook/WebhookDriver.cs` (`Send(FailureValue)`)
  It puts the exception in `Fields`, but the Slack driver (the only one) uses only `Content` and
  `Level`. Fix: drop `Fields` there and append `e.Message` to `Content`.
