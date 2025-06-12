using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Restoran_siparis_fis_olustur_2
{
    public partial class Yönetici: Form
    {
        Menu menu = new Menu(); // Menü nesnesi

        public Yönetici()
        {
            InitializeComponent();
        }

        private void Yönetici_Load(object sender, EventArgs e)
        {
            button2.Enabled = false;
            button3.Enabled = false;
            menu.yemeks = new List<Yemek>
    {
        new Yemek("Köfte", 180),
       new Yemek("Mercimek çorbası",45),new Yemek("Izgara et çeşitleri (tavuk ve et karışık)",240),
        new Yemek("Bulgur pilavı", 65),
        new Yemek("Lahmacun",100),
new Yemek("Tavuk Döner",140)
,new Yemek("Et Döner",200),
                new Yemek("Baklava(porsiyon)",80),new Yemek("Kek(porsiyon)",50),new Yemek("Muzlu puding",45),new Yemek("Künefe", 100),
new Yemek("Kola",40),new Yemek("Meyve suyu",30), new Yemek("Ayran", 15),new Yemek("Su", 10)
    }; MenuleriListBoxaYansit();
            MenuyuKaydet(); 

        }
        private void MenuleriListBoxaYansit()
        {
            Menu.Items.Clear();
            foreach (var yemek in menu.yemeks)
            {
                Menu.Items.Add($"{yemek.Ad} - {yemek.Fiyat} TL");
            }
        }
        private void MenuyuKaydet()
        {
            try
            {
                using (StreamWriter sw = new StreamWriter("menu.txt"))
                {
                    foreach (var yemek in menu.yemeks)
                    {
                        sw.WriteLine($"{yemek.Ad}|{yemek.Fiyat}");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Menü kaydedilirken hata oluştu: " + ex.Message);
            }
        }


        private void button2_Click(object sender, EventArgs e)
        {
            
            string ad = textBox2.Text.Trim();
            string fiyatText = textBox3.Text.Trim();

            if (string.IsNullOrEmpty(ad) || !int.TryParse(fiyatText, out int fiyat))
            {
                MessageBox.Show("Geçerli bir ürün adı ve fiyat girin.");
                return;
            }

            menu.YemekEkle(new Yemek(ad, fiyat));
            MenuleriListBoxaYansit();

            textBox2.Clear();
            textBox3.Clear();
        

    }

        private void button3_Click(object sender, EventArgs e)
        {
            int seciliIndex = Menu.SelectedIndex;

            if (seciliIndex == -1)
            {
                MessageBox.Show("Silmek için bir ürün seçin.");
                return;
            }

            // Menüden seçili yemeği sil
            menu.yemeks.RemoveAt(seciliIndex);
            MenuleriListBoxaYansit();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string girilenID = textBox1.Text.Trim();

            if (girilenID == "admin123")
            {
                button2.Enabled = true;
                button3.Enabled = true;
                MessageBox.Show("Giriş başarılı. Menü düzenlemeye başlayabilirsiniz.");
            }
            else
            {
                MessageBox.Show("Geçersiz Yönetici ID.");
            }
        }
    }
}
