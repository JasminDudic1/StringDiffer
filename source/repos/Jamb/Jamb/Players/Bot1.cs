using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Jamb.Columns;
using System.Windows.Forms;

namespace Jamb.Players
{
    class Bot1: BasePlayer
    {

        int playingRow = -1;
        int playingColumn = -1;



        public Bot1(GamePanel game, Label sumLabel) : base(game,sumLabel) { }


        public override void LabelClick(int index, int row, int player)
        {
            
        }

        public override void RollClicked()
        {
            //this does nothing since the bot can not click to write in
        }

        public async override void TurnStart()
        {
            keepRolling = true;
            //when the turn starts the bot rolls once
            string text = "";

            for(int i = 0; i < 3; i++)
            {
                if (!keepRolling)
                {
                    game.DebugLabel.Text += text;
                    return;
                }
                game.RollDice();

                text += '\n';
                for (int j = 0; j < 6; j++)
                    text += game.Dice[j].Value + ": ";

                await Task.Delay(500);
            }


        }

        public async override void AfterRoll()
        {

            double[][] values = CellCalculator.GetRowsValuesLvl1(game.Dice, game.RollCount, this.columns);


             List<int> bestCell = CellCalculator.GetBestCell(values);
             SetDebug(values);

             if (CellCalculator.CalculateDiceKeepLvl1(bestCell, game.Dice, game.RollCount)){
                game.SetAllDiceImages();
                keepRolling = false;
                 await Task.Delay(2000);
                 WriteToCell(bestCell[2], bestCell[0]);
                 return;

             }

             game.SetAllDiceImages();

        }



    }
}
