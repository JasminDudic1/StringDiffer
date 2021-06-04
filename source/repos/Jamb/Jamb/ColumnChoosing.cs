using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace Jamb
{
    class ColumnChoosing
    {

        private Panel startPanel;

        private Label[] columns;

        private List<int> chosenColumns;

        private int numberOfColumns = 8;

        private string[] labelText = {"Down","Free","Up","To Middle","From Middle","Hand","Max","Order and Answer", };
        private string[] labelDesc = { "Goes from top to bottom",
            "Free to write as you want",
            "Goes from bottom to up",
            "Goes from top to max and from bottom to min",
            "Goes from max to top and from min to bottom",
            "Writable on first throw, otherwise is zero",
            "Writable only when max value of cell, otherwise is zero",
            "Selectable only on first turn, forces the opponent to answer.\n Answers the forced cell, only playable after opponent orders"};

        private Button startButton;

        public ColumnChoosing()
        {

            StartPanel = new Panel();
            StartPanel.Location = new System.Drawing.Point((int)(0.002 * StaticData.windowWidth), (int)(0.002 * StaticData.windowHeight));
            StartPanel.Size = new System.Drawing.Size((int)(0.98 * StaticData.windowWidth), (int)(0.98 * StaticData.windowHeight));
            StartPanel.Visible = true;
            StartPanel.BringToFront();
            StartPanel.BackColor = Color.Green;

            columns = new Label[numberOfColumns];

            chosenColumns = new List<int>();

            for(int i = 0; i < numberOfColumns; i++)
            {

                Label lab = new Label();

                int j = i % 3;
                int k = i / 3;

                int xloc =(int)((0.1 + j*0.3 )*  StaticData.windowWidth);
                int yloc = (int)((0.1 + k * 0.25) * StaticData.windowHeight);

                lab.Location = new System.Drawing.Point(xloc,yloc);
                lab.Size = new System.Drawing.Size((int)(0.2 * StaticData.windowWidth), (int)(0.1 * StaticData.windowHeight));
                lab.Text = labelText[i]+ "\n"+labelDesc[i];
                lab.Parent = StartPanel;
                lab.Visible = true;
                lab.TextAlign = ContentAlignment.MiddleCenter;

                lab.Click += Lab_Click;
                lab.BackColor = Color.Gray;


                columns[i] = lab;

            }


            startButton = new Button();
            startButton.Text = "Start game";

       
            startButton.Location = new System.Drawing.Point((int)(0.4 * StaticData.windowWidth), (int)(0.8 * StaticData.windowHeight));
            startButton.Size = new System.Drawing.Size((int)(0.2 * StaticData.windowWidth), (int)(0.1 * StaticData.windowHeight));
            startButton.Parent = startPanel;
            startButton.Visible = true;

        }

        public Panel StartPanel { get => startPanel; set => startPanel = value; }
        public List<int> ChosenColumns { get => chosenColumns; set => chosenColumns = value; }
        public Button StartButton { get => startButton; set => startButton = value; }

        private void Lab_Click(object sender, EventArgs e)
        {

            Label temp = (sender as Label);
            int index = Array.IndexOf(columns,temp);
            if (chosenColumns.Contains(index))
            {
                chosenColumns.Remove(index);
                temp.BackColor = Color.Gray;
            }
            else
            {
                chosenColumns.Add(index);
                temp.BackColor = Color.Blue;
            }



        }
    }
}
