This project contains a single test that reproduces a `NullReferenceException` exception similar
to [efcore/#35530](https://github.com/dotnet/efcore/issues/35530).

Prerequisites
* dotnet 9
* docker

Run
```bash
dotnet test
```

(Note that the test does not set up any DB schema. The error happens before we ever hit the DB)

Exception
```
MyTest1 (444ms): Error Message: System.NullReferenceException : Object reference not set to an instance of an object.
      Stack Trace:
         at Microsoft.EntityFrameworkCore.Update.ModificationCommand.WriteJson(Utf8JsonWriter writer, Object navigationValue, IUpdateEntry parentEntry, IEntityType entityType, N
      ullable`1 ordinal, Boolean isCollection, Boolean isTopLevel)
         at Microsoft.EntityFrameworkCore.Update.ModificationCommand.<GenerateColumnModifications>g__HandleJson|41_4(List`1 columnModifications, <>c__DisplayClass41_0&)
         at Microsoft.EntityFrameworkCore.Update.ModificationCommand.GenerateColumnModifications()
         at Microsoft.EntityFrameworkCore.Update.ModificationCommand.<>c.<get_ColumnModifications>b__33_0(ModificationCommand command)
         at Microsoft.EntityFrameworkCore.Internal.NonCapturingLazyInitializer.EnsureInitialized[TParam,TValue](TValue& target, TParam param, Func`2 valueFactory)
         at Microsoft.EntityFrameworkCore.Update.ModificationCommand.get_ColumnModifications()
         at Npgsql.EntityFrameworkCore.PostgreSQL.Update.Internal.NpgsqlUpdateSqlGenerator.AppendInsertOperation(StringBuilder commandStringBuilder, IReadOnlyModificationCommand
       command, Int32 commandPosition, Boolean overridingSystemValue, Boolean& requiresTransaction)
         at Npgsql.EntityFrameworkCore.PostgreSQL.Update.Internal.NpgsqlUpdateSqlGenerator.AppendInsertOperation(StringBuilder commandStringBuilder, IReadOnlyModificationCommand
       command, Int32 commandPosition, Boolean& requiresTransaction)
         at Microsoft.EntityFrameworkCore.Update.ReaderModificationCommandBatch.AddCommand(IReadOnlyModificationCommand modificationCommand)
         at Microsoft.EntityFrameworkCore.Update.ReaderModificationCommandBatch.TryAddCommand(IReadOnlyModificationCommand modificationCommand)
         at Microsoft.EntityFrameworkCore.Update.Internal.CommandBatchPreparer.CreateCommandBatches(IEnumerable`1 commandSet, Boolean moreCommandSets, Boolean assertColumnModifi
      cation, ParameterNameGenerator parameterNameGenerator)+MoveNext()
         at Microsoft.EntityFrameworkCore.Update.Internal.CommandBatchPreparer.BatchCommands(IList`1 entries, IUpdateAdapter updateAdapter)+MoveNext()
         at Microsoft.EntityFrameworkCore.Update.Internal.BatchExecutor.ExecuteAsync(IEnumerable`1 commandBatches, IRelationalConnection connection, CancellationToken cancellati
      onToken)
         at Microsoft.EntityFrameworkCore.Storage.RelationalDatabase.SaveChangesAsync(IList`1 entries, CancellationToken cancellationToken)
         at Microsoft.EntityFrameworkCore.ChangeTracking.Internal.StateManager.SaveChangesAsync(IList`1 entriesToSave, CancellationToken cancellationToken)
         at Microsoft.EntityFrameworkCore.ChangeTracking.Internal.StateManager.SaveChangesAsync(StateManager stateManager, Boolean acceptAllChangesOnSuccess, CancellationToken c
      ancellationToken)
         at Npgsql.EntityFrameworkCore.PostgreSQL.Storage.Internal.NpgsqlExecutionStrategy.ExecuteAsync[TState,TResult](TState state, Func`4 operation, Func`4 verifySucceeded, C
      ancellationToken cancellationToken)
         at Microsoft.EntityFrameworkCore.DbContext.SaveChangesAsync(Boolean acceptAllChangesOnSuccess, CancellationToken cancellationToken)
         at Microsoft.EntityFrameworkCore.DbContext.SaveChangesAsync(Boolean acceptAllChangesOnSuccess, CancellationToken cancellationToken)
         at ReproNreJsonElement.MyTest.MyTest1() in /home/chris/devel/tinker/ReproNreJsonElement/Program.cs:line 35
         at ReproNreJsonElement.MyTest.MyTest1() in /home/chris/devel/tinker/ReproNreJsonElement/Program.cs:line 35
         at NUnit.Framework.Internal.TaskAwaitAdapter.GenericAdapter`1.GetResult()
         at NUnit.Framework.Internal.AsyncToSyncAdapter.Await[TResult](Func`1 invoke)
         at NUnit.Framework.Internal.AsyncToSyncAdapter.Await(Func`1 invoke)
         at NUnit.Framework.Internal.Commands.TestMethodCommand.RunTestMethod(TestExecutionContext context)
         at NUnit.Framework.Internal.Commands.TestMethodCommand.Execute(TestExecutionContext context)
         at NUnit.Framework.Internal.Execution.SimpleWorkItem.<>c__DisplayClass3_0.<PerformWork>b__0()
         at NUnit.Framework.Internal.ContextUtils.<>c__DisplayClass1_0`1.<DoIsolated>b__0(Object _)
         at System.Threading.ExecutionContext.RunInternal(ExecutionContext executionContext, ContextCallback callback, Object state)
      --- End of stack trace from previous location ---
         at System.Threading.ExecutionContext.RunInternal(ExecutionContext executionContext, ContextCallback callback, Object state)
         at NUnit.Framework.Internal.ContextUtils.DoIsolated(ContextCallback callback, Object state)
         at NUnit.Framework.Internal.ContextUtils.DoIsolated[T](Func`1 func)
         at NUnit.Framework.Internal.Execution.SimpleWorkItem.PerformWork()

```
