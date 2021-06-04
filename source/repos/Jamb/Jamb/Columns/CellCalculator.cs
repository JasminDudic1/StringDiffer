using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;


namespace Jamb.Columns
{
    class CellCalculator
    {

        public static int CalculateCellValue(int index, List<Dice> dice, int rollCount)
        {

            int[] values = MakeValues(dice);

            if (index < 6) return CalculateNumber(index + 1, values);
            if (index == 7) return CalculateExtreme(values, true);
            if (index == 8) return CalculateExtreme(values, false);
            if (index == 10) return CalculateStraight(values, rollCount);
            if (index == 11) return CalculateThree(values);
            if (index == 12) return CalculateFull(values);
            if (index == 13) return CalculatFour(values);
            if (index == 14) return CalculateFive(values);

            return -1;


        }

        private static int CalculateNumber(int number, int[] values)
        {
            return number * values[number];
        }

        private static int CalculateExtreme(int[] values, bool isMax = true)
        {

            int sum = 0;
            int max = 1;
            int min = 6;


            for (int i = 1; i <= 6; i++)
            {
                if (values[i] != 0 && i > max) max = i;
                if (values[i] != 0 && i < min) min = i;
                sum += i * values[i];
            }

            if (isMax) sum -= min;
            else sum -= max;

            return sum;

        }

        private static int CalculateStraight(int[] values, int rollCount)
        {

            int i = 0;

            for (i = 1; i <= 5; i++)
                if (values[i] == 0) break;
            if (i == 6) return 76 - rollCount * 10;

            for (i = 2; i <= 6; i++)
                if (values[i] == 0) break;
            if (i == 7) return 76 - rollCount * 10;

            return 0;


        }

        private static int CalculateThree(int[] values)
        {

            for (int i = 6; i > 0; i--)
            {
                if (values[i] >= 3) return 3 * i + 20;
            }
            return 0;

        }

        private static int CalculateFull(int[] values)
        {

            int three = 0;
            int two = 0;

            for (int i = 6; i > 0; i--)
            {
                if (values[i] >= 3 && three == 0) three = i;
                else if (values[i] >= 2) two = i;
            }

            if (three == 0 || two == 0) return 0;
            return 3 * three + 2 * two + 30;

        }

        private static int CalculatFour(int[] values)
        {

            for (int i = 6; i > 0; i--)
            {
                if (values[i] >= 4) return 4 * i + 40;
            }
            return 0;
        }

        private static int CalculateFive(int[] values)
        {

            for (int i = 6; i > 0; i--)
            {
                if (values[i] >= 5) return 5 * i + 50;
            }
            return 0;
        }

        public static void CalcualteSums(Label[] labels, int[] values)
        {

            int sum = 0;
            int temp = 0;
            for (int i = 0; i < 6; i++)
                if (values[i] >= 0) sum += values[i];
                else temp = 1;

            if (temp == 0) labels[6].BackColor = Color.Gray;
            if (sum >= 60) sum += 30;

            values[6] = sum;
            labels[6].Text = values[6] + "";

            if (values[0] > 0 && values[7] > 0 && values[8] > 0)
            {
                sum = (values[7] - values[8]) * values[0];
                if (sum < 0) sum = 0;
                if (sum >= 60) sum += 30;
                labels[9].BackColor = Color.Gray;
            }
            else sum = 0;

            values[9] = sum;
            labels[9].Text = values[9] + "";

            sum = 0;
            temp = 0;
            for (int i = 10; i < 15; i++)
                if (values[i] >= 0) sum += values[i];
                else temp = 1;
            if (temp == 0) labels[15].BackColor = Color.Gray;
            values[15] = sum;

            labels[15].Text = values[15] + "";

        }

        public static int GetMax(int row)
        {

            int[] max = { 5, 10, 15, 20, 25, 30, 0, 30, 5, 0, 66, 38, 58, 64, 80, 0 };
            return max[row];
        }

