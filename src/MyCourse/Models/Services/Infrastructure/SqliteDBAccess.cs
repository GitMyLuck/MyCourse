using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.Sqlite;

namespace MyCourse.Models.Services.Infrastructure
{
    public class SqliteDBAccess : IDBAccess
    {
        

public  DataSet Query(FormattableString formattableQuery)
        {
             //Creiamo dei SqliteParameter a partire dalla FormattableString
            var queryArguments = formattableQuery.GetArguments();
            var sqliteParameters = new List<SqliteParameter>();
            for (var i = 0; i < queryArguments.Length; i++)
            {
                var parameter = new SqliteParameter(i.ToString(), queryArguments[i]);
                sqliteParameters.Add(parameter);
                queryArguments[i] = "@" + i;
            }
            string query = formattableQuery.ToString();

            //Colleghiamoci al database Sqlite, inviamo la query e leggiamo i risultati
            using(var conn = new SqliteConnection("Data Source=Data/MyCourse.db"))
            {
                conn.Open();
                using (var cmd = new SqliteCommand(query, conn))
                {
                    //Aggiungiamo i SqliteParameters al SqliteCommand
                    cmd.Parameters.AddRange(sqliteParameters);

                    //Inviamo la query al database e otteniamo un SqliteDataReader
                    //per leggere i risultati
                    using (var reader = cmd.ExecuteReader())
                    {
                        var dataSet = new DataSet();
                        
                        //Creiamo tanti DataTable per quante sono le tabelle
                        //di risultati trovate dal SqliteDataReader
                        do 
                        {
                            var dataTable = new DataTable();
                            dataSet.Tables.Add(dataTable);
                            dataTable.Load(reader);
                        } while (!reader.IsClosed);

                        return dataSet;
                    }
                }
            }    
                
        }

    }
}