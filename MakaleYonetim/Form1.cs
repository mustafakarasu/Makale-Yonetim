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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        
        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox1.Text))
                textBox1.BackColor = Color.Red;
            else
                textBox1.BackColor = Color.White;

            if (string.IsNullOrEmpty(textBox2.Text))
                textBox2.BackColor = Color.Red;
            else
                textBox2.BackColor = Color.White;

            Data d = new Data();
            //d.komut.CommandText = string.Format("SELECT * FROM tblKullanici WHERE Kadi='{0}' AND Sifre='{1}'", textBox1.Text,textBox2.Text);

            d.komut.CommandText = "SELECT * FROM tblKullanici WHERE Kadi=@k AND Sifre=@s";
            d.komut.Parameters.AddWithValue("k",textBox1.Text);
            d.komut.Parameters.AddWithValue("s",textBox2.Text);

            DataTable dt = d.TabloGetir();

            if (dt.Rows.Count > 0)
            {
                DataRow dr = dt.Rows[0];
                tblKullanici.GirisYapan = new tblKullanici();
                tblKullanici.GirisYapan.KullaniciID = (int)dr["KullaniciID"];
                tblKullanici.GirisYapan.AdSoyad = dr["AdSoyad"].ToString();

                string yetki = dt.Rows[0]["YetkiGrupID"].ToString();
                //dataRow ürettikten sonra dt.Rows[0] yazmaya gerek yok. dr yazılabilir.
                if (yetki == "1")
                    new AdminEkran().Show();
                else
                    new EditorEkran().Show();

                this.Hide();             
            }
            else
            {
                MessageBox.Show("Kullanıcı Adı ya da Şifre yanlış.");
            }
                
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void EnteraBasilincaButonaTikla(object sender, KeyEventArgs e)
        {
            //Klavyeden girilen olması için KeyEventArgs oldu.
            if (e.KeyValue == 13)
                button1.PerformClick(); //butona tıkla
        }
    }
}
