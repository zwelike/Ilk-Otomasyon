using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace ProjectB
{
    public partial class Form1 : Form
    {
        SqlConnection connection;
        SqlCommand komut;
        SqlDataAdapter da;

        public Form1()
        {
            InitializeComponent();
           
        }
         void OgrenciGetir()
           {
            string connectionString = "Data Source=LAPTOP-RJCOG2N1\\SQLEXPRESS;Initial Catalog=project;Integrated Security=SSPI";
            connection = new SqlConnection(connectionString);
            connection.Open();
            da = new SqlDataAdapter("SELECT * FROM kimlikbilgileri", connection);
            DataTable tablo = new DataTable();
            da.Fill(tablo);
            dataGridView1.DataSource = tablo;
            connection.Close();

        }

        private void Form1_Load(object sender, EventArgs e)
        {
              OgrenciGetir();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string sorgu = "INSERT INTO  kimlikbilgileri (ogrno,adi,soyadi,bolumu,dogumtarih,dogumyeri,tcno,girisyili,cinsiyet) VALUES (@ogrno,@adi,@soyadi,@bolumu,@dogumtarih,@dogumyeri,@tcno,@girisyili,@cinsiyet)";
            komut = new SqlCommand(sorgu, connection);
            komut.Parameters.AddWithValue("@ogrno", txtno.Text);
            komut.Parameters.AddWithValue("@adi", txtad.Text);
            komut.Parameters.AddWithValue("@soyadi", txtsoyad.Text);
            komut.Parameters.AddWithValue("@bolumu", txtbolum.Text);
            komut.Parameters.AddWithValue("@dogumtarih", dateTimePicker1.Value); 
            komut.Parameters.AddWithValue("@dogumyeri", txtdy.Text);
            komut.Parameters.AddWithValue("@tcno", txttc.Text);
            komut.Parameters.AddWithValue("@girisyili", txtgırısyıl.Text);
            komut.Parameters.AddWithValue("@cinsiyet", txtcins.Text);
            connection.Open();
            komut.ExecuteNonQuery();
            connection.Close();
            OgrenciGetir();

        }

        private void dataGridView1_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            txtno.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            txtad.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            txtsoyad.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            txtbolum.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();

            dateTimePicker1.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();

            txtdy.Text = dataGridView1.CurrentRow.Cells[5].Value.ToString();
            txttc.Text = dataGridView1.CurrentRow.Cells[6].Value.ToString();
            txtgırısyıl.Text = dataGridView1.CurrentRow.Cells[7].Value.ToString();
            txtcins.Text = dataGridView1.CurrentRow.Cells[8].Value.ToString();



        }

        private void sil_Click(object sender, EventArgs e)
        {
            string sorgu = "DELETE FROM kimlikbilgileri WHERE ogrno=@ogrno";
            komut = new SqlCommand(sorgu, connection);
            komut.Parameters.AddWithValue("@ogrno", txtno.Text);
            connection.Open();
            komut.ExecuteNonQuery();
            connection.Close();
            OgrenciGetir();
        }

        private void güncülle_Click(object sender, EventArgs e)
        {
            string sorgu = "UPDATE kimlikbilgileri SET ogrno=@ogrno, adi=@adi, soyadi=@soyadi, bolumu=@bolumu, dogumtarih=@dogumtarih, dogumyeri=@dogumyeri, tcno=@tcno, girisyili=@girisyili, cinsiyet=@cinsiyet WHERE ogrno=@ogrno ";
            komut = new SqlCommand(sorgu, connection);
            komut.Parameters.AddWithValue("@ogrno", txtno.Text);
            komut.Parameters.AddWithValue("@adi", txtad.Text);
            komut.Parameters.AddWithValue("@soyadi", txtsoyad.Text);
            komut.Parameters.AddWithValue("@bolumu", txtbolum.Text);
            komut.Parameters.AddWithValue("@dogumtarih", dateTimePicker1.Value);
            komut.Parameters.AddWithValue("@dogumyeri", txtdy.Text);
            komut.Parameters.AddWithValue("@tcno", txttc.Text);
            komut.Parameters.AddWithValue("@girisyili", txtgırısyıl.Text);
            komut.Parameters.AddWithValue("@cinsiyet", txtcins.Text);



            try
            {
                connection.Open();
                int affectedRows = komut.ExecuteNonQuery();
                if (affectedRows > 0)
                {
                    MessageBox.Show("Kayıt güncellendi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Kayıt güncellenemedi.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata oluştu: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                connection.Close();
                OgrenciGetir();
            }
        }

    }
}
