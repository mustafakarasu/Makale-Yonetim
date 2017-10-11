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
    public partial class YazarEkle : Form
    {
        public YazarEkle()
        {
            InitializeComponent();
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

            Data d = new Data();
            d.komut.CommandText = "SELECT * FROM tblKullanici WHERE Kadi=@pkadi";
            d.komut.Parameters.AddWithValue("pkadi", txt_Kadi.Text);
            if (d.TabloGetir().Rows.Count > 0)
                hatamsg += " \nKullanıcı adı daha önce alınmış.";
            if (!string.IsNullOrEmpty(hatamsg))
                MessageBox.Show(hatamsg);
            #endregion


            if (string.IsNullOrEmpty(hatamsg)) // HATA yoksa ekle.
            {
                Data de = new Data(); // Önceden d nesnesi old. ve parametre aldığından yeni nesne ürettik.
                de.komut.CommandText = @"
INSERT INTO tblKullanici 
(AdSoyad,Kadi,Sifre,Telefon,Eposta,YetkiGrupID) 
VALUES (@padSoyad,@pkadi,@psifre,@ptel,@peposta,@pyetkiID)";
                de.komut.Parameters.AddWithValue("padSoyad", txt_AdSoyad.Text);
                de.komut.Parameters.AddWithValue("pkadi", txt_Kadi.Text);
                de.komut.Parameters.AddWithValue("psifre", txt_Sifre.Text);
                de.komut.Parameters.AddWithValue("ptel", msk_Telefon.Text);
                de.komut.Parameters.AddWithValue("peposta", txt_Mail.Text);
                de.komut.Parameters.AddWithValue("pyetkiID", checkBox1.Checked ? 1 : 2);
                de.KomutCalistir();
                MessageBox.Show("Eklendi");
            }
        }

        private void YazarEkle_Load(object sender, EventArgs e)
        {
            msk_Telefon.Mask = "(999) 000 00 00";
            txt_Sifre.PasswordChar = '*';
            txt_SifreTekrar.PasswordChar = '*';
            txt_Kadi.MaxLength = 50; //Max. 50 karakter uzunluğunda
            txt_Sifre.MaxLength = 50;
            txt_SifreTekrar.MaxLength = 50;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // Guid.NewGuid() : Rastgele Şifre üretmek için kullanılır.
            string s = tblKullanici.SifreUret();
            
            txt_Sifre.Text = s;
            txt_SifreTekrar.Text = s;
        }
        private void button3_Click(object sender, EventArgs e)
        {
            if (txt_Sifre.PasswordChar == '*')
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
    }
}
