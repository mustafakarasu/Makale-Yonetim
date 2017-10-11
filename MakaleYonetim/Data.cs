﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MakaleYonetim
{
    class Data
    {
        public static string ServerName { get; set; }
        public static string Database { get; set; }
        public static string UserID { get; set; }
        public static string Password { get; set; }
        public static bool WindowsAuthentication { get; set; }
        public SqlCommand komut { get; set; }
                
        public SqlConnection con = new SqlConnection();

        //Data d = new Data(); // Ornek olarak

        public Data()
        {
            komut = new SqlCommand();
            string son = WindowsAuthentication ? "Integrated Security = true;" : "Password=" + Password;
            con.ConnectionString = string.Format("Server={0};Database={1};User ID = {2}; {3};", ServerName, Database, UserID, son);
            komut.Connection = con; //tek satır yerine. "",con
        }
        public DataTable TabloGetir()
        {
            SqlDataAdapter sda = new SqlDataAdapter(komut);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            return dt;
        }
        public DataRow SatirGetir()
        {
            try
            {
                return TabloGetir().Rows[0];
            }
            catch
            {
                return null;
            }
        }
        public string AlanGetir()
        {
            try
            {
                return SatirGetir()[0].ToString();
            }
            catch
            {
                return null;
            }
        }

        public string AlanGetir(string sorgu)
        {
            this.komut.CommandText = sorgu;
            try
            {
                return SatirGetir()[0].ToString();
            }
            catch
            {
                return null;
            }
        }
        public int KomutCalistir() //Ekleme,silme,günceleme gibi
        {
            con.Open();
            int sayi = komut.ExecuteNonQuery();
            con.Close();
            return sayi;   //jj.return gördüğü anda bloktan çıkar.
        }
    }
}