        public static double[] GetRowValuesLvl1(List<Dice> dice, int rollCount)
        {

            double[] returnArray = new double[14];

            int[] values = MakeValues(dice);

            for (int i = 0; i < 6; i++)
                returnArray[i] = 10 * values[i + 1];

            if (returnArray[1] <= 20) returnArray[1] += 5;

            for (int i = 3; i < 6; i++)
                if (values[i + 1] < 3 && values[i + 1] > 0) returnArray[i] -= (int)((0.85 * i) / values[i + 1]);


            double temp = CalculateExtreme(values);
            if (temp < 27) returnArray[6] = 25 - (25 - temp) * 10;
            else returnArray[6] = 30 + (temp - 26) * 10;


            temp = CalculateExtreme(values, false);
            if (temp > 9) returnArray[7] = 25 - (temp - 9) * 10;
            else returnArray[7] = 30 + (9 - temp) * 10;

            temp = CalculateStraight(values, rollCount);
            if (temp == 66) returnArray[8] = 70;
            else if (temp == 56) returnArray[8] = 50;
            else if (temp == 46) returnArray[8] = 30;

            temp = CalculateThree(values);
            if (temp == 0) returnArray[9] = 0;
            else
            {
                if (temp >= 32)
                    returnArray[9] = temp;
                else returnArray[9] = temp * 0.7;
            }


            temp = CalculateFull(values)*1.1;

            if (temp == 0) returnArray[10] = 0;
            else
            {
                if (temp >= 48)
                    returnArray[10] = temp;
                else returnArray[10] = temp * 0.5;
            }

            temp = CalculatFour(values);
            if (temp == 0) returnArray[11] = 0;
            {
                if (temp >= 50)
                    returnArray[11] = temp;
                else returnArray[11] = temp * 0.5;
            }

            temp = CalculateFive(values);
            if (temp == 0) returnArray[12] = 0;
            {
                if (temp >= 65)
                    returnArray[12] = temp;
                else returnArray[12] = temp * 0.8;
            }

            return returnArray;
        }

        public static double[][] GetRowsValuesLvl1(List<Dice> dice, int rollCount, BaseColumn[] columns)
        {

            double[][] returnMatrix = new double[columns.Length][];

            double[] temp = GetRowValuesLvl1(dice, rollCount);

            for (int i = 0; i < columns.Length; i++)
            {
                returnMatrix[i] = new double[14];
                returnMatrix[i] = (double[])temp.Clone();
                returnMatrix[i][13] = columns[i].ColumnIndex;

                SetColumnRowValues(returnMatrix[i], MakeValues(dice), columns[i], rollCount);
            }


            return returnMatrix;

        }

