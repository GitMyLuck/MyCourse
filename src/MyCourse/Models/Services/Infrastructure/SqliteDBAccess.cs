using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;
using Microsoft.Data.Sqlite;

namespace MyCourse.Models.Services.Infrastructure
{
    public class SqliteDBAccess : IDBAccess
    {
        

public  DataSet Query(FormattableString formattableQuery)
        {
                
                string connetionString =  @"Data Source=MAIN-PC\SQLEXPRESS;
                                            Initial Catalog=MyCourse;
                                            Integrated Security=true;
                                            TrustServerCertificate=true;";

                var queryArguments = formattableQuery.GetArguments();
                var sqlParameters = new List<SqlParameter>();
                for ( var i = 0; i < queryArguments.Length; i++)
                {
                    var parameter = new SqlParameter("@" + i.ToString(), queryArguments[i]);
                    sqlParameters.Add(parameter);
                }
                string query = formattableQuery.ToString();

                
                using (var conn = new SqlConnection(connetionString))
                {
                    conn.Open();
                    

                    // viene lanciata la query
                    var command = new SqlCommand(query, conn);

                    using(var  reader = command.ExecuteReader())
                    {
                            var dataSet = new DataSet();
                            do
                            {
                                var dataTable = new DataTable();
                                dataSet.Tables.Add(dataTable);
                                dataTable.Load(reader);
                            }   while (!reader.IsClosed);
                        reader.Close();
                        return dataSet;
                    }
                    
                }   // end of using
        }

        
    }
}