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
    public partial class eLearning1918_Elev : Form
    {
        public eLearning1918_Elev()
        {
            InitializeComponent();
        }

        ITEMI item = new ITEMI();
        DataTable[] table = new DataTable[10];
        Random rand = new Random();
        EVALUARI eval = new EVALUARI();
        int[] valoriValide = new int[100];
        int[] tipIntrebare = new int[5];
        int sIntrebari, nrOrd;
        struct Raport
        {
            public int nrOrdine, Tip;
            public string enunt, raspunsElev, raspunsCorect;
        }
        Raport[] raport;
        private void eLearning1918_Elev_Load(object sender, EventArgs e)
        {
            raport = new Raport[10];
        }

        private void button_Start_Click(object sender, EventArgs e)
        {
            this.richTextBox1.Visible = false;
            this.richTextBox1.ResetText();
            this.label_pct.Text = "1";
            this.label_tip.Text = "";
            this.button_Raspund.Visible = true;
            init_questions();
            nrOrd = 1;
            load_question();
        }

        public void load_question()
        {
            this.label_tip.Text = table[nrOrd].Rows[0][1].ToString();
            this.textBox_Enunt.Text = table[nrOrd].Rows[0][2].ToString();
            if (int.Parse(table[nrOrd].Rows[0][1].ToString()) == 1)
            {
                load_tip1();
            }
            else if (int.Parse(table[nrOrd].Rows[0][1].ToString()) == 2)
            {
                load_tip2();
            }
            else if (int.Parse(table[nrOrd].Rows[0][1].ToString()) == 3)
            {
                load_tip3();
            }
            else if (int.Parse(table[nrOrd].Rows[0][1].ToString()) == 4)
            {
                load_tip4();
            }
        }

        public void load_tip1()
        {
            this.panel_tip1.Visible = true;
            this.panel_tip2.Visible = false;
            this.panel_tip3.Visible = false;
            this.panel_tip4.Visible = false;
            this.panel_tip1.Location = new Point(9, 190);
            this.panel_tip1.Size = new Size(1038, 255);
        }
        public void load_tip2()
        {
            this.panel_tip1.Visible = false;
            this.panel_tip2.Visible = true;
            this.panel_tip3.Visible = false;
            this.panel_tip4.Visible = false;
            this.panel_tip2.Location = new Point(9, 190);
            this.panel_tip2.Size = new Size(1038, 255);
            this.radioButton1.Text = table[nrOrd].Rows[0][3].ToString();
            this.radioButton2.Text = table[nrOrd].Rows[0][4].ToString();
            this.radioButton3.Text = table[nrOrd].Rows[0][5].ToString();
            this.radioButton4.Text = table[nrOrd].Rows[0][6].ToString();
        }
        public void load_tip3()
        {
            this.panel_tip1.Visible = false;
            this.panel_tip2.Visible = false;
            this.panel_tip3.Visible = true;
            this.panel_tip4.Visible = false;
            this.panel_tip3.Location = new Point(9, 190);
            this.panel_tip3.Size = new Size(1038, 255);
            this.checkBox1.Text = table[nrOrd].Rows[0][3].ToString();
            this.checkBox2.Text = table[nrOrd].Rows[0][4].ToString();
            this.checkBox3.Text = table[nrOrd].Rows[0][5].ToString();
            this.checkBox4.Text = table[nrOrd].Rows[0][6].ToString();
        }
        public void load_tip4()
        {
            this.panel_tip1.Visible = false;
            this.panel_tip2.Visible = false;
            this.panel_tip3.Visible = false;
            this.panel_tip4.Visible = true;
            this.panel_tip4.Location = new Point(9, 190);
            this.panel_tip4.Size = new Size(1038, 255);
        }


        private void button1_Click(object sender, EventArgs e)
        {
            nrOrd++;
            if (nrOrd >= 10)
            {
                //8, 52
                this.richTextBox1.Visible = true;
                this.richTextBox1.Location = new Point(8, 45);
                this.richTextBox1.Size = new Size(800, 370);
                for (int i = 1; i <= 9; i++)
                {
                    string text = raport[i].nrOrdine.ToString() + "; ";
                    text = text + raport[i].Tip.ToString() + "; ";
                    text = text + raport[i].enunt.ToString() + "; ";
                    text = text + raport[i].raspunsElev.ToString() + "; ";
                    text = text + raport[i].raspunsCorect.ToString() + ";\n\n";
                    this.richTextBox1.AppendText(text);
                }

                DateTime dateTest = DateTime.Now;
                int nota = int.Parse(this.label_pct.Text);
                int userid = GLOBAL.idGLOBAL;
              //  EVALUARI eval = new EVALUARI();
                eval.insertEvaluare(userid, nota, dateTest);
                return;
            }
            load_question();
        }

        private void button_Raspund_Click(object sender, EventArgs e)
        {
            bool raspuns = true;
            raport[nrOrd].nrOrdine = nrOrd;
            raport[nrOrd].enunt = this.textBox_Enunt.Text;
            if (int.Parse(table[nrOrd].Rows[0][1].ToString()) == 1)
            {
                raspuns = r_tip1();
            }
            else if (int.Parse(table[nrOrd].Rows[0][1].ToString()) == 2)
            {
                raspuns = r_tip2();
            }
            else if (int.Parse(table[nrOrd].Rows[0][1].ToString()) == 3)
            {
                raspuns = r_tip3();
            }
            else if (int.Parse(table[nrOrd].Rows[0][1].ToString()) == 4)
            {
                raspuns = r_tip4();
            }
            if (raspuns == true)
            {
                MessageBox.Show("Corect");
                int n = int.Parse(this.label_pct.Text);
                n++;
                this.label_pct.Text = n.ToString();
            }
            else
            {
                MessageBox.Show("Gresit");
            }
        }

        public bool r_tip1()
        {
            raport[nrOrd].raspunsElev = this.textBox_Raspuns.Text;
            raport[nrOrd].Tip = 1;
            raport[nrOrd].raspunsCorect = table[nrOrd].Rows[0][7].ToString();
            if (raport[nrOrd].raspunsCorect.ToLower().Trim() == this.textBox_Raspuns.Text.ToLower().Trim())
            {
                return true;
            }
            return false;
        }
        public bool r_tip2()
        {
            raport[nrOrd].Tip = 2;
            raport[nrOrd].raspunsCorect = table[nrOrd].Rows[0][7].ToString();
            int rc = int.Parse(raport[nrOrd].raspunsCorect);
            if (this.radioButton1.Checked == true)
            {
                raport[nrOrd].raspunsElev = "1";
                if (rc == 1)
                {
                    return true;
                }
                return false;
            }
            else if (this.radioButton2.Checked == true)
            {
                raport[nrOrd].raspunsElev = "2";
                if (rc == 2)
                {
                    return true;
                }
                return false;
            }
            else if (this.radioButton3.Checked == true)
            {
                raport[nrOrd].raspunsElev = "3";
                if (rc == 3)
                {
                    return true;
                }
                return false;
            }
            else if (this.radioButton4.Checked == true)
            {
                raport[nrOrd].raspunsElev = "4";
                if (rc == 4)
                {
                    return true;
                }
                return false;
            }
            return true;
        }
        public bool r_tip3()
        {
            raport[nrOrd].raspunsCorect = table[nrOrd].Rows[0][7].ToString();
            raport[nrOrd].Tip = 3;
            int r = 0;
            if (this.checkBox1.Checked == true)
            {
                r = r * 10 + 1;
            }
            if (this.checkBox2.Checked == true)
            {
                r = r * 10 + 2;
            }
            if (this.checkBox3.Checked == true)
            {
                r = r * 10 + 3;
            }
            if (this.checkBox4.Checked == true)
            {
                r = r * 10 + 4;
            }
            raport[nrOrd].raspunsElev = r.ToString();
            if (raport[nrOrd].raspunsCorect == r.ToString())
            {
                return true;
            }
            return false;
        }
        public bool r_tip4()
        {
            raport[nrOrd].Tip = 4;
            raport[nrOrd].raspunsCorect = table[nrOrd].Rows[0][7].ToString();
            if (this.radioButton_true.Checked == true)
            {
                raport[nrOrd].raspunsElev = "1";
                if (raport[nrOrd].raspunsCorect == "1")
                {
                    return true;
                }
                return false;
            }
            if (this.radioButton_false.Checked == true)
            {
                raport[nrOrd].raspunsElev = "0";
                if (raport[nrOrd].raspunsCorect == "0")
                {
                    return true;
                }
                return false;
            }
            return true;
        }

        private void graficNoteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.tabControl1.SelectedTab = tabPage_Grafic;
            made_grafic();
        }

        private void carnetDeNoteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.tabControl1.SelectedTab = tabPage_Carnet;
            made_carnet();
        }

        private void testeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.tabControl1.SelectedTab = tabPage_Teste;
        }

        private void tabControl1_Selected(object sender, TabControlEventArgs e)
        {

        }

        public void init_questions()
        {

            for (int i = 1; i <= 4; i++)
            {
                tipIntrebare[i] = i;
            }
            int rid;
            sIntrebari = 0;
            nrOrd = 1;
            for (int i = 1; i <= 4; i++)
            {
                int nr = item.ItemCount(tipIntrebare[i]);
                for (int j = sIntrebari + 1; j <= sIntrebari + nr; j++)
                {
                    valoriValide[j] = 1;
                }
                rid = rand.Next(sIntrebari + 1, sIntrebari + nr + 1);
                table[nrOrd] = item.ItemById(rid);
                valoriValide[rid] = 0;
                nrOrd++;
                sIntrebari += nr;
            }
            while (nrOrd < 10)
            {
                do
                {
                    rid = rand.Next(1, sIntrebari + 1);
                } while (valoriValide[rid] == 0);
                table[nrOrd] = item.ItemById(rid);
                valoriValide[rid] = 0;
                nrOrd++;
            }
        }
        public void made_grafic()
        {
            Graphics graphics;
            graphics = panel_Grafic.CreateGraphics();
            Pen pen = new Pen(Color.Black);
            pen.Width = 2;
            Pen penMedie = new Pen(Color.BlueViolet);
            penMedie.Width = 4;
            Pen penGraf = new Pen(Color.Red);
            penGraf.Width = 6;
            int panelSizeX = this.panel_Grafic.ClientSize.Width;
            int panelSizeY = this.panel_Grafic.ClientSize.Height;
            float unitateX = (float)panelSizeX / 10;
            float unitateY = (float)panelSizeY / 10;
            // Point[] note = new Point[11];
            for (int i = 1; i <= 10; i++)
            {
                //   note[i].X = unitateX;
                // note[i].Y = unitateY * (i - 1);
                for (int j = 1; j <= 10; j++)
                {
                    graphics.DrawLine(pen, unitateX * (j - 1), unitateY * (i - 1), unitateX * j, unitateY * (i - 1));
                    graphics.DrawLine(pen, unitateX * j, unitateY * (i - 1), unitateX * j, unitateY * i);
                }
            }

            DataTable tableNote = new DataTable();
            tableNote = eval.getEvaluareById(GLOBAL.idGLOBAL);
            int nNote = tableNote.Rows.Count;
            int sNote = 0;
            for (int i = 0; i < nNote; i++)
            {
                sNote += int.Parse(tableNote.Rows[i][0].ToString());
            }
            float pctmedia = (10 - sNote / nNote) * unitateY;
            graphics.DrawLine(penMedie, 0, pctmedia, this.panel_Grafic.ClientSize.Width, pctmedia);
            //MessageBox.Show(pctmedia.ToString());
            for (int i = 1; i < nNote; i++)
            {
                int nota1 = int.Parse(tableNote.Rows[i - 1][0].ToString());
                int nota2 = int.Parse(tableNote.Rows[i][0].ToString());
                graphics.DrawLine(penGraf, i * unitateX, (10 - nota1) * unitateY, (i + 1) * unitateX, (10 - nota2) * unitateY);
            }
        }

        private void button_print_Click(object sender, EventArgs e)
        {
            this.printPreviewDialog1.Document = printDocument1;
            //Get the document
            if (DialogResult.OK == this.printPreviewDialog1.ShowDialog())
            {
                printDocument1.DocumentName = "Test Page Print";
                printDocument1.Print();
            }
        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            Bitmap bm = new Bitmap(this.dataGridView1.Width, this.dataGridView1.Height);
            this.dataGridView1.DrawToBitmap(bm, new Rectangle(0, 0, this.dataGridView1.Width, this.dataGridView1.Height));
            e.Graphics.DrawImage(bm, 0, 0);
        }

        private void iesireToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public void made_carnet()
        {
            this.label_carnetNote.Text = "Carnet de note al elevului " + eval.getEvaluareNume(GLOBAL.idGLOBAL);
            this.dataGridView1.DataSource = eval.getEvaluareDataNoteById(GLOBAL.idGLOBAL);
        }
    }  
}