        public static void SetColumnRowValues(double[] calculatedValues,int[] diceValues, BaseColumn column, int rollCount)
        {

            int index = column.ColumnIndex;
            if(index==6 && rollCount != 1)
            {
                for(int i = 0; i < 13; i++)
                {
                    calculatedValues[i] = 0;
                }
                return;
            }

            for (int i = 0; i < 6; i++)
            {
                if (column.getValues()[i] != -1 || !column.Writable(i))
                {
                    calculatedValues[i] = -100;
                    continue;
                }

                if(diceValues[i + 1] >= 3)
                {

                    if(i==0) calculatedValues[i] *= diceValues[i + 1]/2;
                    else calculatedValues[i] *= 1.3;

                }

                if (column.getValues()[6] + diceValues[i+1] * i >= 60) calculatedValues[i] *= 1.3;

                if (index == 1)
                    calculatedValues[i] *= 1.1;

                if (index == 6)
                    if (rollCount == 1 && diceValues[i + 1] >= 3) calculatedValues[i] *= 1.4;
                    else calculatedValues[i] = 0;

                if (index == 8)
                    if (rollCount == 1 && diceValues[i + 1] >= 2) calculatedValues[i] *= 1.2;
                    else calculatedValues[i] = 0;

            }


            //check if ones are enough to pull out the max min
            if(column.Writable(0) && column.getValues()[0] == -1 )
            {
                //if they are written in
                if (column.getValues()[7] != -1 && column.getValues()[8] != -1)
                {
                    if (diceValues[1] * (column.getValues()[7] - column.getValues()[8]) >= 60)
                        if (index == 6 || index == 8)
                            calculatedValues[7] *= 2;
                        else calculatedValues[0]*= 1.5;
                    else calculatedValues[0]*= 0.6;
                }
                else if (column.getValues()[7] != -1)
                {

                    if(column.getValues()[0]==3 && column.getValues()[7]>=26 
                    || column.getValues()[0] == 4 && column.getValues()[7] >= 21
                    || column.getValues()[0] == 4 && column.getValues()[7] >= 18)
                        calculatedValues[0] *= 1.3;
                    else calculatedValues[0]*= 0.8;

                }
                else if (column.getValues()[8] != -1)
                {

                    if (column.getValues()[0] == 3 && column.getValues()[8] <=9
                    || column.getValues()[0] == 4 && column.getValues()[8] <=14
                    || column.getValues()[0] == 4 && column.getValues()[8] <= 17)
                        calculatedValues[0] *= 1.3;
                    else calculatedValues[0] *= 0.8;

                }

            }
            //check if max is enough to pull out the max min
            else if (column.Writable(8) && column.getValues()[7] == -1)
            {
                int max = CalculateExtreme(diceValues);
                //check if min is not zero
                if (column.getValues()[8] != -1)
                {
                    //check if ones are not zero
                    if (column.getValues()[0] != -1)
                    {
                        if (column.getValues()[0] * (max - column.getValues()[8]) >= 60)
                        {
                            if (index == 6 || index == 8)
                                calculatedValues[6] *= 2;
                            else calculatedValues[6] *= 1.5;
                        }
                        else calculatedValues[6] *= 0.5;

                    }
                    //if ones are zero check if difference is big
                    else
                    {
                        if ((max - column.getValues()[8]) >= 20)
                            calculatedValues[6] *= 1.5;
                        else if ((max - column.getValues()[8]) >= 15)
                            calculatedValues[6] *= 1.2;
                        else if ((max - column.getValues()[8]) >= 10)
                            calculatedValues[6] *= 0.7;
                    }

                }
                //if min is zero calculate max and ones
                else
                {
                    if (column.getValues()[0] == 3 && max >= 27
                    || column.getValues()[0] == 4 && max >= 22
                    || column.getValues()[0] == 5 && max >= 19)
                 calculatedValues[6] *= 1.3;
                }
            }

            else if (column.Writable(9) && column.getValues()[8] == -1)
            {
                int min = CalculateExtreme(diceValues, false);
                //check if max is not zero
                if (column.getValues()[7] != -1)
                {
                   
                    if (column.getValues()[0] != -1)
                    {
                        if (column.getValues()[0] * (column.getValues()[7] - min) >= 60)
                        {
                            if (index == 6 || index == 8)
                                calculatedValues[7] *= 2;
                            else calculatedValues[7] *= 1.5;
                        }
                        else calculatedValues[7] *= 0.5;
                    }
                    else
                    {
                        if ((column.getValues()[7] - min) >= 20)
                            calculatedValues[7] *= 1.4;
                        else if ((column.getValues()[7] - min) >= 15)
                            calculatedValues[7] *= 1.1;
                        else if ((column.getValues()[7] - min) >= 10)
                            calculatedValues[7] *= 0.7;
                    }

                }
                else
                {
                    if (column.getValues()[0] == 3 && min <= 9
                          || column.getValues()[0] == 4 && min <= 14
                          || column.getValues()[0] == 5 && min <= 17)
                        calculatedValues[7] *= 1.3;
                    else calculatedValues[7] *= 0.7;
                }
            }

            for (int i = 6; i < 8; i++)
                if (!column.Writable(i+1)) calculatedValues[i] = -100;

            if( (index==6 || index == 8) && rollCount == 1)
            {
                if (calculatedValues[6] >= 27) calculatedValues[6] *= 1.2;
                if (calculatedValues[7] <= 8) calculatedValues[6] *= 1.2;
                if (calculatedValues[0] >=4) calculatedValues[6] *= 1.3;
            }

            //straight
            if (column.Writable(10) && column.getValues()[10] == -1 && rollCount==1)
            {
                if (index == 6)
                    calculatedValues[8] *= 1.2;
                else if (index == 8)
                    calculatedValues[8] *= 1.3;
            }

            if( (index==6 || index == 8)&& rollCount==1 || index==3)
            {
                calculatedValues[9] *= 1.1;
                calculatedValues[10] *= 1.1;
                calculatedValues[11] *= 1.1;
                calculatedValues[12] *= 1.2;
            }

            

            for (int i = 8; i < 13; i++)
                if (!column.Writable(i + 2)) calculatedValues[i] = -100;

        }

