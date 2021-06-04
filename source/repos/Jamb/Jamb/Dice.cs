using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace Jamb
{
    class Dice
    {

        PictureBox die;
        bool rollable;
        int value;
        private GamePanel game;

        public Dice(int i,GamePanel game)
        {
            this.game = game;
            die = new PictureBox();
            Point point;
            if (i % 2 == 0)
            {
                point = new Point((int)(0.4 * StaticData.windowWidth), (int)(0.2 * (i / 2) * StaticData.windowHeight));
            }
            else point = new Point((int)(0.5 * StaticData.windowWidth), (int)(0.2 * (i / 2) * StaticData.windowHeight));
            die.Location = point;
            die.Size = new Size((int)(0.1 * StaticData.windowWidth), (int)(0.1 * StaticData.windowWidth));
            die.SizeMode = PictureBoxSizeMode.StretchImage;
            die.Click += GamePanel_Click;
            value = i;
            rollable = true;
        }

        private void GamePanel_Click(object sender, EventArgs e)
        {
            game.diceClicked(this);
        }

        public PictureBox DiePicture { get => die; set => die = value; }
        public bool Rollable { get => rollable; set => rollable = value; }
        public int Value { get => value; set => this.value = value; }
    }
}
