using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;

namespace Jamb
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        StartMenu menu;
        GamePanel game;
        OpponentMenu opponentMenu;
        ColumnChoosing columnMenu;
       

        private void Form1_Load(object sender, EventArgs e)
        {

            this.WindowState = FormWindowState.Maximized;
            StaticData.Initialize(this.Height,this.Width);
            menu = new StartMenu();
            menu.StartPanel.Parent = this;

           

            menu.StartOffline.Click += StartOffline_Click;


        }

        private void StartOffline_Click(object sender, EventArgs e)
        {

            opponentMenu = new OpponentMenu();
            opponentMenu.StartPanel.Parent = this;
            opponentMenu.VersusBot.Click += VersusBot_Click;
            opponentMenu.VersusOpponent.Click += VersusOpponent_Click;
            menu.StartPanel.Visible = false;

        }

        private void VersusOpponent_Click(object sender, EventArgs e)
        {

           /* game = new GamePanel(imageList1);
            game.MainPanel.Parent = this;*/

            opponentMenu.StartPanel.Visible = false;
            ChooseColumns();
        }

        private void VersusBot_Click(object sender, EventArgs e)
        {

            opponentMenu.StartPanel.Visible = false;
            ChooseColumns();
        }

        private void ChooseColumns()
        {
            columnMenu = new ColumnChoosing();
            columnMenu.StartPanel.Parent = this;
            columnMenu.StartButton.Click += StartButton_Click;
        }

        private void StartButton_Click(object sender, EventArgs e)
        {
            columnMenu.StartPanel.Visible = false;
            game = new GamePanel(imageList1,columnMenu.ChosenColumns);
            game.MainPanel.Parent = this;
        }
    }
}
