using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;


namespace A2_NWBA.Code.Utils
{
    public static class SqlTools
    {
        public static SqlConnection GetConnection()
        {
            return new SqlConnection(ConfigurationManager.ConnectionStrings["ConnString"].ConnectionString);
        }

        public static void ExecuteNonQuery(string procedureName, params SqlParameter[] parameters)
        {
            using (SqlConnection connection = GetConnection())
            {
                SqlCommand command = new SqlCommand();
                command.Connection = connection;
                command.CommandText = procedureName;
                command.CommandType = CommandType.StoredProcedure;

                if (parameters.Length > 0) 
                { 
                    foreach (SqlParameter param in parameters)
                    {
                        command.Parameters.Add(param);
                    }
                }

                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
            }
        }

        public static object ExecuteScalar(string procedureName, params SqlParameter[] parameters)
        {
            object scalar;

            using (SqlConnection connection = GetConnection())
            {
                SqlCommand command = new SqlCommand();
                command.Connection = connection;
                command.CommandText = procedureName;
                command.CommandType = CommandType.StoredProcedure;

                if (parameters.Length > 0)
                {
                    foreach (SqlParameter param in parameters)
                    {
                        command.Parameters.Add(param);
                    }
                }

                connection.Open();
                scalar = command.ExecuteScalar();
                connection.Close();
            }
            return scalar;
        }

        public static void ExecuteReader(string procedureName, SqlParameter[] parameters, Action<SqlDataReader> readerCallback)
        {
            using (SqlConnection connection = GetConnection())
            {
                connection.Open();
                ExecuteReader(connection, procedureName, parameters, CommandBehavior.Default, readerCallback);
                connection.Close();
            }
        }


        public static void ExecuteReader(SqlConnection connectionRef, string procedureName, SqlParameter[] parameters, CommandBehavior behavior, Action<SqlDataReader> readerCallback)
        {
            using (SqlCommand command = new SqlCommand(procedureName, connectionRef))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.CommandTimeout = 180;

                if (parameters.Length > 0)
                {
                    foreach (SqlParameter param in parameters)
                    {
                        command.Parameters.Add(param);
                    }
                }

                SqlDataReader reader = command.ExecuteReader(behavior);

                try
                {
                    readerCallback(reader);
                }
                finally
                {
                    if (reader != null) 
                        reader.Close();
                } 
            } 
        }
    }
}