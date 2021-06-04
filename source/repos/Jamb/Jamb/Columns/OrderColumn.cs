using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
namespace Jamb.Columns
{
    class OrderColumn:BaseColumn
    {
        public OrderColumn(Panel panel, int index, GamePanel game, int playerNumber, Jamb.Players.BasePlayer player) :
            base(panel, index, game, playerNumber, player)
        {
            base.nameLabel.Text = "O";
            this.ColumnIndex = 8;
        }

        public override string ReturnName()
        {
            return "Answer";
        }

        public override string Description()
        {
            return "Column to play after opponents orders";
        }

        public override bool Writable(int row)
        {
          
            if (values[row] != -1)
            {

                return false;
            }

            if (game.forcedLabel == null && game.RollCount != 1)
            {

                return false;
            }
            if (game.forcedLabel != null && game.forcedLabel != labels[row])
            {

                return false;
            }

            if (row == 6 || row == 9 || row == 15) return false;
         
            return true;
        }

        public override bool WriteToCell(int row)
        {
            
            if (!Writable(row)) return false;
            if (calculatedValues[row] == -1) return false;
            if (game.forcedLabel != null && game.forcedLabel == labels[row])
            {

                values[row] = Convert.ToInt32(labels[row].Text);
                labels[row].BackColor = Color.White;
                return true;
            }
            else if (game.forcedLabel == null && game.RollCount == 1)
            { 
                force(row);
                return false;

            }return false;

        }

        private void force(int row)
        {
            game.forcedLabel = labels[row];
            game.forcedRow = row;
            game.forcedIndex = index;
            game.forcedType = 1;
            labels[row].BackColor = Color.Aqua;
        }
 

    }
}
