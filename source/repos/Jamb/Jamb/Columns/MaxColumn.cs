using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
namespace Jamb.Columns
{
    class MaxColumn:BaseColumn
    {
        public MaxColumn(Panel panel, int index, GamePanel game, int playerNumber, Jamb.Players.BasePlayer player) :
            base(panel, index, game, playerNumber, player)
        {
            base.nameLabel.Text = "M";
            this.ColumnIndex = 7;
        }

        public override string ReturnName()
        {
            return "Max";
        }

        public override string Description()
        {
            return "Column that only writes max value of the cell, or 0";
        }

        public override void CheckDice(List<Dice> dice)
        {
            int value = 0;
            for (int i = 0; i < 15; i++)
            {
                calculatedValues[i] = -1;
                if (!Writable(i) || (game.forcedLabel != null && labels[i] != game.forcedLabel))
                {
                    if (values[i] == -1) labels[i].Text = "";
                    continue;
                }
                value = CellCalculator.CalculateCellValue(i, dice, game.RollCount);
                if (value != CellCalculator.GetMax(i)) value = 0;
                if (value == -1 ) continue;
                labels[i].Text = value + " ";
                calculatedValues[i] = value;
            }

        }

    }
}
