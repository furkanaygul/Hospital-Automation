using HastaSiraOtomasyon.Entity;
using Npgsql;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HastaSiraOtomasyon
{
    public partial class Doktor_Ekran : Form
    {
        Context db = new Context();
        NpgsqlConnection baglanti = new NpgsqlConnection("server=localHost; port=5432; Database=DBHastane; user ID=postgres; password=12345");

        public Doktor_Ekran()
        {
            InitializeComponent();
        }

        private void Doktor_Ekran_Load(object sender, EventArgs e)
        {
            tablo();
            groupBox1.Enabled = false;

        }

        public void tablo()
        {
            dataGridView1.Visible = true;
            string str = @"select dbo.""Muayenes"".muayene_id as ""SIRA NO"",h.adi_soyadi as ""AD SOYAD"" ,d.adi_soyadi as ""DOKTOR"", dbo.""Muayenes"".saati as ""R.SAATI"" from dbo.""Muayenes"" INNER JOIN dbo.""Hastas"" h ON dbo.""Muayenes"".""Hasta_tc"" = h.tc INNER JOIN dbo.""Doktors"" d ON dbo.""Muayenes"".""Doktor_id"" = d.id  Where dbo.""Muayenes"".muayene_durumu='false' and d.adi_soyadi='Furkan Aygul'";
            //string str = @" select * from dbo.""Muayenes""";
            NpgsqlCommand komut = new NpgsqlCommand(str, baglanti);
            NpgsqlDataAdapter dataAdapter = new NpgsqlDataAdapter(komut);
            DataSet dataSet = new DataSet();
            dataAdapter.Fill(dataSet);
            dataGridView1.DataSource = dataSet.Tables[0];
        }
        public void ilaclar()
        {
            baglanti.Open();
            string komut = @"select i.ilac_id as ""ID"",i.ilac_adi as ""ADI"" from dbo.""ilaclars"" i where muayene_id=@p1";
            NpgsqlCommand command = new NpgsqlCommand(komut, baglanti);
            command.Parameters.AddWithValue("@p1",int.Parse(label6.Text));
            NpgsqlDataAdapter dataAdapter = new NpgsqlDataAdapter(command);
            DataSet dataSet = new DataSet();
            dataAdapter.Fill(dataSet);
            dataGridView2.DataSource = dataSet.Tables[0];
            baglanti.Close();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            tablo();
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                var id = dataGridView1.CurrentRow.Cells[0].Value;
                var secilen = db.Muayenes.Find(id);
                DialogResult muayene = new DialogResult();
                muayene = MessageBox.Show("Hasta Cagirilsin mi ?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (muayene == DialogResult.Yes)
                {
                    label2.Text = secilen.Hasta.adi_soyadi;
                    label4.Text = secilen.Hasta.tc.ToString();
                    label6.Text = secilen.muayene_id.ToString();
                    pictureBox1.ImageLocation = secilen.Hasta.resmi;
                    groupBox1.Enabled = true;
                    //CagırmaEkranı cagırmaEkranı = new CagırmaEkranı();
                    //cagırmaEkranı.yaz(secilen);
                    //cagırmaEkranı.Show();
                }


            }
            catch (Exception ex)
            {

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ilaclar ilac = new ilaclar();
            ilac.ilac_adi = textBox1.Text;
            ilac.kullanim_zamani = textBox2.Text;
            ilac.muayene_id = int.Parse(label6.Text);
            db.Ilaclars.Add(ilac);
            db.SaveChanges();
            ilaclar();
        }

        private void dataGridView2_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            
                var id = dataGridView2.CurrentRow.Cells[0].Value;
                var secilen = db.Ilaclars.Find(id);
                DialogResult i = new DialogResult();
                i = MessageBox.Show("Cikarmak Istediginize emin misiniz ?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (i == DialogResult.Yes)
                {
                    db.Ilaclars.Remove(secilen);
                    db.SaveChanges();
                    ilaclar();
                }


            
        }

        private void dataGridView2_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            this.dataGridView2.Columns[0].Visible = false;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var secilen = db.Muayenes.Find(int.Parse(label6.Text));
            DialogResult i = new DialogResult();
            i = MessageBox.Show("Muayene bitirilsin mi?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (i == DialogResult.Yes)
            {
                secilen.muayene_durumu = true;
                db.SaveChanges();
                ilaclar();
                groupBox1.Enabled = false;
                dataGridView2.DataSource = null;
                textBox1.Text = null;
                textBox2.Text = null;
            }
        }

        private bool dragging = false;
        private Point dragCursorPoint;
        private Point dragFormPoint;

        private void Doktor_Ekran_MouseDown(object sender, MouseEventArgs e)
        {
            dragging = true;
            dragCursorPoint = Cursor.Position;
            dragFormPoint = this.Location;
        }

        private void Doktor_Ekran_MouseMove(object sender, MouseEventArgs e)
        {
            if (dragging)
            {
                Point dif = Point.Subtract(Cursor.Position, new Size(dragCursorPoint));
                this.Location = Point.Add(dragFormPoint, new Size(dif));
            }
        }

        private void Doktor_Ekran_MouseUp(object sender, MouseEventArgs e)
        {
            dragging = false;
        }
    }
}
