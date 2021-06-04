using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
namespace Jamb.Columns
{
    class ColumnBuilder
    {

        public static void BuildColumn(Panel panel, int index, BaseColumn column)
        {

            double offsetLoc = 0.05;
            double offsetSizeHeight = 0.05;
            double offsetSizeWidth = 0.03;

            column.getNameLabel().Text = "";
            column.getNameLabel().Size = new Size((int)(offsetSizeWidth * StaticData.windowWidth), (int)(offsetSizeHeight * StaticData.windowHeight));
            column.getNameLabel().Location = new Point((int)(0.03 * index * StaticData.windowWidth), (int)(offsetLoc * 0 * StaticData.windowHeight));

            column.getNameLabel().Parent = panel;

            column.getNameLabel().Font = new Font("Arial", 18);
            column.getNameLabel().TextAlign = ContentAlignment.MiddleCenter;


            for (int i = 0; i < 16; i++)
            {

                column.getLabels()[i] = new Label();
                column.getLabels()[i].Text = "";
                column.getLabels()[i].Size = new Size((int)(offsetSizeWidth * StaticData.windowWidth), (int)(offsetSizeHeight * StaticData.windowHeight));
                column.getLabels()[i].Location = new Point((int)(0.03 * index * StaticData.windowWidth), (int)(offsetLoc * (i + 1) * StaticData.windowHeight));

                column.getLabels()[i].Parent = panel;

                column.getLabels()[i].Font = new Font("Arial", 12);
                column.getLabels()[i].BorderStyle = BorderStyle.FixedSingle;
                column.getLabels()[i].TextAlign = ContentAlignment.MiddleCenter;

                column.getValues()[i] = -1;


            }



        }
    }
}
