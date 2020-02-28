using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.IO;

namespace eLearningMareaUnire1918
{
    public partial class eLearning1918_start : Form
    {
        public eLearning1918_start()
        {
            InitializeComponent();
        }
        CONNECT conn = new CONNECT();
        QUERYS que = new QUERYS();
        UTILIZATORI util = new UTILIZATORI();
        List<string> imagini = new List<string>();
        int imgIndex;
        private void eLearning1918_start_Load(object sender, EventArgs e)
        {
            dbcheck();
            sterge();
            initializare();
            //---------------------------------------
            initImg();
            auto();
        }

        private void button_Login_Click(object sender, EventArgs e)
        {
            string email = this.textBox_email.Text;
            string parola = this.textBox_parola.Text;

            if (util.userExists(email, parola))
            {
                this.Hide();
                eLearning1918_Elev frm = new eLearning1918_Elev();
                frm.ShowDialog();
                this.Close();
            }
            else
            {
                this.textBox_email.ResetText();
                this.textBox_parola.ResetText();
                MessageBox.Show("Eroare Autentificare");
            }
        }
        //--------------------------------------

        public void initImg()
        {
            imgIndex = 0;
            string fn = Application.StartupPath + @"\Resurse\imaginislideshow";
            DirectoryInfo directory = new DirectoryInfo(fn);
            FileInfo[] file = directory.GetFiles();
            foreach (FileInfo info in file)
            {
                imagini.Add(info.ToString());
            }
            string imgName = Application.StartupPath + @"\Resurse\imaginislideshow\" + imagini[imgIndex];
            this.pictureBox_slideshow.Image = Image.FromFile(imgName);

            //------------------------------------------
            this.progressBar1.Maximum = imagini.Count;
            this.progressBar1.Value = 1;

            //-----------------------------------------
            this.timer1.Enabled = false;

            //----------------------------------------
            string imgNameUser = Application.StartupPath + @"\Resurse\user.bmp";
            this.pictureBox_login.Image = Image.FromFile(imgNameUser);
        }

        public void auto()
        {
            this.button_Inainte.Enabled = false;
            this.button_Inapoi.Enabled = false;
            this.button_Auto_Manual.Text = "Manual";
            this.timer1.Enabled = true;
        }
        public void manual()
        {
            this.button_Inainte.Enabled = true;
            this.button_Inapoi.Enabled = true;
            this.button_Auto_Manual.Text = "Auto";
            this.timer1.Enabled = false;
        }
        private void button_Auto_Manual_Click(object sender, EventArgs e)
        {
            if (timer1.Enabled == true)
            {
                manual();
            }
            else
            {
                auto();
            }
        }

        private void button_Inainte_Click(object sender, EventArgs e)
        {
            changeImg(1);
        }

        private void button_Inapoi_Click(object sender, EventArgs e)
        {
            changeImg(-1);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            changeImg(1);
        }

        public void changeImg(int plus_minus)
        {
            imgIndex += 1 * plus_minus;
            if (imgIndex == imagini.Count)
            {
                imgIndex = 0;
            }
            if (imgIndex < 0)
            {
                imgIndex = imagini.Count - 1;
            }
            string imgName = Application.StartupPath + @"\Resurse\imaginislideshow\" + imagini[imgIndex];
            this.pictureBox_slideshow.Image = Image.FromFile(imgName);
            this.progressBar1.Value = imgIndex + 1;
        }

        //-------------up data base------------------------------

        public void dbcheck()
        {
            que.dbccTabels();
        }

        public void sterge()
        {
            SqlCommand command = new SqlCommand();
            command.CommandText = "DELETE FROM Evaluari";
            command.Connection = conn.getConnection();

            conn.openConnection();
            command.ExecuteNonQuery();
            conn.closeConnection();

            command = new SqlCommand();
            command.CommandText = "DELETE FROM Itemi";
            command.Connection = conn.getConnection();

            conn.openConnection();
            command.ExecuteNonQuery();
            conn.closeConnection();

            command = new SqlCommand();
            command.CommandText = "DELETE FROM Utilizatori";
            command.Connection = conn.getConnection();

            conn.openConnection();
            command.ExecuteNonQuery();
            conn.closeConnection();
        }

