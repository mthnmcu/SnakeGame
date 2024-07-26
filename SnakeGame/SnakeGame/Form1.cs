using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SnakeGame
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        Panel parca;
        Panel elma = new Panel();
        List<Panel> yilan = new List<Panel>();

        string yon = "sağ";
        int panelGenislik = 600; // Panel genişliği
        int panelYukseklik = 600; // Panel yüksekliği
        int parcaBoyut = 20; // Yılan parça boyutu


        private void label3_Click(object sender, EventArgs e)
        {
            label2.Text = "0";
            paneliTemizle();

            parca = new Panel();
            parca.Location = new Point(200, 200);
            parca.Size = new Size(parcaBoyut, parcaBoyut);
            parca.BackColor = Color.Purple;
            yilan.Add(parca);
            panel1.Controls.Add(yilan[0]);

            timer1.Start();
            elmaOlustur();
        }

        void carpismaKontrol()
        {
            // Yılanın başının panel sınırlarına çarpıp çarpmadığını kontrol et
            if (yilan[0].Location.X < 0 || yilan[0].Location.X >= panelGenislik ||
                yilan[0].Location.Y < 0 || yilan[0].Location.Y >= panelYukseklik)
            {
                label4.Visible = true;
                label4.Text = "KAYBETTİNİZ";
                timer1.Stop();
            }

            // Yılan parçalarının birbirine çarpıp çarpmadığını kontrol et
            for (int i = 2; i < yilan.Count; i++)
            {
                if (yilan[0].Location == yilan[i].Location)
                {
                    label4.Visible = true;
                    label4.Text = "KAYBETTİNİZ";
                    timer1.Stop();
                }
            }
        }


        void paneliTemizle()
        {
            panel1.Controls.Clear();
            yilan.Clear();
            label4.Visible = false;
        }


        private void timer1_Tick(object sender, EventArgs e)
        {
            int locx = yilan[0].Location.X;
            int locy = yilan[0].Location.Y;

            elmaYedimmi();
            hareket();
            carpismaKontrol();

            if (yon == "sağ")
            {
                locx += parcaBoyut;
            }
            if (yon == "sol")
            {
                locx -= parcaBoyut;
            }
            if (yon == "aşağı")
            {
                locy += parcaBoyut;
            }
            if (yon == "yukarı")
            {
                locy -= parcaBoyut;
            }

            // Panel sınırlarına çarpma kontrolü
            if (locx < 0 || locx >= panelGenislik || locy < 0 || locy >= panelYukseklik)
            {
                label4.Visible = true;
                label4.Text = "KAYBETTİNİZ";
                timer1.Stop();
                return; // Oyunu durdur
            }

            yilan[0].Location = new Point(locx, locy);
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Right && yon != "sol")
                yon = "sağ";
            if (e.KeyCode == Keys.Left && yon != "sağ")
                yon = "sol";
            if (e.KeyCode == Keys.Up && yon != "aşağı")
                yon = "yukarı";
            if (e.KeyCode == Keys.Down && yon != "yukarı")
                yon = "aşağı";
        }


        void elmaOlustur()
        {
            Random rnd = new Random();
            int elmaX, elmaY;
            elmaX = rnd.Next(panelGenislik);
            elmaY = rnd.Next(panelYukseklik);

            elmaX -= elmaX % parcaBoyut;
            elmaY -= elmaY % parcaBoyut;

            elma.Size = new Size(parcaBoyut, parcaBoyut);
            elma.BackColor = Color.Red;
            elma.Location = new Point(elmaX, elmaY);
            panel1.Controls.Add(elma);
        }

        void elmaYedimmi()
        {
            int puan = int.Parse(label2.Text);
            if (yilan[0].Location == elma.Location)
            {
                panel1.Controls.Remove(elma);
                puan += 50;
                label2.Text = puan.ToString();
                elmaOlustur();
                parcaEkle();
            }

            void parcaEkle()
            {
                Panel ekParca = new Panel();
                ekParca.Size = new Size(parcaBoyut, parcaBoyut);
                ekParca.BackColor = Color.Purple;
                yilan.Add(ekParca);
                panel1.Controls.Add(ekParca);
            }
        }
        void hareket()
        {
            for (int i = yilan.Count - 1; i > 0; i--)
                yilan[i].Location = yilan[i - 1].Location;
        }
    }
}