        private static int[] MakeValues(List<Dice> dice)
        {
            int[] values = { 0, 0, 0, 0, 0, 0, 0 };
            for (int i = 0; i < 6; i++)
                if (values[dice[i].Value] < 6)
                    values[dice[i].Value]++;

            return values;
        }

        public static List<int> GetBestCell(double[][] matrix)
        {

            List<int> returnList = new List<int>();

            double max = -100;
            int maxRow = 0;
            int maxIndex = 0;
            int index = -5;

            for(int i = 0; i < matrix.Length; i++)
            {

                for(int j = 0; j < 13; j++)
                {

                    if (matrix[i][j] > max)
                    {
                        max = matrix[i][j];
                        maxRow = j;
                        maxIndex =(int)(matrix[i][13]);
                        index = i+1;
                    }

                }


            }

            returnList.Add(maxRow);
            returnList.Add(maxIndex);
            returnList.Add(index);
            returnList.Add((int)max);

            return returnList;


        }

        public static bool CalculateDiceKeepLvl1(List<int> bestCell, List<Dice> dice, int rollCount)
        {

            if (bestCell[3] >= 40 && (bestCell[1] == 6 || bestCell[1] == 8))
            {
                return true;
            }
            if (rollCount == 3) return true;



            KeepNone(dice);

            if (bestCell [0]>= 0 && bestCell[0] <= 5)
                KeepNumber(dice, bestCell[0] + 1);

            if (bestCell[0] == 6) KeepHigh(dice);
            if (bestCell[0] == 7) KeepLow(dice);
            if (bestCell[0] == 8) return true;
            if (bestCell[0] == 9) KeepN(dice, 3);
            if (bestCell[0] == 10) KeepFull(dice);
            if (bestCell[0] == 11) KeepN(dice, 4);
            if (bestCell[0] == 12) KeepN(dice, 5);


            return false;
        }

        private static void KeepN(List<Dice> dice,int n)
        {
            int[] values = MakeValues(dice);

            for(int i = 6; i > 0; i--)
            {
                if (values[i] == n)
                {
                    KeepNumber(dice, i);
                    return;
                }
            }

        }

        private static void KeepFull(List<Dice> dice)
        {
            int[] values = MakeValues(dice);
            int three = 0;
            int two = 0;

            for (int i = 6; i > 0; i--)
            {
                if (values[i] >= 3 && three==0)
                {
                    three = i;
                    if (two != 0 && three > two)
                        KeepNumber(dice, i);
                    else KeepNumber(dice, i, 3);

                }
                else if (values[i] >= 2 && two==0)
                {
                    two = i;
                    if(three!=0 && two >three)
                    KeepNumber(dice, i);
                    else KeepNumber(dice, i, 2);
                }
            }

        }

        private static void KeepLow(List<Dice> dice)
        {
            int[] values = MakeValues(dice);

            int max = 0;

            for (int i = 6; i > 0; i--)
                if (values[i] != 0 && i > max) max = i;

            KeepAll(dice);

            for(int i = 0; i < dice.Count; i++)
            {
                if(dice[i].Value == max)
                {
                    dice[i].Rollable = true;
                    return;
                }
            }

        }

        private static void KeepHigh(List<Dice> dice)
        {
            int[] values = MakeValues(dice);

            int min = 6;

            for (int i = 1; i<=6; i++)
                if (values[i] != 0 && i < min) min = i;

            KeepAll(dice);

            for (int i = 0; i < dice.Count; i++)
            {
                if (dice[i].Value == min)
                {
                    dice[i].Rollable = true;
                    return;
                }
            }

        }

        private static void KeepNumber(List<Dice> dice, int number, int times = 5)
        {

            for (int i = 0; i < dice.Count; i++)
                if (dice[i].Value == number && times !=0)
                {
                    times--;
                    dice[i].Rollable = false;
                }

        }

        private static void KeepAll(List<Dice> dice)
        {
            for (int i = 0; i < dice.Count; i++)
                dice[i].Rollable = false;

        }

        private static void KeepNone(List<Dice> dice)
        {
            for (int i = 0; i < dice.Count; i++)
                dice[i].Rollable = true;
        }

    }
}
