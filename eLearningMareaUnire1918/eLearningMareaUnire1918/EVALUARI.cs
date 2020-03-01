using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace eLearningMareaUnire1918
{
    class EVALUARI
    {
        CONNECT conn = new CONNECT();

        public void insertEvaluare(int id, int nota, DateTime date)
        {
            SqlCommand command = new SqlCommand();
            command.CommandText = "INSERT INTO Evaluari(IdElev,DataEvaluare,NotaEvaluare) VALUES(@id,@data,@nota)";
            command.Connection = conn.getConnection();

            //@id,@data,@nota
            command.Parameters.Add("id", SqlDbType.Int).Value = id;
            command.Parameters.Add("data", SqlDbType.DateTime).Value = date;
            command.Parameters.Add("nota", SqlDbType.Int).Value = nota;

            conn.openConnection();
            command.ExecuteNonQuery();
            conn.closeConnection();
        }
        public DataTable getEvaluareById(int id)
        {
            SqlCommand command = new SqlCommand();
            command.CommandText = "SELECT NotaEvaluare FROM Evaluari WHERE IdElev=@id";
            command.Connection = conn.getConnection();

            //@id,@data,@nota
            command.Parameters.Add("id", SqlDbType.Int).Value = id;

            DataTable table = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = command;
            adapter.Fill(table);

            return table;
        }
        public DataTable getEvaluareDataNoteById(int id)
        {
            SqlCommand command = new SqlCommand();
            command.CommandText = "SELECT NotaEvaluare,DataEvaluare FROM Evaluari WHERE IdElev=@id";
            command.Connection = conn.getConnection();

            //@id,@data,@nota
            command.Parameters.Add("id", SqlDbType.Int).Value = id;

            DataTable table = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = command;
            adapter.Fill(table);

            return table;
        }

        public string getEvaluareNume(int id)
        {
            SqlCommand command = new SqlCommand();
            command.CommandText = "SELECT NumePrenumeUtilizator FROM Utilizatori WHERE IdUtilizator=@id";
            command.Connection = conn.getConnection();

            //@id,@data,@nota
            command.Parameters.Add("id", SqlDbType.Int).Value = id;

            DataTable table = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = command;
            adapter.Fill(table);

            return table.Rows[0][0].ToString();
        }
    }
}
