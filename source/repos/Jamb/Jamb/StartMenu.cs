using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace Jamb
{
    class StartMenu
    {

        private Panel startPanel;
        private Button startOffline;
        private Button startOnline;

        public StartMenu()
        {

            startPanel = new Panel();
            startPanel.Location = new System.Drawing.Point((int)(0.002 * StaticData.windowWidth), (int)(0.002 * StaticData.windowHeight));
            startPanel.Size = new System.Drawing.Size((int)(0.98*StaticData.windowWidth),(int)(0.98*StaticData.windowHeight));
            startPanel.Visible = true;
            startPanel.BringToFront();
            startPanel.BackColor = Color.Green;

            startOffline = new Button();
            startOffline.Location = new Point((int)(0.3 * StaticData.windowWidth), (int)(0.1 * StaticData.windowHeight));
            startOffline.Size = new Size((int)(0.4 * StaticData.windowWidth), (int)(0.1 * StaticData.windowHeight));
            startOffline.Visible = true;
            startOffline.Parent = startPanel;
            startOffline.Text = "Play offline";

            startOnline = new Button();
            startOnline.Location = new Point((int)(0.3 * StaticData.windowWidth), (int)(0.3 * StaticData.windowHeight));
            startOnline.Size = new Size((int)(0.4 * StaticData.windowWidth), (int)(0.1 * StaticData.windowHeight));
            startOnline.Visible = true;
            startOnline.Parent = startPanel;
            startOnline.Text = "Play online";

        }

        public Panel StartPanel { get => startPanel; set => startPanel = value; }
        public Button StartOffline { get => startOffline; set => startOffline = value; }
        public Button StartOnline { get => startOnline; set => startOnline = value; }
    }
}
