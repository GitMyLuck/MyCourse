using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;


namespace MyCourse.Models.Services.Infrastructure
{
    public class SQLServerDBAccess : IDBAccess
    {
        public  async Task<DataSet> QueryAsync(FormattableString formattableQuery)
        {
                
                string connetionString =  @"Data Source=MAIN-PC\SQLEXPRESS;
                                            Initial Catalog=MyCourse;
                                            Integrated Security=true;
                                            TrustServerCertificate=true;";

                // ciclo di sostituzione di tutti i parametri passati
                // in maniera da sanitizzarli per SQLInjection
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
                    await conn.OpenAsync();
                    

                    // viene lanciata la query
                    using (var command = new SqlCommand(query, conn))
                    {

                                using(var  reader = await command.ExecuteReaderAsync())
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
                                }  // end Execute Reader

                    }   // end SqlCommand
                    
                }   // end SQLConnection
        }

        
    
    }
}