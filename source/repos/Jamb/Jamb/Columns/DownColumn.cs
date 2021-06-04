using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;



namespace Jamb.Columns
{
    class DownColumn : BaseColumn
    {
    

        public DownColumn(Panel panel, int index, GamePanel game, int playerNumber, Jamb.Players.BasePlayer player) :
            base(panel, index, game, playerNumber, player)
        {
            base.nameLabel.Text = "↓";
            this.ColumnIndex = 1;
        }

        public override string ReturnName()
        {
            return "Down";
        }

        public override string Description()
        {
            return "Column that plays from top to bottom";
        }

        public override bool Writable(int row)
        {

            if (values[row] != -1) return false;

            if (row == 0) return true;

            if (values[GetRowBefore(row)] == -1) return false;

            if (row == 6 || row == 9 || row == 15) return false;

            return true;

        }

        private int GetRowBefore(int row)
        {
            if (row == 7 || row == 10) return row - 2;
            return row - 1;
        }


    }
}
