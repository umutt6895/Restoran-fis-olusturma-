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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Restoran_siparis_fis_olustur_2
{
   
    public partial class Musteri: Form
    {
        List<Yemek> menuListesi = new List<Yemek>();
        List<(Yemek yemek, int adet)> siparisListesi = new List<(Yemek, int)>();
        public Musteri()
        {
            InitializeComponent();
        }

        private void Musteri_Load(object sender, EventArgs e)
        {
            menuListesi.Clear();
            MENÜ.Items.Clear();

            try
            {
                if (File.Exists("menu.txt"))
                {
                    string[] satirlar = File.ReadAllLines("menu.txt");

                    foreach (var satir in satirlar)
                    {
                        string[] parcalar = satir.Split('|');
                        if (parcalar.Length == 2)
                        {
                            string ad = parcalar[0];
                            int fiyat = int.Parse(parcalar[1]);
                            var yemek = new Yemek(ad, fiyat);
                            menuListesi.Add(yemek);
                            MENÜ.Items.Add($"{yemek.Ad} - {yemek.Fiyat} TL");
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Menü dosyası bulunamadı.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Menü yüklenirken hata oluştu: " + ex.Message);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int seciliIndex = MENÜ.SelectedIndex;
            if (seciliIndex == -1)
            {
                MessageBox.Show("Lütfen bir yemek seçin.");
                return;
            }
            int adet = (int)numericUpDown1.Value;
            if (adet <= 0)
            {
                MessageBox.Show("Lütfen adet seçin.");
                return;
            }
            Yemek secilenYemek = menuListesi[seciliIndex];
            siparisListesi.Add((secilenYemek, adet));
            // Sipariş listesini güncelle
            Seçilenler.Items.Clear();
            foreach (var item in siparisListesi)
            {
                Seçilenler.Items.Add($"{item.yemek.Ad} x {item.adet} = {item.yemek.Fiyat * item.adet} TL");
            }

            // Toplam tutarı hesapla ve göster
            int toplam = 0;
            foreach (var item in siparisListesi)
            {
                toplam += item.yemek.Fiyat * item.adet;
            }
            label2.Text = $"Toplam Tutar: {toplam} TL";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox1.Text))
            {
                MessageBox.Show("Lütfen masa numarasını girin.");
                return;
            }
            if (siparisListesi.Count == 0)
            {
                MessageBox.Show("Sipariş listesi boş.");
                return;
            }
            string masaNo = textBox1.Text.Trim();
           
            string klasorYolu = Application.StartupPath; // Uygulama dizini
            int maxDosyaBoyutu = 1024 * 1024; // 1 MB sınırı (byte cinsinden)

            int dosyaIndex = 1;
            string dosyaAdi;
            FileInfo fi;

            // Mevcut dosyaların içinden en sonuncusunu bul ve boyutuna bak
            do
            {
                dosyaAdi = Path.Combine(klasorYolu, $"GunlukFis_{dosyaIndex}.txt");
                fi = new FileInfo(dosyaAdi);
                dosyaIndex++;
            }
            while (fi.Exists && fi.Length > maxDosyaBoyutu);

            // Yazılacak dosya artık bulundu
            try
            {
                using (StreamWriter sw = new StreamWriter(dosyaAdi, true)) // true: ekleme modu
                {
                    sw.WriteLine($"Masa No: {masaNo}");
                    sw.WriteLine("Siparişler:");

                    foreach (var item in siparisListesi)
                    {
                        sw.WriteLine($"{item.yemek.Ad} x {item.adet} = {item.yemek.Fiyat * item.adet} TL");
                    }

                    int toplam = siparisListesi.Sum(x => x.yemek.Fiyat * x.adet);
                    sw.WriteLine($"Toplam Tutar: {toplam} TL");
                    sw.WriteLine($"Tarih: {DateTime.Now}");
                    sw.WriteLine(new string('-', 40)); // Ayraç
                }

                MessageBox.Show($"Fiş {Path.GetFileName(dosyaAdi)} dosyasına eklendi.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Dosya yazılırken hata oluştu: " + ex.Message);
            }
        }

    }
}
