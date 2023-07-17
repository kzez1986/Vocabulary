using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace Słówka
{
    public partial class Form1 : Form
    {
        private string nazwaPliku = "default.txt";
        private List<string[]> słówka = new List<string[]>();
        private Random los = new Random();

        public Form1()
        {
            InitializeComponent();
            WczytajPlik("default.txt");
            timer2.Interval = los.Next(10000) + 1;
            timer2.Enabled = true;
        }

        private void zamknijToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Hide();
            timer1.Enabled = false;
        }

        private void WczytajPlik(string nazwaPliku)
        {
            słówka.Clear();
            StreamReader sr = new StreamReader(nazwaPliku);
            string linia;
            while (!sr.EndOfStream)
            {
                linia = sr.ReadLine();
                int indeks = linia.LastIndexOf('|');
                string[] podział = new string[2];
                podział[0] = linia.Substring(0,indeks);
                podział[1] = linia.Substring(indeks+1,linia.Length-indeks-1);
                słówka.Add(podział);
            }
            sr.Close();
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            int indeks = los.Next(słówka.Count);
            notifyIcon1.ShowBalloonTip(15000,"UWAGA! Słówko", słówka[indeks][0] + "\n" + słówka[indeks][1],ToolTipIcon.Info);
            if (checkBox2.Checked == true)
                (new System.Media.SoundPlayer("slowko.wav")).Play();
            if (checkBox1.Checked == true)
                timer2.Interval = los.Next(trackBar1.Value * 30 * 1000) + 1;
            else
                timer2.Interval = trackBar1.Value * 30 * 1000;
        }

        private void wczytajPlikToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "Zestawy słówek (*.zst)|*.zst";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                WczytajPlik(openFileDialog1.FileName);
            }
            
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            int nowy_interwal = trackBar1.Value * 30;
            textBox1.Text = nowy_interwal.ToString() + " s";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Hide();
        }

        private void ustawieniaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Show();
        }
    }
}
