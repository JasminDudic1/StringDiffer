using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace Jamb
{
    class OpponentMenu
    {

        private Panel startPanel;
        private Button versusOpponent;
        private Button versusBot;

        public OpponentMenu()
        {

            StartPanel = new Panel();
            StartPanel.Location = new System.Drawing.Point((int)(0.002 * StaticData.windowWidth), (int)(0.002 * StaticData.windowHeight));
            StartPanel.Size = new System.Drawing.Size((int)(0.98 * StaticData.windowWidth), (int)(0.98 * StaticData.windowHeight));
            StartPanel.Visible = true;
            StartPanel.BringToFront();
            StartPanel.BackColor = Color.Green;

            VersusOpponent = new Button();
            VersusOpponent.Location = new Point((int)(0.3 * StaticData.windowWidth), (int)(0.1 * StaticData.windowHeight));
            VersusOpponent.Size = new Size((int)(0.4 * StaticData.windowWidth), (int)(0.1 * StaticData.windowHeight));
            VersusOpponent.Visible = true;
            VersusOpponent.Parent = StartPanel;
            VersusOpponent.Text = "Play versus opponent";

            VersusBot = new Button();
            VersusBot.Location = new Point((int)(0.3 * StaticData.windowWidth), (int)(0.3 * StaticData.windowHeight));
            VersusBot.Size = new Size((int)(0.4 * StaticData.windowWidth), (int)(0.1 * StaticData.windowHeight));
            VersusBot.Visible = true;
            VersusBot.Parent = StartPanel;
            VersusBot.Text = "Play versus bot";

        }

        public Panel StartPanel { get => startPanel; set => startPanel = value; }
        public Button VersusOpponent { get => versusOpponent; set => versusOpponent = value; }
        public Button VersusBot { get => versusBot; set => versusBot = value; }
    }
}
