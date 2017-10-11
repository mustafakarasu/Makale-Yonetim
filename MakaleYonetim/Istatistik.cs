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
    public partial class Istatistik : Form
    {
        public Istatistik()
        {
            InitializeComponent();
        }

        private void Istatistik_Load(object sender, EventArgs e)
        {
            Data d = new Data();
            d.komut.CommandText = "SELECT COUNT(*) FROM tblMakale";
            lbl_makaleSayi.Text =  d.AlanGetir();

            d.komut.CommandText = "SELECT COUNT(*) FROM tblKullanici WHERE SilindiMi=0";
            lbl_yazarSayisi.Text = d.AlanGetir();

            lbl_kategoriSayisi.Text = d.AlanGetir("SELECT COUNT(*) FROM tblKategori");

            lbl_sonEklenen.Text = d.AlanGetir("SELECT TOP 1 Baslik FROM tblMakale ORDER BY Tarih DESC");

            lbl_enCok.Text = d.AlanGetir(@"
SELECT TOP 1 AdSoyad, COUNT(*) 
FROM tblKullanici k
INNER JOIN tblMakale m
ON k.KullaniciID = m.KullaniciID
GROUP BY AdSoyad
ORDER BY COUNT(*) DESC ");

            lbl_enAzMakale.Text = d.AlanGetir(@"
SELECT TOP 1 KategoriAdi, COUNT(*)
FROM tblKategori k
INNER JOIN tblMakale m
ON k.KategoriID = m.KategoriID
GROUP BY KategoriAdi
ORDER BY COUNT(*) ASC");
        }
    }
}
