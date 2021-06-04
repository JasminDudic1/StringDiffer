using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace Jamb.Columns
{
    class AnswerColumn : BaseColumn
    {
        public AnswerColumn(Panel panel, int index, GamePanel game, int playerNumber,Jamb.Players.BasePlayer player) :
            base(panel, index, game, playerNumber,player)
        {
            base.nameLabel.Text = "A";
            this.ColumnIndex = 9;
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

            if (values[row] != -1) return false;

            if (game.forcedLabel == null) return false;
            if (game.forcedLabel != labels[row]) return false;

            if (row == 6 || row == 9 || row == 15) return false;

            return true;
        }

        public override void AfterWriting()
        {
            game.forcedLabel = null;
            game.forcedRow = -1;
            game.forcedType = -1;
            game.forcedIndex = -1;
        }

        public override void AfterReset()
        {
            if (game.forcedLabel != null && game.currentPlayer==playerNumber)
            {
                game.forcedLabel = labels[game.forcedRow];
                game.forcedIndex = index;
                labels[game.forcedRow].BackColor = Color.Aqua;
            }
            
        }

    }
}
