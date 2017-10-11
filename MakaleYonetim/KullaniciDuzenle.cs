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
    public partial class KullaniciDuzenle : Form
    {
        public int gelenID { get; set; }

        public KullaniciDuzenle()
        {
            InitializeComponent();
        }
       
        private void KullaniciDuzenle_Load(object sender, EventArgs e)
        {
            Data d = new Data();
            d.komut.CommandText = "SELECT * FROM tblKullanici WHERE KullaniciID=" + gelenID;
            DataRow dr = d.SatirGetir(); // gelenID den gelen veriler dr satırında

            txt_AdSoyad.Text = dr["AdSoyad"].ToString();
            txt_Kadi.Text = dr["Kadi"].ToString();
            txt_Sifre.Text = dr["Sifre"].ToString();
            txt_SifreTekrar.Text= dr["Sifre"].ToString();
            msk_Telefon.Text = dr["Telefon"].ToString();
            txt_Mail.Text = dr["Eposta"].ToString();

            //checkBox1.Checked = true;
            checkBox1.Checked = (int)dr["YetkiGrupID"] == 1;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if(txt_Sifre.PasswordChar == '*')
            {
                txt_Sifre.PasswordChar = '\0';
                txt_SifreTekrar.PasswordChar = '\0';
            }
            else
            {
                txt_Sifre.PasswordChar = '*';
                txt_SifreTekrar.PasswordChar = '*';
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            string sifre = tblKullanici.SifreUret();
            txt_Sifre.Text = sifre;
            txt_SifreTekrar.Text = sifre;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            tblKullanici.Validasyonlar(this);

            #region Hatalar
            string hatamsg = "";

            if (txt_Sifre.Text != txt_SifreTekrar.Text)
                hatamsg += "Şifreler eşleşmiyor";

            if (!txt_Mail.Text.Contains("@"))
                hatamsg += " \nEmail geçerli değil";

            #endregion

            if (!string.IsNullOrEmpty(hatamsg))
                MessageBox.Show(hatamsg);
            else
            {
                #region Kayit

                Data d = new Data();
                d.komut.CommandText = @"
UPDATE tblKullanici 
SET AdSoyad=@padSoyad, Kadi=@pkadi, Sifre=@psifre, Telefon=@ptel, Eposta=@peposta, YetkiGrupID=@pyetkiID
WHERE KullaniciID=" + gelenID;

                d.komut.Parameters.AddWithValue("padSoyad", txt_AdSoyad.Text);
                d.komut.Parameters.AddWithValue("pkadi", txt_Kadi.Text);
                d.komut.Parameters.AddWithValue("psifre", txt_Sifre.Text);
                d.komut.Parameters.AddWithValue("ptel", msk_Telefon.Text);
                d.komut.Parameters.AddWithValue("peposta", txt_Mail.Text);
                d.komut.Parameters.AddWithValue("pyetkiID", checkBox1.Checked ? 1 : 2);
                d.KomutCalistir();
                MessageBox.Show("Güncellendi.");

                #endregion
            }

        }
    }
}
