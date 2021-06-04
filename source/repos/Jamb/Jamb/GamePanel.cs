using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using Jamb.Players;
using Jamb.Columns;

namespace Jamb
{
    class GamePanel
    {

        Random rnd = new Random();

        private Panel mainPanel;
        private Panel leftPanel;
        private Panel rightPanel;
        private Button rollButton;

        private List<Dice> dice;

        private ColumnLabels labelsLeft;
        private ColumnLabels labelsRight;

        public int currentPlayer = 0;
        private int rollCount = 0;


        public Label forcedLabel = null;
        public int forcedIndex = -1;
        public int forcedRow = -1;
        public int forcedType = -1;//1 is for order

        private bool forcedPick = false;

        private bool gameEnd = false;

        private int turn = 0;

        private bool rollWait = false;

        ImageList imageList;

        private BasePlayer[] players;

        private Label debugLabel;

        public GamePanel(ImageList imageList, List<int> columns)
        {
            
            this.imageList = imageList;

            mainPanel = new Panel();
            mainPanel.Location = new System.Drawing.Point((int)(0.002 * StaticData.windowWidth), (int)(0.002 * StaticData.windowHeight));
            mainPanel.Size = new System.Drawing.Size((int)(0.98 * StaticData.windowWidth), (int)(0.98 * StaticData.windowHeight));
            mainPanel.Visible = true;
            mainPanel.BringToFront();

            leftPanel = new Panel();
            leftPanel.Location = new Point((int)(0.002 * StaticData.windowWidth), (int)(0.002 * StaticData.windowHeight));
            leftPanel.Size = new Size( (int)(0.398 * StaticData.windowWidth), (int)(0.94 * StaticData.windowHeight));
            leftPanel.BackColor = Color.LightSeaGreen;
            leftPanel.Parent = mainPanel;

            Label sumLabelLeft = new Label();
            sumLabelLeft.Location = new Point((int)(0.3 * leftPanel.Width), (int)(0.92 * leftPanel.Height));
            sumLabelLeft.Size = new Size((int)(0.08 * StaticData.windowWidth), (int)(0.05 * StaticData.windowHeight));
            sumLabelLeft.Parent = leftPanel;
            sumLabelLeft.Font = new Font("Arial", 12);
            sumLabelLeft.BorderStyle = BorderStyle.FixedSingle;
            sumLabelLeft.TextAlign = ContentAlignment.MiddleCenter;

            rightPanel = new Panel();
            rightPanel.Location = new Point((int)(0.6 * StaticData.windowWidth), (int)(0.002 * StaticData.windowHeight));
            rightPanel.Size = new Size((int)(0.4 * StaticData.windowWidth), (int)(0.94 * StaticData.windowHeight));
            rightPanel.BackColor = Color.LightSalmon;
            rightPanel.Parent = mainPanel;

            Label sumLebelRight = new Label();
            sumLebelRight.Location = new Point((int)(0.3 * rightPanel.Width), (int)(0.92 * rightPanel.Height));
            sumLebelRight.Size = new Size((int)(0.08 * StaticData.windowWidth), (int)(0.05 * StaticData.windowHeight));
            sumLebelRight.Parent = rightPanel;
            sumLebelRight.Font = new Font("Arial", 12);
            sumLebelRight.BorderStyle = BorderStyle.FixedSingle;
            sumLebelRight.TextAlign = ContentAlignment.MiddleCenter;

            dice = new List<Dice>();

            for (int i = 0; i < 6; i++)
            {
                Dice die = new Dice(i,this);
                die.DiePicture.Parent = mainPanel;  
                dice.Add(die);
            }

            
 
            rollButton = new Button();
            rollButton.Parent = mainPanel;
            rollButton.Location = new Point((int)(0.4 * StaticData.windowWidth), (int)(0.85 * StaticData.windowHeight));
            rollButton.Size = new Size((int)(0.2 * StaticData.windowWidth), (int)(0.05 * StaticData.windowHeight));
            rollButton.Text = "Roll";

            debugLabel = new Label();
            debugLabel.Parent = mainPanel;
            debugLabel.Location = new Point((int)(0.4 * StaticData.windowWidth), (int)(0.587 * StaticData.windowHeight));
            debugLabel.Size = new Size((int)(0.2 * StaticData.windowWidth), (int)(0.25 * StaticData.windowHeight));
            debugLabel.Font = new Font("Courier New", 7.3f);

            RollButton.Click += RollButton_Click;

            labelsLeft = new ColumnLabels(leftPanel);
            labelsRight = new ColumnLabels(rightPanel);

            players = new BasePlayer[2];

            players[0] = new BasePlayer(this,sumLabelLeft);
            //players[1] = new BasePlayer(this);
            players[1] = new Bot1(this,sumLebelRight);

            

            #region ColumnSetup

            columns.Sort();

            int size = columns.Count;
            if (columns.Contains(7)) size++;

            BaseColumn[] playerOneColumns = new BaseColumn[size];
            BaseColumn[] playerTwoColumns = new BaseColumn[size];

            for (int i = 0; i < size; i++)
            {



                switch (columns[i])
                {
                    case 0:
                        playerOneColumns[i] = new Columns.DownColumn(leftPanel, i + 1, this, 0,players[0]);
                        playerTwoColumns[i] = new Columns.DownColumn(rightPanel, i + 1, this, 1,players[1]);
                        break;
                    case 1:
                        playerOneColumns[i] = new Columns.FreeColumn(leftPanel, i + 1, this, 0, players[0]);
                        playerTwoColumns[i] = new Columns.FreeColumn(rightPanel, i + 1, this, 1, players[1]);
                        break;
                    case 2:
                        playerOneColumns[i] = new Columns.UpColumn(leftPanel, i + 1, this, 0, players[0]);
                        playerTwoColumns[i] = new Columns.UpColumn(rightPanel, i + 1, this, 1, players[1]);
                        break;
                    case 3:
                        playerOneColumns[i] = new Columns.ToMiddleColumn(leftPanel, i + 1, this, 0, players[0]);
                        playerTwoColumns[i] = new Columns.ToMiddleColumn(rightPanel, i + 1, this, 1, players[1]);
                        break;
                    case 4:
                        playerOneColumns[i] = new Columns.FromMiddleColumn(leftPanel, i + 1, this, 0, players[0]);
                        playerTwoColumns[i] = new Columns.FromMiddleColumn(rightPanel, i + 1, this, 1, players[1]);
                        break;
                    case 5:
                        playerOneColumns[i] = new Columns.FirstThrowColumn(leftPanel, i + 1, this, 0, players[0]);
                        playerTwoColumns[i] = new Columns.FirstThrowColumn(rightPanel, i + 1, this, 1, players[1]);
                        break;
                    case 6:
                        playerOneColumns[i] = new Columns.MaxColumn(leftPanel, i + 1, this, 0, players[0]);
                        playerTwoColumns[i] = new Columns.MaxColumn(rightPanel, i + 1, this, 1, players[1]);
                        break;
                    case 7:
                        playerOneColumns[i] = new Columns.OrderColumn(leftPanel, i + 1, this, 0, players[0]);
                        playerTwoColumns[i] = new Columns.OrderColumn(rightPanel, i + 1, this, 1, players[1]);
                        i++;
                        playerOneColumns[i] = new Columns.AnswerColumn(leftPanel, i + 1, this, 0, players[0]);
                        playerTwoColumns[i] = new Columns.AnswerColumn(rightPanel, i + 1, this, 1, players[1]);
                        break;

                    default:
                        break;
                }

            }

            #endregion

            players[0].Columns = playerOneColumns;
            players[1].Columns = playerTwoColumns;

            ResetAll(false);
            currentPlayer = 0;

        }

