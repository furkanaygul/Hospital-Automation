using HastaSiraOtomasyon.Entity;
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
    public partial class CagırmaEkranı : Form
    {

        public CagırmaEkranı()
        {
            InitializeComponent();
        }

        public void yaz(Muayene muayene)
        {
            label2.Text = muayene.Hasta.adi_soyadi;
            label3.Text = muayene.muayene_id.ToString();
            Thread.Sleep(2000);
            label2.Visible = false;
            label3.Visible = false;
            Thread.Sleep(500);
            label2.Visible = true;
            label3.Visible = true;
            Close();
        }
    }
}
