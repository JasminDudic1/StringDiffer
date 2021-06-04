using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace Jamb
{
    class ColumnLabels
    {

        private Label[] labels;

        public ColumnLabels(Panel panel)
        {

            double offsetLoc = 0.05;
            double offsetSizeHeight = 0.05;
            double offsetSizeWidth = 0.03;

            labels = new Label[16];
            for(int i = 0; i <= 6; i++)
            {

                labels[i] = new Label();
                labels[i].Text = "" + (i+1);
                labels[i].Size = new Size((int)(offsetSizeWidth * StaticData.windowWidth), (int)(offsetSizeHeight * StaticData.windowHeight));
                labels[i].Location = new Point((int)(0.0 * StaticData.windowWidth), (int)(offsetLoc * (i+1) * StaticData.windowHeight));

                labels[i].Parent = panel;

            }

            for (int j = 0; j <= 2; j++)
            {
                int i = j + 7;
                labels[i] = new Label();
                labels[i].Size = new Size((int)(offsetSizeWidth * StaticData.windowWidth), (int)(offsetSizeHeight * StaticData.windowHeight));
                labels[i].Location = new Point((int)(0.0 * StaticData.windowWidth), (int)(offsetLoc * (i + 1) * StaticData.windowHeight));
                labels[i].Parent = panel;

            }
            labels[7].Text = "+";
            labels[8].Text = "-";

            for (int j = 0; j <= 5; j++)
            {
                int i = j + 10;
                labels[i] = new Label();
                labels[i].Text = "Text " + i;
                labels[i].Size = new Size((int)(offsetSizeWidth * StaticData.windowWidth), (int)(offsetSizeHeight * StaticData.windowHeight));
                labels[i].Location = new Point((int)(0.0 * StaticData.windowWidth), (int)(offsetLoc * (i + 1) * StaticData.windowHeight));
                labels[i].Parent = panel;

            }


            for(int i = 0; i < 16; i++)
            {
                labels[i].Font = new Font("Arial", 12);
                labels[i].BorderStyle = BorderStyle.FixedSingle;
                labels[i].TextAlign = ContentAlignment.MiddleCenter;
            }

            labels[6].Text = labels[9].Text = labels[15].Text = "Sum";
            labels[10].Text = "S";
            labels[11].Text = "T";
            labels[12].Text = "F";
            labels[13].Text = "P";
            labels[14].Text = "Y";



        }


    }
}
