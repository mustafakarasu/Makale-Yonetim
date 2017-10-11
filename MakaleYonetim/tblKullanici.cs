using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MakaleYonetim
{
    class tblKullanici // tblKullanici (model)
    {
        #region Model
        public static tblKullanici GirisYapan { get ; set;}

        public int KullaniciID { get; set; }
        public string Kadi { get; set; }
        public string Sifre { get; set; }
        public string AdSoyad { get; set; }
        public int YetkiGrupID { get; set; }
        public string Telefon { get; set; }
        public string Eposta { get; set; }
        #endregion

        //Business: İş yapan kısım
        #region Business
        //6 haneli bir şifre üreten metodu yazın.

        public static string SifreUret()
        {
            string sifre = Guid.NewGuid().ToString().Substring(0, 6);
            return sifre;
        }

        public static void Validasyonlar(Form gelenForm) // 18.08.Hangi form olursa onun kontrolleri kontrol edilecek.
        {
            #region Textbox_Renk

            foreach (var item in gelenForm.Controls) //var tipinde bırak. Çeşitli kontroller var.
            {
                if (item is TextBox)  // gelen item textboxt sa
                {
                    //item. deyince textbox ın özellikleri gelmiyor. Belirtmek gerekir.
                    TextBox t = (TextBox)item;
                    if (string.IsNullOrEmpty(t.Text))
                        t.BackColor = Color.Red;
                    else
                        t.BackColor = Color.White;
                }
                else if (item is MaskedTextBox)
                {
                    MaskedTextBox m = (MaskedTextBox)item;
                    if (string.IsNullOrEmpty(m.Text.Replace(" ", "").Replace("(", "").Replace(")", "")))
                        // ? KontrolEt: m.Mask.Length: maskın uzunluğu içindeki text den kısa veya eş
                        m.BackColor = Color.Red;
                    else
                        m.BackColor = Color.White;
                }
            }

            #endregion
        }

        #endregion  
    }
}
