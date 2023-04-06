using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.Sqlite;

namespace MyCourse.Models.Services.Infrastructure
{
    public class SqliteDBAccess : IDBAccess
    {
        public DataSet Query(FormattableString formattableQuery)
        {
            
            var queryArguments = formattableQuery.GetArguments();
            var sqliteParameters = new List<SqliteParameter>();
            for ( var i = 0; i < queryArguments.Length; i++)
            {
                var parameter = new SqliteParameter(i.ToString(), queryArguments[i]);
                sqliteParameters.Add(parameter);
                queryArguments[i] = "@" + i;
            }
            string query = formattableQuery.ToString();
            //  L'utilizzo di using permette di fare il Dispose della connessione
            //  comunque anche in caso si eccezione
            //  SqliteConnection
            using (var conn = new SqliteConnection("Data Source=Data/MyCourse.db"))
            {
                    conn.Open();
                    //  SqliteCommand
                    using (var cmd = new SqliteCommand(query, conn))
                    {
                            cmd.Parameters.AddRange(sqliteParameters);
                            // SqliteReader
                            SqliteParameterCollection paramCollection = cmd.Parameters;
                            string parameterList = "";
                            for (int i = 0; i < paramCollection.Count; i++)
                            {
                                    parameterList += String.Format("  {0}, {1}, {2}, {3}, {4}\n",
                                    paramCollection[i].DbType,  
                                    paramCollection[i].ParameterName,  
                                    paramCollection[i].Value,
                                    paramCollection[i].Direction);
                            }
                                    Console.WriteLine("Parameter Collection:\n" + parameterList);

                            using (var reader = cmd.ExecuteReader())
                            {
                                    var dataSet = new DataSet();
                                    do
                                    {
                                            var dataTable = new DataTable();
                                            dataSet.Tables.Add(dataTable);
                                            dataTable.Load(reader);
                                    } while (!reader.IsClosed);
                                    
                                    return dataSet;
                            }   //end SqliteReader
                    }   // end SQLiteCommand
                
            }   // end SQLiteConnection
            
        }   

        
    }
}