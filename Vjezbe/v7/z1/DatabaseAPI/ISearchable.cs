namespace DatabaseAPI;

using Microsoft.Data.Sqlite;

public interface ISearchable 
{
  void FromReader(SqliteDataReader reader);
}
