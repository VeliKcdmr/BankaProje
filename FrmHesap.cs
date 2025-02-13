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
    public partial class FrmHesap : Form
    {
        public FrmHesap()
        {
            InitializeComponent();
            // form kapatıldığında giriş formunu aç
            FrmGiris frm = new FrmGiris();
            this.FormClosing += (sender, e) => frm.Show();


        }
        public string hesapno;
        SqlConnection baglanti = new SqlConnection("Data Source=RAMPAGE\\SQLEXPRESS;Initial Catalog=DbBanka;Integrated Security=True");
        // verileri getirme
        void verigetir()
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("Select * from TblKisiler inner join TblHesap on TblKisiler.HesapNo=TblHesap.HesapNo where TblKisiler.HesapNo=@p1", baglanti);
            komut.Parameters.AddWithValue("@p1", hesapno);
            SqlDataReader dr = komut.ExecuteReader();
            if (dr.Read())
            {
                LblAdSoyad.Text = dr[1].ToString() + " " + dr[2].ToString();
                LblHesap.Text = dr[5].ToString();
                LblTelefon.Text = dr[4].ToString();
                LblTc.Text = dr[3].ToString();
                LblBakiye.Text = dr[8].ToString() + " ₺";

            }
            baglanti.Close();
        }
        // textboxları temizlemek için
        void temizle()
        {
            TxtHesap.Text = "";
            TxtTutar.Text = "";
        }
        private void FrmHesap_Load(object sender, EventArgs e)
        {
           verigetir();
        }
        // para gönderme işlemi
        private void BtnGonder_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("Update TblHesap set Bakiye=Bakiye+@p1 where HesapNo=@p2", baglanti);            
            komut.Parameters.AddWithValue("@p1", TxtTutar.Text);
            komut.Parameters.AddWithValue("@p2", TxtHesap.Text);            
            komut.ExecuteNonQuery();
            //baglanti.Close();
            SqlCommand komut2 = new SqlCommand("Update TblHesap set Bakiye=Bakiye-@p1 where HesapNo=@p2", baglanti);
            komut2.Parameters.AddWithValue("@p1", TxtTutar.Text);
            komut2.Parameters.AddWithValue("@p2", hesapno);
            komut2.ExecuteNonQuery();
            //baglanti.Close();
            SqlCommand komut3 = new SqlCommand("Insert into TblHareket (Gonderen,Alici,Tutar) values (@p1,@p2,@p3)", baglanti);
            komut3.Parameters.AddWithValue("@p1", hesapno);
            komut3.Parameters.AddWithValue("@p2", TxtHesap.Text);
            komut3.Parameters.AddWithValue("@p3", TxtTutar.Text);
            komut3.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Para Gönderme İşlemi Başarılı");
            verigetir();
            temizle();
        }
    }
}
