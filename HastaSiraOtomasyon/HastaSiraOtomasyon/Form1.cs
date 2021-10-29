using HastaSiraOtomasyon.Entity;
using Npgsql;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HastaSiraOtomasyon
{
    public partial class Form1 : Form
    {

        public Form1()
        {
            InitializeComponent();
        }
        NpgsqlConnection baglanti = new NpgsqlConnection("server=localHost; port=5432; Database=DBHastane; user ID=postgres; password=12345");
        Context db = new Context();
        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                Doktor_Ekran doktor_Ekran = new Doktor_Ekran();
                doktor_Ekran.Show();
                B_Hasta_timer_Tick(sender, e);
                baglanti.Open();
                string komut = @"select* from dbo.""Doktors""";
                NpgsqlDataAdapter dataAdapter = new NpgsqlDataAdapter(komut, baglanti);
                DataTable dataTable = new DataTable();
                dataAdapter.Fill(dataTable);
                doktorlar_cbx.DisplayMember = "adi_soyadi";
                doktorlar_cbx.ValueMember = "id";
                doktorlar_cbx.DataSource = dataTable;
                baglanti.Close();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        private void B_Hasta_timer_Tick(object sender, EventArgs e)
        {
            string str = @"select dbo.""Muayenes"".muayene_id as ""SIRA NO"",h.adi_soyadi as ""AD SOYAD"" ,d.adi_soyadi as ""DOKTOR"", dbo.""Muayenes"".saati as ""R.SAATI"" from dbo.""Muayenes"" INNER JOIN dbo.""Hastas"" h ON dbo.""Muayenes"".""Hasta_tc"" = h.tc INNER JOIN dbo.""Doktors"" d ON dbo.""Muayenes"".""Doktor_id"" = d.id  Where dbo.""Muayenes"".muayene_durumu='false'";
            //string str = @" select * from dbo.""Muayenes""";
            NpgsqlCommand komut = new NpgsqlCommand(str, baglanti);
            NpgsqlDataAdapter dataAdapter = new NpgsqlDataAdapter(komut);
            DataSet dataSet = new DataSet();
            dataAdapter.Fill(dataSet);
            dataGridView1.DataSource = dataSet.Tables[0];
        }

      

        private void btn_hasta_olustur_Click(object sender, EventArgs e)
        {

            try
            {
                Hasta hasta = new Hasta();
                hasta.adi_soyadi = hasta_ad_tbx.Text;
                hasta.tc = Int64.Parse(tc_tbx.Text);
                if (openFileDialog1.FileName != "openFileDialog1")
                {

                    string result = DateTime.Now.ToString("yyyyMMddHHmmssfff");
                    result += Path.GetExtension(openFileDialog1.FileName);
                    File.Copy(pictureBox3.ImageLocation, Path.Combine(@"C:\Users\FURKAN\source\repos\HastaSiraOtomasyon\HastaSiraOtomasyon\ImgResources\", result), true);
                    hasta.resmi = @"C:\Users\FURKAN\source\repos\HastaSiraOtomasyon\HastaSiraOtomasyon\ImgResources\" + result;

                }
                else
                {
                    hasta.resmi = @"C:\Users\FURKAN\source\repos\HastaSiraOtomasyon\HastaSiraOtomasyon\ImgResources\users.png";
                }
               
                db.Hastas.Add(hasta);
                db.SaveChanges();


                durum_lbl.Visible = true;
                durum_lbl.ForeColor = Color.Green;
                durum_lbl.Text = "Kayit islemi basarili";
                MessageBox.Show("Kayit islemi basarili");
            }
            catch (Exception ex)
            {
                
                durum_lbl.Visible = true;
                durum_lbl.ForeColor = Color.Red;
                durum_lbl.Text = "Kayit islemi basarisiz";
                MessageBox.Show(ex.Message);
            }


            //baglanti.Open();
            //string komut = @"insert into dbo.""Hastas""(adi_soyadi,tc) values (@p1,@p2)";
            //NpgsqlCommand command = new NpgsqlCommand(komut, baglanti);
            //command.Parameters.AddWithValue("@p1", hasta_ad_tbx.Text);
            //command.Parameters.AddWithValue("@p2", tc_tbx.Text);
            //command.ExecuteNonQuery();
            //durum_lbl.Text = "Basariyla Kaydedildi";
            //baglanti.Close();
        }

        private void btn_hasta_kayit_Click(object sender, EventArgs e)
        {
            Muayene muayene = new Muayene();
             muayene.Doktor=db.Doktors.Find(doktorlar_cbx.SelectedValue);
            muayene.Hasta = db.Hastas.Find(Int64.Parse(textBox1.Text));
            muayene.muayene_durumu = false;
            muayene.saati = DateTime.Now.ToString("h:mm:ss tt");
            db.Muayenes.Add(muayene);
            db.SaveChanges();
            B_Hasta_timer_Tick(sender, e);

        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                var id = dataGridView1.CurrentRow.Cells[0].Value.ToString();
                var secilen = db.Muayenes.Find(int.Parse(id));
                DialogResult dr = new DialogResult();
                dr = MessageBox.Show("Silmek istediğinize emin misiniz?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (dr == DialogResult.Yes)
                {
                    db.Muayenes.Remove(secilen);
                    db.SaveChanges();
                    B_Hasta_timer_Tick(sender, e);
                }
               
                
            }
            catch (Exception ex)
            {

            }
        }

        private void rsm_ekl_btn_Click(object sender, EventArgs e)
        {
            openFileDialog1.Title = "Resim Sec";
            openFileDialog1.Filter = "Image Files (*.bmp;*.jpg;*.jpeg,*.png)|*.BMP;*.JPG;*.JPEG;*.PNG";
            if (openFileDialog1.ShowDialog()==DialogResult.OK)
            {
                pictureBox3.ImageLocation = openFileDialog1.FileName;
            }
        }
        private bool dragging = false;
        private Point dragCursorPoint;
        private Point dragFormPoint;
        private void Form1_MouseUp(object sender, MouseEventArgs e)
        {
            dragging = false;
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            if (dragging)
            {
                Point dif = Point.Subtract(Cursor.Position, new Size(dragCursorPoint));
                this.Location = Point.Add(dragFormPoint, new Size(dif));
            }
        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            dragging = true;
            dragCursorPoint = Cursor.Position;
            dragFormPoint = this.Location;
        }
        //public void durum_success()
        //{
        //    durum_lbl.Visible = true;
        //    durum_lbl.ForeColor = Color.Green;
        //    durum_lbl.Text = "Kayit islemi basarili";
        //    System.Threading.Thread.Sleep(3000);
        //    durum_lbl.Visible = false;
        //}
    }
}
