using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
namespace Jamb.Columns
{
    class FreeColumn : BaseColumn
    {
     

        public FreeColumn(Panel panel, int index, GamePanel game, int playerNumber, Jamb.Players.BasePlayer player) :
            base(panel, index, game, playerNumber, player)
        {
            base.nameLabel.Text = "F";
            this.ColumnIndex = 2;
        }

        public override string ReturnName()
        {
            return "Free";
        }

        public override string Description()
        {
            return "Column to play as you like";
        }


    }
}
