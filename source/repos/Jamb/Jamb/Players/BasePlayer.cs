using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Jamb.Columns;
using System.Windows.Forms;

namespace Jamb.Players
{
    class BasePlayer
    {

        protected GamePanel game;
        protected BaseColumn[] columns;
        protected Label sumLabel;
        protected int sum = 0;

        protected bool keepRolling = true;

        internal BaseColumn[] Columns { get => columns; set => columns = value; }

        public BasePlayer(GamePanel game, Label sumLabel )
        {
            this.game = game;
            this.sumLabel = sumLabel;
        }

        public virtual void LabelClick(int index, int row, int player)
        {
            //this.WriteToCell(index, row);
            game.WriteCell(index, row, player);
            SetSum();

        }

        public virtual void RollClicked()
        {
           this.game.RollDice();
        }

        public async virtual void AfterRoll()
        {

           /* double[][] values = CellCalculator.GetRowsValuesLvl1(game.Dice, game.RollCount, this.columns);
            

            List<int> bestCell = CellCalculator.GetBestCell(values);
            SetDebug(values);

            if (CellCalculator.CalculateDiceKeepLvl1(bestCell, game.Dice, game.RollCount)){
                keepRolling = false;
                await Task.Delay(2000);
                WriteToCell(bestCell[2], bestCell[0]);
                return;

            }

            game.SetAllDiceImages();*/

        }


        public virtual void SetDebug(double[][] values)
        {

            string text = "";
            string[] chars = { "1", "2", "3", "4", "5", "6", "+", "−", "S", "T", "F", "P", "Y" };


            for (int i = 0; i < 13; i++)
            {
                text += chars[i].PadRight(2, ' ') + ":";

                for (int j = 0; j < this.columns.Length; j++)
                {
                    values[j][i] = Math.Round(values[j][i], 1);
                    int offset = 3 - values[j][i].ToString().Length;
                    if (values[j][i] == 0) offset = 1;
                    text += values[j][i].ToString().PadRight(5, ' ') + "|";
                }
                text += '\n';

            }

            game.DebugLabel.Text = text;

        }

        public virtual void WriteToCell(int index, int row)
        {


            if (row > 5) row++;
            if (row > 8) row++;

            game.WriteCell(index,row, game.currentPlayer);
            SetSum();
        }

        public virtual int GetSum()
        {
            return this.sum;
        }

        public virtual void SetSum()
        {
            int sum = 0;
            for (int i = 0; i < columns.Length; i++)
                sum += columns[i].GetAllSum();
            sumLabel.Text = "" + sum;
            this.sum = sum;
        }

        public async virtual void TurnStart()
        {
            
        }



    }
}
