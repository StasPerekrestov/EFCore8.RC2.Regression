# Regression issue in RC2

Steps to reproduce

1. `docker run --rm --name some-postgres -e POSTGRES_PASSWORD=secret -p 5432:5432 -d postgres`
2. `cd ./RC2.CompiledQueries.Issue`
3. `dotnet ef database update`


Result:
Unhandled exception. System.InvalidOperationException: The compiled query '(ctx, id) => ctx.Users
    .Where(t => t.Id == id)
    .Select(t => new LookupModel(
        t.Id,
        t.FirstName,
        null
    ))' was executed with a different model than it was compiled against. Compiled queries can only be used with a single model.
   at Microsoft.EntityFrameworkCore.Query.Internal.CompiledQueryBase`2.ExecuteCore(TContext context, CancellationToken cancellationToken, Object[] parameters)
   at Microsoft.EntityFrameworkCore.Query.Internal.CompiledQueryBase`2.ExecuteCore(TContext context, Object[] parameters)
   at Microsoft.EntityFrameworkCore.Query.Internal.CompiledAsyncEnumerableQuery`2.Execute[TParam1](TContext context, TParam1 param1)
   at Program.<>c__DisplayClass0_0.<<<Main>$>g__FetchAndDump|3>d.MoveNext() in ./develop/other/EF.RC2.CompiledQueries.Issue/RC2.CompiledQueries.Issue/Program.cs:line 18
--- End of stack trace from previous location ---
   at Program.<Main>$(String[] args) in ./develop/other/EF.RC2.CompiledQueries.Issue/RC2.CompiledQueries.Issue/Program.cs:line 14
   at Program.<Main>$(String[] args) in ./develop/other/EF.RC2.CompiledQueries.Issue/RC2.CompiledQueries.Issue/Program.cs:line 14
   at Program.<Main>$(String[] args) in ./develop/other/EF.RC2.CompiledQueries.Issue/RC2.CompiledQueries.Issue/Program.cs:line 14
   at Program.<Main>(String[] args)