        public void initializare()
        {
            SqlCommand command;
            string fn = Application.StartupPath + @"\Resurse\date.txt";
            StreamReader reader = new StreamReader(fn);
            string sir;
            string[] siruri;
            char split = ';';

            //utilizatori
            sir = reader.ReadLine();
            while ((sir = reader.ReadLine()) != null)
            {
                siruri = sir.Split(split);
                if (siruri.Length == 1)
                {
                    break;
                }
                command = new SqlCommand();
                command.CommandText = "INSERT INTO Utilizatori(NumePrenumeUtilizator,ParolaUtilizator,EmailUtilizator,ClasaUtilizator) VALUES(@nm,@pass,@em,@clas)";
                command.Connection = conn.getConnection();

                //@nm,@pass,@em,@clas
                command.Parameters.Add("nm", SqlDbType.VarChar).Value = siruri[0];
                command.Parameters.Add("pass", SqlDbType.VarChar).Value = siruri[1];
                command.Parameters.Add("em", SqlDbType.VarChar).Value = siruri[2];
                command.Parameters.Add("clas", SqlDbType.VarChar).Value = siruri[3];

                conn.openConnection();
                command.ExecuteNonQuery();
                conn.closeConnection();
            }

            //itemi
            //sir = reader.ReadLine();
            while ((sir = reader.ReadLine()) != null)
            {
                siruri = sir.Split(split);
                if (siruri.Length == 1)
                {
                    break;
                }
                command = new SqlCommand();
                command.CommandText = "INSERT INTO Itemi(TipItem,EnuntItem,Raspuns1Item,Raspuns2Item,Raspuns3Item,Raspuns4Item,RaspunsCorectItem) VALUES(@tip,@en,@r1,@r2,@r3,@r4,@rr) ";
                command.Connection = conn.getConnection();

                //@tip,@en,@r1,@r2,@r3,@r4,@rr
                command.Parameters.Add("tip", SqlDbType.Int).Value = int.Parse(siruri[0]);
                command.Parameters.Add("en", SqlDbType.VarChar).Value = siruri[1];
                command.Parameters.Add("r1", SqlDbType.VarChar).Value = siruri[2];
                command.Parameters.Add("r2", SqlDbType.VarChar).Value = siruri[3];
                command.Parameters.Add("r3", SqlDbType.VarChar).Value = siruri[4];
                command.Parameters.Add("r4", SqlDbType.VarChar).Value = siruri[5];
                command.Parameters.Add("rr", SqlDbType.VarChar).Value = siruri[6];

                conn.openConnection();
                command.ExecuteNonQuery();
                conn.closeConnection();
            }

            //utilizatori
            //sir = reader.ReadLine();
            while ((sir = reader.ReadLine()) != null)
            {
                siruri = sir.Split(split);
                if (siruri.Length == 1)
                {
                    break;
                }
                command = new SqlCommand();
                command.CommandText = "INSERT INTO Evaluari(IdElev,DataEvaluare,NotaEvaluare) VALUES(@id,@data,@nota)";
                command.Connection = conn.getConnection();

                //@id,@data,@nota
                command.Parameters.Add("id", SqlDbType.Int).Value = int.Parse(siruri[0]);
                command.Parameters.Add("data", SqlDbType.DateTime).Value = DateTime.Parse(siruri[1]);
                command.Parameters.Add("nota", SqlDbType.Int).Value = int.Parse(siruri[2]);

                conn.openConnection();
                command.ExecuteNonQuery();
                conn.closeConnection();
            }
        }
    }
}
