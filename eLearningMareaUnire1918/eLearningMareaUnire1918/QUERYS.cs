using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace eLearningMareaUnire1918
{
    class QUERYS
    {
        CONNECT conn = new CONNECT();
        public void dbccTabels()
        {
            SqlCommand command = new SqlCommand();
            command.CommandText = "DBCC CHECKIDENT (Evaluari,RESEED,0)";
            command.Connection = conn.getConnection();

            conn.openConnection();
            command.ExecuteNonQuery();
            conn.closeConnection();

            command = new SqlCommand();
            command.CommandText = "DBCC CHECKIDENT (Itemi,RESEED,0)";
            command.Connection = conn.getConnection();

            conn.openConnection();
            command.ExecuteNonQuery();
            conn.closeConnection();

            command = new SqlCommand();
            command.CommandText = "DBCC CHECKIDENT (Utilizatori,RESEED,0)";
            command.Connection = conn.getConnection();

            conn.openConnection();
            command.ExecuteNonQuery();
            conn.closeConnection();
        }
    }
}
