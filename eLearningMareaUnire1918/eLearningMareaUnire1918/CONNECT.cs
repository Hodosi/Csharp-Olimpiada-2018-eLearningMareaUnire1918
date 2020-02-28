using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace eLearningMareaUnire1918
{
    class CONNECT
    {
        public static string constring = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|eLearning1918.mdf;Integrated Security=True;Connect Timeout=30";

        SqlConnection conn = new SqlConnection(constring);

        public SqlConnection getConnection()
        {
            return conn;
        }

        public void openConnection()
        {
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
        }

        public void closeConnection()
        {
            if (conn.State == ConnectionState.Open)
            {
                conn.Close();
            }
        }
    }
}
