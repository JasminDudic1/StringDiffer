using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using Jamb.Players;

namespace Jamb.Columns
{
    class BaseColumn
    {
        protected Label[] labels;
        protected int[] values;
        protected int[] calculatedValues;
        protected int index = 0;
        protected Label nameLabel;
        protected GamePanel game;
        protected int playerNumber;
        protected BasePlayer player;

        private int columnIndex;

        public int ColumnIndex { get => columnIndex; set => columnIndex = value; }

        public BaseColumn(Panel panel, int index, GamePanel game, int playerNumber,BasePlayer player)
        {
            this.index = index;
            this.game = game;
            this.playerNumber = playerNumber;
            this.player = player;

            labels = new Label[16];
            values = new int[16];
            calculatedValues = new int[16];
            nameLabel = new Label();


            ColumnBuilder.BuildColumn(panel, index, this);

            for (int i = 0; i < 16; i++)
            {
                labels[i].Click += LabelClick;
                values[i] = -1;
                calculatedValues[i] = -1;
            }
        }

        private void LabelClick(object sender, EventArgs e)
        {
            int row = Array.IndexOf(labels, (sender as Label));

            player.LabelClick(index, row, playerNumber);

        }

        public virtual void CheckDice(List<Dice> dice)
        {
            int rollCount = game.RollCount;
            for (int i = 0; i < 15; i++)
            {
                calculatedValues[i] = -1;

                if (!Writable(i) || (game.forcedLabel != null && labels[i] != game.forcedLabel))
                {
                    if (values[i] == -1) labels[i].Text = "";
                    continue;
                }

                int value = CellCalculator.CalculateCellValue(i, dice, rollCount);
                if (value == -1) continue;

                labels[i].Text = value + " ";
                calculatedValues[i] = value;

            }


        }

        public virtual int GetAllSum()
        {
            return values[6] + values[9] + values[15];
        }

        public Label[] getLabels()
        {
            return labels;
        }

        public Label getNameLabel()
        {
            return nameLabel;
        }

        public int[] getValues()
        {
            return values;
        }

        public virtual bool Writable(int row)
        {

            if (values[row] != -1) return false;

            if (row == 6 || row == 9 || row == 15) return false;

            return true; 
        }

        public virtual void AfterWriting()
        {

        }

        public virtual string ReturnName()
        {
            return "Empty";
        }

        public virtual string Description()
        {
            return "Empty Desc";
        }

        public virtual bool WriteToCell(int row )
        {

            if (!Writable(row)) return false;


            if (game.forcedLabel != null && game.forcedLabel != labels[row]) return false;

            if (calculatedValues[row] == -1) return false;
            values[row] = Convert.ToInt32(labels[row].Text);
            labels[row].BackColor = Color.White;
            AfterWriting();
            return true;
        }

        public virtual void ResetCells()
        {
            for (int i = 0; i < 16; i++)
                if (values[i] == -1) labels[i].Text = "";
            CellCalculator.CalcualteSums(labels, values);
            AfterReset();
        }

        public virtual void AfterReset()
        {

        }



        public List<int> GetWritables()
        {

            List<int> returnList = new List<int>();

            for(int i = 0; i < 16; i++)
                if (Writable(i)) returnList.Add(i);

            return returnList;
        }

    }
}
