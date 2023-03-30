using System.Data;
using Microsoft.Data.Sqlite;

namespace MyCourse.Models.Services.Infrastructure
{
    public class SqliteDBAccess : IDBAccess
    {
        public DataSet Query(string query)
        {
            //  L'utilizzo di using permette di fare il Dispose della connessione
            //  comunque anche in caso si eccezione
            //  SqliteConnection
            using (var conn = new SqliteConnection("Data Source=Data/MyCourse.db"))
            {
                    conn.Open();
                    //  SqliteCommand
                    using (var cmd = new SqliteCommand(query, conn))
                    {
                            // SqliteReader
                            using (var reader = cmd.ExecuteReader())
                            {
                                    var dataSet = new DataSet();
                                    var dataTable = new DataTable();
                                    dataSet.Tables.Add(dataTable);
                                    dataTable.Load(reader);
                                    return dataSet;
                            }   //end SqliteReader
                    }   // end SQLiteCommand
                
            }   // end SQLiteConnection
            
        }   

        
    }
}