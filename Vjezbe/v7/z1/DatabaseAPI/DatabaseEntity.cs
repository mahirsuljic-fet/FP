namespace DatabaseAPI;

using System;
using Microsoft.Data.Sqlite;

public abstract class DatabaseEntity
{
  protected readonly Database Database;

  protected DatabaseEntity(Database database)
  {
    this.Database = database;
  }

  protected void Commit(string sql) 
  {
    if (string.IsNullOrWhiteSpace(sql))
      throw new ArgumentException("SQL query cannot be null or empty.", nameof(sql));
    using var cmd = new SqliteCommand(sql, Database.Connection);
    BindCommitParameters(cmd);
    cmd.ExecuteNonQuery();

  }

  protected abstract void BindCommitParameters(SqliteCommand cmd);

}
