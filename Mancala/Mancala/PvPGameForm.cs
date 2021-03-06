using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Mancala
{
    public partial class PvPGameForm : Form
    {
        MenuForm _menuForm;
        int _round = 1; //1 = first player, 2 = second player
        bool _moved = false;
        Label[] _pits; //pit6 - store for player 1, pit13 - store for player2
        PictureBox[] _pb;
        int _lastPitIndex = -1;
        Random _rnd = new Random();

        public PvPGameForm(MenuForm menuForm)
        {
            InitializeComponent();
            _menuForm = menuForm;
            _pits = new Label[14] { pit0, pit1, pit2, pit3, pit4, pit5, pit6, pit7, pit8, pit9, pit10, pit11, pit12, pit13 };
            _pb = new PictureBox[14] { pit0pb, pit1pb, pit2pb, pit3pb, pit4pb, pit5pb, pit6pb, pit7pb, pit8pb, pit9pb, pit10pb, pit11pb, pit12pb, pit13pb };
            //set all to pits to be unclickable until Start Game
            SetAllPitsDisabled();
            logGameConsole.Text = "";
        }

        private void GameForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            _menuForm.Show();
        }

        private void startGameBtn_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < _pits.Length; i++)
            {
                if (i == 6 || i == 13)
                {
                    SetPitCount(_pits[i], 0);
                }
                else
                {
                    SetPitCount(_pits[i], 4);
                }
            }
            //start the game by making the player's 1 pits available to click
            startGameBtn.Hide();
            SetPits1Enable();
        }

        //event on clicking on the pit
        void pit_Click(object sender, EventArgs e)
        {
            _moved = true;
            Label label = sender as Label;
            PictureBox pb = sender as PictureBox;
            //if the click was on picture box, get the label name
            if (label == null)
            {
                string name = pb.Name.Remove(pb.Name.Length - 2, 2);
                label = GetPitByName(name);
            }
            if (GetPitCount(label) == 0)
            {
                _moved = false;
                return;
            }

            if(_round % 2 == 1)
            {
                LOG($"Player1 Clicked p{GetPitIndex(label)} with {GetPitCount(label)} rocks!");
            }
            else
            {
                LOG($"Player2 Clicked p{GetPitIndex(label)} with {GetPitCount(label)} rocks!");
            }
            

            //make the count of rocks 0 in the clicked pit, and start sharing them to the next pits
            ShareCount(label);
            //if the last rock was dropped in the player's store, permit another move, else disable all pits until the player hits Next 
            if (!(_lastPitIndex == 6 || _lastPitIndex == 13))
            {
                SetAllPitsDisabled();
            }
            else
            {
                _moved = false;
            }
        }

        private void next_Click(object sender, EventArgs e)
        {
            if (!_moved)
            {
                MessageBox.Show("Please make a move!");
                return;
            }
            LOG("-------------------------------------------------");
            //we count the rounds (par - player 1, impar - player 2)
            _round++;
            if (_round % 2 == 1)
            {
                label1.Text = "Player 1 Moves";
                SetPits1Enable();
                SetPits2Disable();
            }
            else
            {
                label1.Text = "Player 2 Moves";
                SetPits2Enable();
                SetPits1Disable();
            }
            _moved = false;
        }

        void ShareCount(Label label)
        {
            int count = GetPitCount(label);
            bool passedStore = false;
            SetPitCount(label, 0);

            //get the index of the next pit after the clicked pit
            int nextPitIndex = GetPitIndex(label) + 1;

            //start putting 1 rock in the other pits (as many as the clicked pit count)
            do
            {
                //we only have 14 pits so we do not want to overflow the number
                nextPitIndex = nextPitIndex % 14;

                //the rocks are not distributed in the opponent's store
                if (_round % 2 == 1 && nextPitIndex == 13)
                {
                    nextPitIndex = 0;
                }
                else if (_round % 2 == 0 && nextPitIndex == 6)
                {
                    nextPitIndex = 7;
                }
                if (_round % 2 == 1 && nextPitIndex == 6)
                {
                    passedStore = true;
                }
                else if (_round % 2 == 0 && nextPitIndex == 13)
                {
                    passedStore = true;
                }

                //add 1 rock to the next pit and substract it from the count of the clicked pit
                int val = GetPitCount(_pits[nextPitIndex]) + 1;
                SetPitCount(_pits[nextPitIndex], val);

                nextPitIndex++;
                count--;
            }
            while (count != 0);

            //get the pit where it was placed the last rock from the clicked pit
            _lastPitIndex = nextPitIndex - 1;

            //if the last pit is 1 (it was 0 + the last rock), if it is not one of the stores and if it did not drop a rock in the store,
            // then get the opposite pit's rocks and put them in your store
            if (GetPitCount(_pits[_lastPitIndex]) == 1 && !(_lastPitIndex == 6 || _lastPitIndex == 13) && !passedStore)
            {
                //calculate the opposite pit
                int oppositeIndex = 12 - _lastPitIndex;

                //if there are rocks in the opposite pit, add them to your store, along with the current 1 rock in the last pit
                if (GetPitCount(_pits[oppositeIndex]) != 0)
                {
                    if (_round % 2 == 1)
                    {
                        int val = GetPitCount(_pits[6]) + GetPitCount(_pits[oppositeIndex]) + GetPitCount(_pits[_lastPitIndex]);
                        SetPitCount(_pits[6], val);
                    }
                    else
                    {
                        int val = GetPitCount(_pits[13]) + GetPitCount(_pits[oppositeIndex]) + GetPitCount(_pits[_lastPitIndex]);
                        SetPitCount(_pits[13], val);
                    }
                    //set to 0 and draw 0 rocks in the last pit and it's opposite 
                    LOG($"Captured p{_lastPitIndex} -> p{oppositeIndex}");
                    SetPitCount(_pits[oppositeIndex], 0);
                    SetPitCount(_pits[_lastPitIndex], 0);
                }
            }
            //after each move, the end game is checked 
            if (CheckEndGame())
            {
                EndGame();
            }
        }

        int GetPitIndex(Label label)
        {
            return int.Parse(label.Name.Remove(0, 3));
        }

        int GetPitCount(Label label)
        {
            return int.Parse(label.Text);
        }

        Label GetPitByName(string name)
        {
            return this.Controls.Find(name, true).FirstOrDefault() as Label;
        }

        void SetPitCount(Label label, int count)
        {
            //set the new number of rocks (actual + 1)
            label.Text = count.ToString();
            //draw the number of rocks
            DrawRocks(label);
        }

        void DrawRocks(Label label)
        {
            int count = GetPitCount(label);
            //get the picture box related to the clicked pit
            PictureBox pb = this.Controls.Find(label.Name + "pb", true).FirstOrDefault() as PictureBox;

            //Clear the clicked pit
            System.Drawing.Graphics g = System.Drawing.Graphics.FromHwnd(pb.Handle);
            Rectangle rect = new Rectangle(0, 0, 85, 85);
            g.FillRectangle(new SolidBrush(Color.Transparent), rect);
            pb.Refresh();


            if (count == 0)
            {
                return;
            }

            //stuff to calculate the positions of rocks in pits and the if is in case of stores
            int x = 15, y = 25;
            int startx = 15;
            int pas = 15;
            int limit = 75;
            if (label == _pits[6] || label == _pits[13])
            {
                startx = 30;
                x = 30;
                y = 45;
            }


            //draw as many rocks as the count
            do
            {
                rect = new Rectangle(x, y, 10, 10);
                Color color = Color.FromArgb(_rnd.Next(256), _rnd.Next(256), _rnd.Next(256));
                g.DrawEllipse(Pens.Black, rect);
                g.FillEllipse(new SolidBrush(color), rect);

                x = x + pas;
                if (x == limit)
                {
                    y = y + pas;
                    x = startx;
                }
                count--;
            }
            while (count != 0);
        }

        //add click event on player's 1 pits
        void SetPits1Enable()
        {
            for (int i = 0; i < 6; i++)
            {
                _pits[i].Click += new EventHandler(pit_Click);
                _pb[i].Click += new EventHandler(pit_Click);
            }
        }

        //add click event on player's 2 pits
        void SetPits2Enable()
        {
            for (int i = 7; i < 13; i++)
            {
                _pits[i].Click += new EventHandler(pit_Click);
                _pb[i].Click += new EventHandler(pit_Click);
            }
        }

        //remove click event on player's 1 pits (not clickable)
        void SetPits1Disable()
        {
            for (int i = 0; i < 6; i++)
            {
                _pits[i].Click -= new EventHandler(pit_Click);
                _pb[i].Click -= new EventHandler(pit_Click);
            }
        }

        //remove click event on player's 2 pits (not clickable)
        void SetPits2Disable()
        {
            for (int i = 7; i < 13; i++)
            {
                _pits[i].Click -= new EventHandler(pit_Click);
                _pb[i].Click -= new EventHandler(pit_Click);
            }
        }

        //remove click event on all pits and stores (not clickable)
        void SetAllPitsDisabled()
        {
            SetPits1Disable();
            SetPits2Disable();
        }

        //the game is ended when no rocks are on one player's side
        bool CheckEndGame()
        {
            //count how many pits with 0 rocks we find
            int count0s = 0;
            for (int i = 0; i < 6; i++)
            {
                if (GetPitCount(_pits[i]) == 0)
                {
                    count0s++;
                }
            }
            //if all the pits are empty, then move all the rocks from enemy's pits into his store
            if (count0s == 6)
            {
                int val = 0;
                for (int i = 7; i < 13; i++)
                {
                    //add the rocks and make the pits empty
                    val += GetPitCount(_pits[i]);
                    SetPitCount(_pits[i], 0);
                }
                //add to the store the rocks
                val += GetPitCount(_pits[13]);
                SetPitCount(_pits[13], val);
                return true;
            }

            //the same thing for the second player
            count0s = 0;
            for (int i = 7; i < 13; i++)
            {
                if (GetPitCount(_pits[i]) == 0)
                {
                    count0s++;
                }
            }
            if (count0s == 6)
            {
                int val = 0;
                for (int i = 0; i < 6; i++)
                {
                    val += GetPitCount(_pits[i]);
                    SetPitCount(_pits[i], 0);
                }
                val += GetPitCount(_pits[6]);
                SetPitCount(_pits[6], val);
                return true;
            }
            return false;
        }

        void EndGame()
        {
            SetAllPitsDisabled();
            nextBtn.Hide();
            if (GetPitCount(_pits[6]) > GetPitCount(_pits[13]))
            {
                LOG("Player1 wins!");
                MessageBox.Show($"Player1 wins!!!");
            }
            else if (GetPitCount(_pits[6]) < GetPitCount(_pits[13]))
            {
                LOG("Player2 wins!");
                MessageBox.Show($"Player2 wins!!!");
            }
            else
            {
                LOG("Draw!");
                MessageBox.Show($"Draw!!!");
            }
        }

        void LOG(string message)
        {
            logGameConsole.AppendText($"\n{message}");
        }
    }
}
