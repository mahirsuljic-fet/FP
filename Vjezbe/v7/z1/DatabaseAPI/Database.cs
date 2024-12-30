namespace DatabaseAPI;

using System;
using Microsoft.Data.Sqlite;

public class Database : IDisposable {
  private readonly string _databasePath;
  private bool _initialized = false;
  public SqliteConnection Connection { get; private set; }

  public Database(string databasePath, string createTablesString) {
    if (string.IsNullOrEmpty(databasePath)) {
      throw new ArgumentException("Database path cannot be null or empty.",
                                  nameof(databasePath));
    }
    this._databasePath = databasePath;
    InitializeConnection(createTablesString);
  }

  private void InitializeConnection(string createTablesString) {
    var connectionString = $"Data Source={this._databasePath}";
    Connection = new SqliteConnection(connectionString);
    Connection.Open();

    using var cmd = Connection.CreateCommand();

    cmd.CommandText = createTablesString;
    cmd.ExecuteNonQuery();
    _initialized = true;
  }

  public void Dispose() { Connection.Dispose(); }

  public List<T> Search<T>(string sql, Dictionary<string, object>? parameters) where T : ISearchable, new() {

    if (string.IsNullOrWhiteSpace(sql))
      throw new ArgumentException("SQL query cannot be null or empty.",
                                  nameof(sql));

    var results = new List<T>();

    using var command = Connection.CreateCommand();
    command.CommandText = sql;

    if (parameters != null) {
      foreach (var param in parameters) {
        command.Parameters.AddWithValue(param.Key, param.Value);
      }
    }

    using var reader = command.ExecuteReader();
    while (reader.Read()) {
      var item = new T();
      item.FromReader(reader);
      results.Add(item);
    }
    return results;
  }
}
