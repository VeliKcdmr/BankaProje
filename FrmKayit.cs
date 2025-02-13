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

namespace BankaProje
{
    public partial class FrmKayit : Form
    {
        public FrmKayit()
        {
            InitializeComponent();
        }
        SqlConnection baglanti = new SqlConnection("Data Source=RAMPAGE\\SQLEXPRESS;Initial Catalog=DbBanka;Integrated Security=True");
        // kayıt işlemi
        private void BtnKaydet_Click(object sender, EventArgs e)
        {
            try
            {
                baglanti.Open();
                SqlCommand komut = new SqlCommand("insert into TblKisiler (Ad,Soyad,Tc,Telefon,HesapNo,Sifre) values (@p1,@p2,@p3,@p4,@p5,@p6)", baglanti);
                SqlCommand komut2 = new SqlCommand("insert into TblHesap (HesapNo) values (@p5)", baglanti);
                komut.Parameters.AddWithValue("@p1", TxtAd.Text);
                komut.Parameters.AddWithValue("@p2", TxtSoyad.Text);
                komut.Parameters.AddWithValue("@p3", MTxtTc.Text);
                komut.Parameters.AddWithValue("@p4", MTxtTelefon.Text);
                komut.Parameters.AddWithValue("@p5", MTxtHesap.Text);
                komut.Parameters.AddWithValue("@p6", TxtSifre.Text);
                komut.ExecuteNonQuery();
                komut2.ExecuteNonQuery();
                baglanti.Close();
                MessageBox.Show("Hesap Oluşturuldu");
            }
            catch (Exception)
            {
                MessageBox.Show("Hesap Oluşturulamadı");
            }


        }
        // hesap numarası seçme
        private void BtnHesapSec_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("Select * from TblHesap where HesapNo=@p1", baglanti);
            komut.Parameters.AddWithValue("@p1", MTxtHesap.Text);
            SqlDataReader dr = komut.ExecuteReader();
            if (dr.Read())
            {
                MessageBox.Show("Hesap Numarası Kullanılmaktadır");
            }
            else
            {
                Random rnd = new Random();
                int sayi = rnd.Next(100000, 999999);
                MTxtHesap.Text = sayi.ToString();               
            }
            baglanti.Close();


        }
    }
}