        private void RollButton_Click(object sender, EventArgs e)
        {

            players[currentPlayer].RollClicked();

        }

        public async void RollDice()
        {
            if (rollWait || gameEnd) return;

            int i = 0;
            for (i = 0; i < 6; i++)
                if (dice[i].Rollable == true) break;

            if (i == 6) return;

            RollCount++;
            if (!CheckIfRollable())
            {
                MessageBox.Show("Can not roll ");
                RollCount--;
                return;
            }
            
            if (RollCount > 3) return;
         

            for (i = 0; i < 10; i++)
            {
                for (int j = 0; j < 6; j++)
                {
                    SetDiceImage(j, rnd.Next(1, 7));
                }
                await Task.Delay(10);
            }

            RollButton.Text = "Roll: " + rollCount;

            players[currentPlayer].AfterRoll();
            forcedPick = true;
            FillColumns();
        }
        public bool WriteCell(int index,int row, int player)
        {
            if (player != currentPlayer || rollCount==0) return false;
            if (players[currentPlayer].Columns[index-1].WriteToCell(row))
            {
                ResetAll();
                return true;
            }
            else
            {
                return false;
            }
        }
        private async void ResetAll(bool turnStart=true)
        {
            
            rollWait = true;
            currentPlayer = ++currentPlayer % 2;


            if (turn !=0 && forcedLabel==null)
            {
                RollCount = 1;

                CheckWritables();

                if (forcedPick)
                {
                    debugLabel.Text = "Igrac"+currentPlayer+" nema potez";

                    currentPlayer = ++currentPlayer % 2;
                    CheckWritables();
                    if (forcedPick)
                    {
                        debugLabel.Text = "Nemaju oba potez";
                        GameEnd();
                    }
                    else
                    {
                        debugLabel.Text = currentPlayer + " Nema poteza ali ima drugi igrac";
                        //idk nothing i guess
                    }
                }//else debugLabel.Text = "Igrac "+currentPlayer+" Ima u potezu "+turn;

            }
            turn++;
            RollCount = 0;
 
            ResetDice();
            ResetColumns();
            rollWait = false;
           if(turnStart) players[currentPlayer].TurnStart();

        }
        private bool CheckIfRollable()
        {
            forcedPick = true;
            if (forcedLabel != null) return true;

            //checks if the current player has writable cells
            CheckWritables();
            //if he deos forced pick will be false
            //if he doesnt forced pick will be true
            return !forcedPick;

        } 
        private void GameEnd()
        {
            gameEnd = true;

            int sum1 = players[0].GetSum();
            int sum2 = players[1].GetSum();

            MessageBox.Show(sum1 + " : " + sum2);
            



        }
        private void CheckWritables()
        {
            forcedPick = true;

            for (int j = 0; j < players[currentPlayer].Columns.Length; j++)
            {
                List<int> temp = players[currentPlayer].Columns[j].GetWritables();
                if ( temp.Count!= 0)
                    forcedPick = false;
            }

            

        }
        private void ResetColumns()
        {
            for (int i = 0; i < 2; i++)
            {

                for (int j = 0; j < players[currentPlayer].Columns.Length; j++)
                {
                    players[i].Columns[j].ResetCells();
                }

            }
        }
        private void ResetDice()
        {

            for(int i = 0; i < 6; i++)
            {
                dice[i].DiePicture.Image = imageList.Images[0];
                dice[i].Value = 0;
                dice[i].Rollable = true;
            }
            forcedPick = false;
            RollButton.Text = "Roll" ;

        }
        public void diceClicked(Dice d)
        {
            if (d.Value == 0) return;
            d.Rollable = !d.Rollable;
            if (!d.Rollable)
            {
                d.DiePicture.Image = imageList.Images[d.Value + 6];
            }
            else
            {
               d.DiePicture.Image = imageList.Images[d.Value];
            }

        }
        public void SetDiceImage(int index,int value)
        {

            if (!dice[index].Rollable) return;
            dice[index].DiePicture.Image = imageList.Images[value];
            dice[index].Value = value;

        }
        public void SetAllDiceImages()
        {
            for(int i = 0; i < dice.Count; i++)
            {
                if (!dice[i].Rollable)
                {
                    dice[i].DiePicture.Image = imageList.Images[dice[i].Value + 6];
                }
                else
                {
                    dice[i].DiePicture.Image = imageList.Images[dice[i].Value];
                }
            }
        }
        public void FillColumns()
        {
            for (int j = 0; j < players[currentPlayer].Columns.Length; j++)
                players[currentPlayer].Columns[j].CheckDice(this.dice);
            CheckWritables();
            
            
        }
        public Panel MainPanel { get => mainPanel; set => mainPanel = value; }
        public Panel LeftPanel { get => leftPanel; set => leftPanel = value; }
        public Panel RightPanel { get => rightPanel; set => rightPanel = value; }
        public Button RollButton { get => rollButton; set => rollButton = value; }
        internal List<Dice> Dice { get => dice; set => dice = value; }
        public int RollCount { get => rollCount; set => rollCount = value; }
        public Label DebugLabel { get => debugLabel; set => debugLabel = value; }
    }
}
