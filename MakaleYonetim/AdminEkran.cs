using System;
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
    public partial class AdminEkran : Form
    {
        public AdminEkran()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            YazarEkle y = new YazarEkle();
            y.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            object secilenid = listBox1.SelectedValue;
            if (listBox1.SelectedIndex < 1)
                MessageBox.Show("Bir yazar seçiniz.");
            else
            {
                DialogResult result = MessageBox.Show("Düzenlemek istediğinize emin misiniz?", "Düzenle",MessageBoxButtons.YesNo,MessageBoxIcon.Question);

                if(result == DialogResult.Yes)
                {
                    KullaniciDuzenle kd = new KullaniciDuzenle();
                    kd.gelenID = Convert.ToInt32(secilenid);
                    kd.Show();
                }
            }  
        }

        private void AdminEkran_Load(object sender, EventArgs e)
        {
            Data d = new Data();
            d.komut.CommandText = "SELECT AdSoyad,KullaniciID FROM tblKullanici WHERE SilindiMi=0";
            //DataTable dt = d.TabloGetir(); -- Bu burda yazarsak bütün tablo dolar.
            DataTable dt = new DataTable();
            dt.Columns.Add("AdSoyad");
            dt.Columns.Add("KullaniciID");

            DataRow dr = dt.NewRow();
            dr["AdSoyad"] = "Tüm Yazalar";
            dr["KullaniciID"] = 0;
            dt.Rows.Add(dr);

            DataTable dbdengelen = d.TabloGetir();
            foreach (DataRow item in dbdengelen.Rows)
            {
                dt.ImportRow(item);
            }

            listBox1.DisplayMember = "AdSoyad";
            listBox1.ValueMember = "KullaniciID";
            listBox1.DataSource = dt;  //En sonda olması gerekiyor. ValueMember dan önce yazılınca listbox ın selectedChanged tetiklendi.
          
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(listBox1.SelectedValue);
            string sqlSorgu = "SELECT MakaleID,Baslik,Tarih FROM tblMakale ";
            if (id != 0)
                sqlSorgu += "WHERE KullaniciID="+id;

            sqlSorgu += " ORDER BY MakaleID DESC";

            Data d = new Data();
            d.komut.CommandText = sqlSorgu;
            dataGridView1.DataSource = d.TabloGetir();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex < 1)
                MessageBox.Show("Bir yazar seçiniz.");
            else
            {
                DialogResult dialog = MessageBox.Show("Silmek istediğinize emin misiniz?", "Sil", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if(dialog == DialogResult.Yes)
                {
                    //Kullanıcı gerçekten silmek istiyor.
                    string id = listBox1.SelectedValue.ToString();
                    Data d = new Data();
                    d.komut.CommandText = "UPDATE tblKullanici SET SilindiMi=1 WHERE KullaniciID=" +id;
                    d.KomutCalistir();
                    MessageBox.Show("Silindi");
                    AdminEkran_Load(sender,e);
                }
            }                
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Istatistik i = new Istatistik();
            i.Show();
        }

        private void çıkışYapToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
            Form1 f = (Form1)Application.OpenForms["Form1"];
            f.Show();
        }
    
    }
}
