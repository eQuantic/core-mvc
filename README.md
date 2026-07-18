# eQuantic.Core.Mvc Library

MVC integration for the eQuantic Core stack.

The 2.x package provides model binders that bind `IFiltering`/`ISorting` query-string parameters
(from [eQuantic.Linq](https://www.nuget.org/packages/eQuantic.Linq) 1.x/2.x) directly into MVC
action parameters:

```csharp
services.AddControllers()
    .AddFilterModelBinder()
    .AddSortModelBinder();
```

## Using eQuantic.Linq v3?

From **eQuantic.Linq v3** on, query-string binding lives in the Linq family itself:
[eQuantic.Linq.Web.AspNetCore](https://www.nuget.org/packages/eQuantic.Linq.Web.AspNetCore)
binds a complete `EntityQuery<T>` (filter, ordering, paging, projection) in MVC controllers and
Minimal APIs, with OpenAPI documentation available via
[eQuantic.Linq.Web.Swashbuckle](https://www.nuget.org/packages/eQuantic.Linq.Web.Swashbuckle) /
[eQuantic.Linq.Web.OpenApi](https://www.nuget.org/packages/eQuantic.Linq.Web.OpenApi).
See the [eQuantic.Linq documentation](https://github.com/eQuantic/core-linq/tree/main/docs).

This package is **not** deprecated: 2.x remains the binding layer for the v2 stack, and
`eQuantic.Core.Mvc` remains the home of Core ↔ ASP.NET Core MVC integration going forward.

## Install

```dos
Install-Package eQuantic.Core.Mvc
```

MIT © eQuantic Tech
