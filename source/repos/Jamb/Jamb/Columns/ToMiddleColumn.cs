using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
namespace Jamb.Columns
{
    class ToMiddleColumn : BaseColumn
    {
        public ToMiddleColumn(Panel panel, int index, GamePanel game, int playerNumber, Jamb.Players.BasePlayer player) :
            base(panel, index, game, playerNumber, player)
        {
            base.nameLabel.Text = "↓↑";
            this.ColumnIndex = 4;
        }

        public override string ReturnName()
        {
            return "To middle";
        }

        public override string Description()
        {
            return "Column to play from top to max and from bottom to min";
        }

        public override bool Writable(int row)
        {

            if (values[row] != -1) return false;

            if (row == 0 || row == 14) return true;
            
            if (row == 6 || row == 9 || row == 15) return false;

            if (row <= 7 && values[GetRowBefore(row)] == -1) return false;
            else if (row >= 8 && values[GetRowAfter(row)] == -1) return false;

            return true;

        }
        private int GetRowBefore(int row)
        {
            if (row == 7 || row == 10) return row - 2;
            return row - 1;
        }
        private int GetRowAfter(int row)
        {
            if (row == 5 || row == 8) return row + 2;
            return row + 1;
        }

    }
}
