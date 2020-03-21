using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Saper
{
    public partial class FormMainWindow : Form
    {
        private const int fieldSize = 30;
        private SaperLogic myGame;
        public FormMainWindow()
        {
            InitializeComponent();
            prostaToolStripMenuItem_Click(null, null);
        }

        private void prostaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            myGame = new SaperLogic(8, 8, 10);
            generateView();
        }

        private void średniaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            myGame = new SaperLogic(12, 10, 25);
            generateView();
        }

        private void trudnaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            myGame = new SaperLogic(20, 15, 50);
            generateView();
        }

        private void generateView()
        {
            panelButtons.Controls.Clear();

            for(int x = 0; x < myGame.BoardWidth; x++)
            {
                for( int y = 0; y < myGame.BoardHeight; y++)
                {
                    Button b = new Button();
                    b.Size = new Size(fieldSize, fieldSize);
                    b.Location = new Point(fieldSize * x, fieldSize * y);
                    b.Click += B_Click;
                    panelButtons.Controls.Add(b);
                    b.Tag = new Point(x, y);
                }
            }
        }

        private void B_Click(object sender, EventArgs e)
        {
            if(myGame.State == SaperLogic.GameState.InProgress)
            {
                if(sender is Button)
                {
                    Button b = sender as Button;
                    if(b.Tag is Point)
                    {
                        Point p = (Point)b.Tag;

                        myGame.Uncover(p);
                        refreshView();

                        if(myGame.State == SaperLogic.GameState.Win)
                        {
                            MessageBox.Show("Wygrałeś!");
                        }
                        else if(myGame.State == SaperLogic.GameState.Loss)
                        {
                            MessageBox.Show("Przegrałeś!");
                        }
                    }
                }
            }
        }
        
        private void refreshView()
        {
            foreach(Button b in panelButtons.Controls)
            {
                SaperLogic.Field f = myGame.GetField((Point)b.Tag);
                if(f.Covered == false)
                {
                    if(f.FieldType == SaperLogic.FieldTypeEnum.Bomb)
                    { 
                        b.BackColor = Color.Red;
                        b.Text = "@";
                    }
                    else
                    {
                        b.BackColor = Color.White;
                        
                        if(f.FieldType == SaperLogic.FieldTypeEnum.BombCount)
                        {
                            b.Text = f.BombCount.ToString();
                        }
                    }
                }
            }
        }
    }
}
