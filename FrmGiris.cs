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
    public partial class FrmGiris : Form
    {
        public FrmGiris()
        {
            InitializeComponent();
            // form kapatıldığında uygulamayı kapat
            this.FormClosing += (sender, e) => Application.Exit();
        }
        // sql bağlantısı
        SqlConnection baglanti = new SqlConnection("Data Source=RAMPAGE\\SQLEXPRESS;Initial Catalog=DbBanka;Integrated Security=True");
        private void BtnGiris_Click(object sender, EventArgs e)
        {
            // sql kullanıcı giriş sorgusu
            baglanti.Open();
            SqlCommand komut = new SqlCommand("Select * from TblKisiler where Hesapno=@p1 and Sifre=@p2", baglanti);
            komut.Parameters.AddWithValue("@p1", MTxtHesap.Text);
            komut.Parameters.AddWithValue("@p2", TxtSifre.Text);
            SqlDataReader dr = komut.ExecuteReader();
            if (dr.Read())
            {
                FrmHesap fr = new FrmHesap();
                fr.hesapno = MTxtHesap.Text;
                fr.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Hatalı Giriş");
            }
            baglanti.Close();
        }

        private void LBtnKayıt_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            FrmKayit fr = new FrmKayit();
            fr.Show();
        }
    }
}
