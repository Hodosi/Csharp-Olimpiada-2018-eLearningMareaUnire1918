using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace eLearningMareaUnire1918
{
    class ITEMI
    {
        CONNECT conn = new CONNECT();

        public int ItemCount(int tip)
        {
            SqlCommand command = new SqlCommand();
            command.CommandText = "SELECT * FROM Itemi WHERE TipItem=@tip";
            command.Connection = conn.getConnection();

            command.Parameters.Add("tip", SqlDbType.VarChar).Value = tip;


            DataTable table = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = command;
            adapter.Fill(table);

            return table.Rows.Count;
        }

        public DataTable ItemById(int id)
        {
            SqlCommand command = new SqlCommand();
            command.CommandText = "SELECT * FROM Itemi WHERE IdItem=@id";
            command.Connection = conn.getConnection();

            command.Parameters.Add("id", SqlDbType.VarChar).Value = id;


            DataTable table = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = command;
            adapter.Fill(table);

            return table;
        }
    }
}
