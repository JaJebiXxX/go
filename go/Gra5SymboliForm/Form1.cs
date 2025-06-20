using System;
using System.Drawing;
using System.Windows.Forms;
using System.Media;
using System.IO;

namespace Gra5SymboliForm
{
    public partial class Form1 : Form
    {
        private int defWidth = 30;
        private int defHeight = 30;
        private int defSpace = 5;
        static string soundFilePath = Path.Combine(Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory).Parent.Parent.FullName, "bre.wav");
        SoundPlayer player = new SoundPlayer(soundFilePath);
        private Gra5Symboli gra;

        public Form1()
        {
            InitializeComponent();
            gra = new Gra5Symboli();

            for (int i = 0; i < 19; i++)
            {
                for (int j = 0; j < 19; j++)
                {
                    tworzPrzycisk(i, j);
                }
            }
        }

        private void tworzPrzycisk(int col, int wie)
        {
            Button b = new Button();
            b.Width = defWidth;
            b.Height = defHeight;
            b.Left = col * (defWidth + defSpace);
            b.Top = wie * (defHeight + defSpace);
            b.FlatStyle = FlatStyle.Flat;
            b.BackColor = Color.FromArgb(240, 217, 181);
            b.Paint += new PaintEventHandler(b_Paint);
            planszaPanel.Controls.Add(b);
            b.Tag = new Point(col, wie);
            b.Click += b_Click;
        }

        private void b_Click(object sender, EventArgs e)
        {
            Button bb = (Button)sender;
            Point pp = (Point)bb.Tag;
            PSymbol ruch = gra.postawSymbol(pp.X, pp.Y);

            if (ruch != PSymbol.Nic)
            {
                bb.Invalidate();

                logTextBox.AppendText($"Ruch: {pp.X},{pp.Y} -> {ruch}\r\n");

                PSymbol zwyciezca = gra.ktoWygrał();
                if (zwyciezca != PSymbol.Nic)
                {
                    string zwyciezcaText = zwyciezca == PSymbol.Kółko ? "Wygraly biale" : "Wygraly czarne";
                    MessageBox.Show(zwyciezcaText);
                    gra.reset();
                    resetujPrzyciski();
                }
            }
            else
            {
                player.PlaySync();
            }
        }

        private void b_Paint(object sender, PaintEventArgs e)
        {
            Button bb = (Button)sender;
            Point pp = (Point)bb.Tag;
            PSymbol symbol = gra.jakiJestTuSymbol(pp.X, pp.Y);

            if (symbol != PSymbol.Nic)
            {
                Graphics g = e.Graphics;
                Rectangle rect = new Rectangle(5, 5, bb.Width - 10, bb.Height - 10);
                Brush brush = symbol == PSymbol.Kółko ? Brushes.White : Brushes.Black;
                g.FillEllipse(brush, rect);
            }
        }

        private void resetujPrzyciski()
        {
            foreach (Control ctrl in planszaPanel.Controls)
            {
                if (ctrl is Button btn)
                {
                    btn.Invalidate();
                }
            }
            logTextBox.Clear();
        }
    }
}
