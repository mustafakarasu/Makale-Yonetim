﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MakaleYonetim
{
    public partial class MakaleDuzenle : Form
    {
        public MakaleDuzenle()
        {
            InitializeComponent();
        }

        public int MakaleID { get; set; }
        

        private void MakaleDuzenle_Load(object sender, EventArgs e)
        {
            lbl_yazar.Text = tblKullanici.GirisYapan.AdSoyad;

            Data d = new Data();
            d.komut.CommandText = "SELECT * FROM tblKategori";
            comboBox1.DataSource = d.TabloGetir();
            comboBox1.DisplayMember = "KategoriAdi";
            comboBox1.ValueMember = "KategoriID";

            d.komut.CommandText = "SELECT * FROM tblMakale WHERE MakaleID="+MakaleID; // makaleID satırını getirsin.
            DataRow dr = d.SatirGetir();  // Satır Getirmesi için

            textBox1.Text = dr["Baslik"].ToString();
            richTextBox1.Text = dr["Icerik"].ToString();
            comboBox1.SelectedValue = (int) dr["KategoriID"];
        }

        private void btn_duzenle_Click(object sender, EventArgs e)
        {
            Data d = new Data();
            d.komut.CommandText = "UPDATE tblMakale SET Baslik=@pbaslik, KategoriID=@pkid, Icerik=@picerik, KarakterSayisi=LEN(@picerik) WHERE MakaleID=@mid";
            d.komut.Parameters.AddWithValue("pbaslik",textBox1.Text);
            d.komut.Parameters.AddWithValue("pkid",comboBox1.SelectedValue);
            d.komut.Parameters.AddWithValue("picerik",richTextBox1.Text);
            d.komut.Parameters.AddWithValue("mid",MakaleID);
            d.KomutCalistir();
            MessageBox.Show("Makale Düzenlendi.");
            EditorEkran eform = (EditorEkran) Application.OpenForms["EditorEkran"];
            eform.EditorEkran_Load(sender,e);
        }
    }
}
