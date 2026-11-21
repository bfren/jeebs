# Audit fix plan

Four audits so far, phases 1–17 done (D10 still to investigate). Each phase is one issue / branch, one commit per item, and ends with:

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
| 12–15 | Third audit, 2026-11-08 (v10.0.0-beta.1) | C1–C14 | #634, #637, #639, #641 | yes |
| 16–17 | Fourth audit, 2026-11-11 (v10.0.0-beta.2) | D1–D14 | #647, #648 | yes (D10 open) |

Released in **v10.0.0-beta.2** (2026-11-10, #645 / #646), the first published v10 pre-release
(beta.1 was never released). Its release notes were built from the breaking changes list below, so
the 10.0.0 notes should start from beta.2's and add only what lands after it.

### Done after all

- **#12 reads without a transaction** (first audit, put off as "won't do"). Done in phase 12
  (#635): the unit of work's transaction is lazy, so only a write begins it and read-only work has
  none, and `IAdapter` methods take the `IDbConnection` with a nullable `IDbTransaction`.

### Won't do

- **Rate limiting on the sign-in route.**
- **D7 greedy shortcode regexes in `GenerateExcerpt`.** WordPress shortcodes don't nest or sit
  side by side in our content, so losing the text between two of them is acceptable.
- **ImageSharp as an image driver.** Six Labors Split License: Apache 2.0 only for open-source use,
  a transitive dependency, under 1M USD revenue or non-profits, otherwise paid - and 4.x fails Release
  builds of the project that references it without a licence key. The Stb driver (D15) is used instead.

### Not audited yet

WordPress tables, entities and enums; `Data.Testing` (skimmed only); the sample apps other than
`AppMvc` and `AppConsoleWp`; whether the `Rqlite` package reuses its HTTP connection across
`Connect()` calls. None of the audits ran the tests or started an app before
reporting, so findings come from reading the code.

### Next audit

- **Check all doc comments.** Every XML doc comment in `src`: `cref`s and `inheritdoc`s that point at
  removed or renamed members, `param`s that no longer exist, and summaries that no longer match the
  code. The build only flags unresolved `cref`s (CS1574, e.g. `src/Jeebs.Data/DbClient.StandardQueries.cs:39`
  still names `GetRetrieveQuery{TEntity, TModel}(object)`); the rest needs reading.

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
- **Config schema:** `jeebsconfig.json` is validated with NJsonSchema (MIT) instead of
  Newtonsoft.Json.Schema (AGPL). It accepts the same files, but the error text in
  `ConfigSchemaValidationFailedException` is worded differently.
- **Images:** `ImageFormat.Bmp`, `Gif` and `Ico` are removed (Skia can't write them; saving crashed).
  The remaining values are renumbered: `Unsupported = 0` (new), `Jpeg = 1`, `Png = 2` (were 3 and 4),
  so any format stored or sent as a number must be converted. `IImageDriver.FromStream` returns `Result<IImageWrapper>`, and both `FromFile` and `FromStream`
  fail with "Not a valid image." instead of throwing. `IImageWrapper` is `IDisposable`, so dispose
  each wrapper (including the one `ApplyMask` returns) to free its memory. The Skia driver now
  brings the Linux native library, so apps no longer need to add it. Publish with `-r <rid>`
  (e.g. `-r linux-x64`): a publish without one copies SkiaSharp's native library for every
  platform, about 470 MB, against about 12 MB for one.

---

# Fourth audit — 2026-11-11 (v10.0.0-beta.2)

Items are numbered D1–D14 to match the audit report. Covered the parts earlier audits skipped:
WordPress beyond the query builder, `Services.Drawing` / Skia, and the `AppMvc` / `AppConsoleWp` samples.
D7 is in Won't do; D10 is investigate-first.

## Phase 16 — WordPress (D1, D2, D3, D4, D11 part, D12, D13)

- [x] **D1 Embed URLs written into HTML unescaped** — `src/Jeebs.WordPress/ContentFilters/Blocks/YouTube.cs:48`,
  `src/Jeebs.WordPress/ContentFilters/Blocks/Vimeo.cs:17`
  `Uri.ToString()` un-escapes, so `?x="><script>` comes back raw; Vimeo pastes `embed.Url` straight
  into `data-url="…"`. **Investigate first:** as an Author (no `unfiltered_html`), save an embed
  whose url is `https://vimeo.com/1" onmouseover="alert(1)` through the REST API, then read
  `post_content` from the database. kses runs over block attributes for those users, which removes
  tags but probably not a bare `"`. Editors and admins have `unfiltered_html` on single sites, so
  they can save anything. Fix either way (escaping output is cheap): `WebUtility.HtmlEncode` on the
  URL in both, and `uri.AbsoluteUri` instead of `uri` for YouTube. Test: a url containing `"` and
  `<` comes out encoded.
  Done (the video ID is encoded too). The WordPress check hasn't been run.
- [x] **D2 One bad embed URL throws for the whole query** — `src/Jeebs.WordPress/ContentFilters/Blocks/YouTube.cs:41`
  `YouTubeRegex` matches every `wp:embed` block, not only YouTube, and `new Uri(youTube.Url)` throws
  on a relative, empty or missing url (`UriFormatException` / `ArgumentNullException`). Nothing
  catches it. **Investigate first:** search real `post_content` for `wp:embed` blocks with no `"url"`
  or a relative one (old `core-embed/*` blocks, imports, REST writes skip the editor). Fix:
  `if (!Uri.TryCreate(youTube.Url, UriKind.Absolute, out var uri)) return;`. Test: a block with
  `"url":"youtu.be/abc"` is left unchanged.
  Done, tested with relative, empty and missing urls. The content search hasn't been run.
- [x] **D3 Only the first WordPress instance registers its custom types** — `src/Jeebs.WordPress/Wp.cs:111`
  `Init<WpConfig>` should be `Init<TConfig>`, otherwise `WpBcg` and `WpUsa` share one key and only
  the first resolved runs `RegisterCustomPostTypes` / `RegisterCustomTaxonomies`. Test (part of D11):
  two `Wp` subclasses with different config types both have their `Register…` methods called.
  Done: `tests/Tests.Jeebs.WordPress/_/Wp/Constructor_Tests.cs`, which failed before the fix.
- [x] **D4 `WpDb` resets every Dapper type handler** — `src/Jeebs.WordPress/WpDb.cs:97`
  `ResetTypeHandlers()` deletes handlers other databases in the same app added (Guid, JSON, lists),
  and can race with a query on another request. WordPress doesn't need them, but a multi-db app does.
  Fix: delete the line; the WordPress handlers are added straight after it.
- [x] **D12 `SetMeta` scans all meta rows per post** — `src/Jeebs.WordPress/Functions/QueryPostsF.SetMeta.cs:32`
  Fix: `var byPost = postsMeta.ToLookup(m => m.PostId);` once, then filter `byPost[post.Id]`.
  Existing `SetMeta` tests cover it.
- [x] **D13 Attachment query selects `Url` twice** — `src/Jeebs.WordPress/Functions/QueryAttachmentsF.GetQuery.cs:34`
  The `guid` column is overwritten by the `CONCAT` one on line 36. Delete line 34 and update the
  expected SQL in the `GetQuery` tests.

## Phase 17 — images (D5, D6, D8, D9, D11 part, D14)

- [x] **D5 Remove unsupported image formats** — `src/Jeebs.Services.Drawing/ImageFormat.cs`,
  `src/Jeebs.Services.Drivers.Drawing.Skia/Extensions/…/ImageFormatExtensions…cs:19-26`
  Skia only encodes JPEG, PNG and WebP; `Encode` returns null for the rest and `Save` throws a
  `NullReferenceException`. Fix: remove `Bmp`, `Gif` and `Ico` (keep `Jpeg = 3`, `Png = 4` so stored
  values don't change) and their switch arms. Update the `GetEncodedImageFormat` tests. **Breaking.**
  Done. There were no `GetEncodedImageFormat` tests, so they are new (see D11).
- [x] **D6 Invalid image data throws** — `src/Jeebs.Services.Drivers.Drawing.Skia/ImageDriver.cs:25, 33`
  `SKImage.FromEncodedData` returns null for a non-image. Fix: return
  `R.Fail("Not a valid image.")` when it is null, and make `FromStream` return
  `Result<IImageWrapper>` like `FromFile`. Test: a text file / stream fails. **Breaking.**
- [x] **D8 Resizing uses nearest-neighbour sampling** — `src/Jeebs.Services.Drivers.Drawing.Skia/ImageWrapper.cs:80`
  `new()` sampling options mean no smoothing. Fix:
  `new SKSamplingOptions(SKFilterMode.Linear, SKMipmapMode.Linear)`; drop the `IsAntialias` paint
  if it no longer does anything.
  Done; the paint is gone (`IsAntialias` only affects shape edges). The test checks that halving
  1px black / white stripes gives grey, and fails (pure black) with the old `new()`.
- [x] **D9 Image wrappers can't be disposed through the interface** — `src/Jeebs.Services.Drawing/IImageWrapper.cs:9`
  Native bitmaps wait for the finaliser. Fix: `IImageWrapper : IDisposable` (base `ImageWrapper`
  gets an abstract or virtual `Dispose`), and dispose both wrappers in `AppMvc`'s `ImageController`. **Breaking.**
  Done with an abstract `Dispose`.
- [x] **D11 Tests for the image code** — new `tests/Tests.Jeebs.Services.Drawing` (register in the
  `.slnx` under `/tests/`) for `ImageF.CalculateNewSize` / `CalculateMask` (e.g. 400×200 into
  100×100), and a new `Tests.Jeebs.Services.Drivers.Drawing.Skia` for D5, D6 and D8 (neither project exists yet). The `Wp` test is under D3.
  Done: 6 + 11 tests, under new `/tests/.Services/` and `/tests/.Services/.Drivers/` solution folders.
  SkiaSharp ships no Linux native library, so CI (Ubuntu) couldn't run them, and nor could any app on
  a Linux server. The driver now references `SkiaSharp.NativeAssets.Linux.NoDependencies` (no
  fontconfig needed, as nothing draws text), which brings glibc, musl and ARM builds.
- [x] **D14 Sample image controller** — `apps/AppMvc/Controllers/ImageController.cs:15, 25`
  Content type `image/jpg` for PNG bytes, a hard-coded `Q:\` path, and unbounded `width` / `height`
  from the query string (`100000 × 100000` tries ~40 GB). Fix: `"image/png"`, the path from config,
  and cap both sizes (e.g. 4000) before `ApplyMask`.
  Done: `ImagePath` in `appsettings.Development.json`, 400 Bad Request outside 1–4000 (both sides
  required - with one at 0 the other is worked out from the image, so the cap wouldn't hold).
- [x] **D15 Managed image driver** — new `src/Jeebs.Services.Drivers.Drawing.Stb`
  SkiaSharp's native libraries add ~470 MB to a publish without `-r`. The Stb driver has no native
  code: `StbImageSharp` (MIT / Unlicense) reads, `StbImageWriteSharp` (public domain) writes JPEG and
  PNG, and `PixelF.Resize` crops and resizes with a two-pass tent filter (bilinear when enlarging,
  area-weighted when shrinking, colour weighted by alpha). Skia applies EXIF orientation and stb
  doesn't, so `PixelF.GetOrientation` / `Orient` do it; all eight orientations match the Skia driver.
  About 4× slower than Skia: ~195 ms against ~49 ms for a 6000×4000 JPEG to a 400×300 thumbnail
  (Apple Silicon, Release). 31 tests in `tests/Tests.Jeebs.Services.Drivers.Drawing.Stb`.

## Investigate later — D10

- [ ] **D10 Custom fields query per post** — `src/Jeebs.WordPress/Functions/QueryPostsF.HydrateAsync.cs:51`,
  `src/Jeebs.WordPress/CustomFields/AttachmentCustomField.cs:65`, `TermCustomField.cs`
  Each attachment field runs a posts query plus a meta query for every post (about 120 queries for
  20 sermons with three attachment fields). Batching means changing `ICustomField.HydrateAsync`
  from one post to many, so it is a refactor, not a fix. First measure the query count and time on
  a real sermons list page; only design a batched hydrate if it is slow.

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

- [x] **C3 Calendar text not escaped, lines end with LF** — `src/Jeebs.Calendar/VCalendar.cs:89-91`,
  `src/Jeebs.Calendar/Extensions/StringBuilder/StringBuilderExtensions.AppendMax75.cs`
  Newlines, `;` and `,` in summary / description / location break the `.ics`, and `AppendLine`
  writes `\n` on Linux where RFC 5545 needs `\r\n`. Fix: one escape helper (`\` → `\\`, `;` → `\;`,
  `,` → `\,`, newline → `\n`) on the three fields; write `\r\n`. Test: a description with a
  newline and a location with a comma.
  Done (`VCalendar.Escape`, `Escape_Tests`). `Escape` also treats a null field as empty, so an event
  with no location writes `LOCATION:` as before instead of throwing.
- [x] **C14 Lines folded by characters, not bytes** — same `AppendMax75`
  RFC 5545 folds at 75 octets; accented text makes longer lines and a split can cut an emoji.
  Fix: fold by UTF-8 byte count without splitting a rune. Test with accents and an emoji.
  Done: 74 bytes of text per line (each folded line starts with a space), so ASCII output is
  unchanged. `AppendMax75_Tests` covers accents, an emoji at the fold and a mixed string.

## Phase 15 — dependencies and small fixes (C6, C13)

- [x] **C6 AGPL dependency in an MIT library** — `src/Jeebs.Config/ConfigValidator.cs`,
  `Newtonsoft.Json.Schema` 4.0.2 in `Directory.Packages.props`
  The package is AGPL-3.0 or paid, and every `Jeebs.Apps` app pulls it in. Fix: switch to an
  MIT-licensed validator (e.g. `JsonSchema.Net`), only used in this file. Check the existing
  config tests still pass against the same schema.
  Done with NJsonSchema 11.6.1: JsonSchema.Net 9.x ships under a maintenance-fee EULA, and 8.0.5
  (the last plain MIT release) is frozen. NJsonSchema can't resolve refs into nested `definitions`,
  so they are flattened. There were no validator tests; `Validate_Tests` was written against the
  old validator first (valid, invalid, comments / trailing commas, every sample app config).
- [x] **C13 Webhook failure details thrown away** — `src/Jeebs.Services.Webhook/WebhookDriver.cs` (`Send(FailureValue)`)
  It puts the exception in `Fields`, but the Slack driver (the only one) uses only `Content` and
  `Level`. Fix: drop `Fields` there and append `e.Message` to `Content`.
  Done: `Content` gets the exception type and message on a new line. No test - there is no
  `Tests.Jeebs.Services.Webhook` project.
