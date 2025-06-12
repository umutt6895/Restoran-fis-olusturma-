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
    
    public partial class Form1: Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Musteri mform = new Musteri();
            mform.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Yönetici yform = new Yönetici();
            yform.Show();
        }
    }
    public interface Iyemek
    {
        string Ad { get; }
        int Fiyat { get; }
    }

    public interface Iyazdirilabilir
    {
        void Fisyazdir(string dosyayolu);
    }

    public class Yemek : Iyemek
    {
        public string Ad { get; private set; }
        public int Fiyat { get; private set; }
        public Yemek(string ad, int fiyat)

        {
            Ad = ad; Fiyat = fiyat;

        }
        

    }
    public class Siparis : Iyazdirilabilir
    {
        public List<Yemek> yemekler;
        public int masano { get; private set; }
        public Siparis(int masa)

        {
            masano = masa;
            yemekler = new List<Yemek>();

        }

        public void Fisyazdir(string dosyayolu)
        {

        }

    }
    //  Menü yönetimi

    public class Menu
    {
        public List<Yemek> yemeks { get; set; }

        public void YemekEkle(Yemek y)
        {
            yemeks.Add(y);
        }
        public void YemekCikar(Yemek y)
        {
            yemeks.Remove(y);
        }
        public void GosterMenu()
        {
            foreach (var yemek in yemeks)
            {
                Console.WriteLine($"{yemek.Ad}  - {yemek.Fiyat} TL");
            }
        }

    }
}
