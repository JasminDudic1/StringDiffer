using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
namespace Jamb.Columns
{
    class UpColumn:BaseColumn
    {

        public UpColumn(Panel panel, int index, GamePanel game, int playerNumber, Jamb.Players.BasePlayer player) :
            base(panel, index, game, playerNumber, player)
        {
            base.nameLabel.Text = "↑";
            this.ColumnIndex = 3;
        }

        public override string ReturnName()
        {
            return "Up";
        }

        public override string Description()
        {
            return "Column to play from bottom to up";
        }

        public override bool Writable(int row)
        {

            if (values[row] != -1) return false;

            if (row == 14) return true;

            if (row == 6 || row == 9 || row == 15) return false;

            if (values[GetRowAfter(row)] == -1) return false;

            return true;

        }

        private int GetRowAfter(int row)
        {

            if (row == 5 || row == 8 ) return row + 2;
            return row + 1;

        }

    }
}
