using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace eLearningMareaUnire1918
{
    class UTILIZATORI
    {
        CONNECT conn = new CONNECT();

        public bool userExists(string email, string pass)
        {
            SqlCommand command = new SqlCommand();
            command.CommandText = "SELECT * FROM Utilizatori WHERE EmailUtilizator=@em AND ParolaUtilizator=@pass";
            command.Connection = conn.getConnection();

            command.Parameters.Add("em", SqlDbType.VarChar).Value = email;
            command.Parameters.Add("pass", SqlDbType.VarChar).Value = pass;

            DataTable table = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = command;
            adapter.Fill(table);

            if (table.Rows.Count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
