using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MakaleYonetim
{
    public partial class EditorEkran : Form
    {
        public EditorEkran()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MakaleEkle m = new MakaleEkle();
            m.Show();
        }
        string gridSorgu = ""; // Heryerden ulaşmak için global yaptık. Load da çalıştıktan sonra sonuna ekleme yapılacak.
        public void EditorEkran_Load(object sender, EventArgs e)
        {
            gridSorgu= "SELECT MakaleID,Baslik,k.KategoriAdi,KarakterSayisi AS Karakter,Tarih FROM tblMakale m INNER JOIN tblKategori k ON m.KategoriID = k.KategoriID";
            
            Data d = new Data();
            d.komut.CommandText = gridSorgu;
            dataGridView1.DataSource = d.TabloGetir();

            d.komut.CommandText = "SELECT * FROM tblKategori";
            DataTable dt = d.TabloGetir();  //Ara eleman.Müdahale etmek için
            DataRow satir = dt.NewRow(); //En üste Tüm Kategoriler seçeneği olsun.
            satir["KategoriID"] = 0; // En başa
            satir["KategoriAdi"] = "Tüm Kategoriler";
            dt.Rows.Add(satir);

            comboBox1.DisplayMember = "KategoriAdi";
            comboBox1.ValueMember = "KategoriID";
            comboBox1.SelectedValue = 0;
            comboBox1.DataSource = dt;
            comboBox1.SelectedValue = 0;

        }

        private void button2_Click(object sender, EventArgs e)
        {
            object id = dataGridView1.SelectedRows[0].Cells["MakaleID"].Value;
            Data d = new Data();
            d.komut.CommandText = "DELETE FROM tblMakale WHERE MakaleID=@mid";
            d.komut.Parameters.AddWithValue("mid",id);
            d.KomutCalistir();
            MessageBox.Show("Silindi.");
            EditorEkran_Load(sender,e);
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            Data d = new Data();
            if ((int) comboBox1.SelectedValue == 0) //Kendi eklediğimiz satır. 
            {
                d.komut.CommandText = gridSorgu; // Tüm kategoriler seçeneği çalışsın.
            }
            else // sql deki
            {
                d.komut.CommandText = gridSorgu + " WHERE m.KategoriID=@kid";
                d.komut.Parameters.AddWithValue("kid",comboBox1.SelectedValue);
            }

            dataGridView1.DataSource = d.TabloGetir(); // ikisi içinde geçerli o yüzden parantezler dışında.
        }

        private void button3_Click(object sender, EventArgs e)
        {
            object makaleid = dataGridView1.SelectedRows[0].Cells["MakaleID"].Value;

            MakaleDuzenle duzenle = new MakaleDuzenle();
            duzenle.MakaleID = Convert.ToInt32(makaleid);
            duzenle.Show();
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            string sorgu = gridSorgu;
            if (comboBox2.SelectedItem == "Eskiden Yeniye")  //Items olarak tanımladık.
                sorgu += " ORDER BY Tarih ASC";
            else
                sorgu += " ORDER BY Tarih DESC";

            Data d = new Data();
            d.komut.CommandText = sorgu;
            dataGridView1.DataSource = d.TabloGetir();

        }

        private void çıkışYapToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
            Form1 f = (Form1)Application.OpenForms["Form1"];
            f.Show();
        }
    }
}
