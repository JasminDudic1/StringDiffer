using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
namespace Jamb.Columns
{
    class FirstThrowColumn:BaseColumn
    {
        public FirstThrowColumn(Panel panel, int index, GamePanel game, int playerNumber, Jamb.Players.BasePlayer player) :
            base(panel, index, game, playerNumber, player)
        {
            base.nameLabel.Text = "H";
            this.ColumnIndex = 6;
        }

        public override string ReturnName()
        {
            return "First throw";
        }

        public override string Description()
        {
            return "Column that can be played the first throw, otherwise is 0";
        }

        public override void CheckDice(List<Dice> dice)
        {
            int rollCount = game.RollCount;
            int value = 0;
            for (int i = 0; i < 15; i++)
            {
                calculatedValues[i] = -1;
                if (!Writable(i) || (game.forcedLabel != null && labels[i] != game.forcedLabel))
                {
                    if (values[i] == -1) labels[i].Text = "";
                    continue;
                }

                if (rollCount != 1)
                {
                    if (i == 7) value = 5;
                    else if (i == 8) value = 30;
                    else value = 0;
                }
                else value = CellCalculator.CalculateCellValue(i, dice, rollCount);
                if (value == -1) continue;
                labels[i].Text = value + " ";
                calculatedValues[i] = value;
            }

        }

    }

    

}
