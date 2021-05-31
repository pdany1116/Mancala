using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Mancala
{
    public partial class PvPGameForm : Form
    {
        MenuForm _menuForm;
        int round = 1; //1 = first player, 2 = second player
        bool moved = false;
        Label[] pits; //pit6 - store for player 1, pit13 - store for player2
        PictureBox[] pb;
        int lastPitIndex = -1;
        Random rnd = new Random();

        public PvPGameForm(MenuForm menuForm)
        {
            InitializeComponent();
            _menuForm = menuForm;
            pits = new Label[14] { pit0, pit1, pit2, pit3, pit4, pit5, pit6, pit7, pit8, pit9, pit10, pit11, pit12, pit13 };
            pb = new PictureBox[14] { pit0pb, pit1pb, pit2pb, pit3pb, pit4pb, pit5pb, pit6pb, pit7pb, pit8pb, pit9pb, pit10pb, pit11pb, pit12pb, pit13pb };
            //put 4 rocks in every pit except stores at the beggining of the game
            for(int i = 0; i < pits.Length; i++)
            {
                if (i == 6 || i == 13)
                {
                    pits[i].Text = "0";
                }
                else
                {
                    pits[i].Text = "4";
                }
            }
            //set all to pits to be unclickable until Start Game
            SetAllPitsDisabled();
        }

        //event on clicking on the pit
        void pitClick(object sender, EventArgs e)
        {
            moved = true;
            Label label = sender as Label;
            PictureBox pb = sender as PictureBox;
            //if the click was on picture box, get the label name
            if (label == null)
            {
                string name = pb.Name.Remove(pb.Name.Length - 2, 2);
                label = this.Controls.Find(name, true).FirstOrDefault() as Label;
            }
            //make the count of rocks 0 in the clicked pit, and start sharing them to the next pits
            ShareCount(label, int.Parse(label.Text));
            //if the last rock was dropped in the player's store, permit another move, else disable all pits until the player hits Next 
            if (!(lastPitIndex == 6 || lastPitIndex == 13))
            {
                SetAllPitsDisabled();
            }
            else
            {
                moved = false;
            }
        }

        void DrawRocks(Label label, int count)
        {
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
            if(label == pits[6] || label == pits[13])
            {
                startx = 30;
                x = 30;
                y = 45;
            }


            //draw as many rocks as the count
            do
            {
                rect = new Rectangle(x, y, 10, 10);
                Color color = Color.FromArgb(rnd.Next(256), rnd.Next(256), rnd.Next(256));
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

        void ShareCount(Label label, int count)
        {
            bool passedStore = false;
            //set number of rocks in the clicked pit to 0
            label.Text = "0";
            //Draw 0 rocks in the clicked pit
            DrawRocks(label, 0);

            //get the index of the next pit after the clicked pit
            int nextPitIndex = int.Parse(label.Name.Remove(0, 3)) + 1;

            //start putting 1 rock in the other pits (as many as the clicked pit count)
            do
            {
                //we only have 14 pits so we do not want to overflow the number
                nextPitIndex = nextPitIndex % 14;

                //the rocks are not distributed in the opponent's store
                if (round % 2 == 1 && nextPitIndex == 13)
                {
                    nextPitIndex = 0;
                }
                else if (round % 2 == 0 && nextPitIndex == 6)
                {
                    nextPitIndex = 7;
                }
                if (round % 2 == 1 && nextPitIndex == 6)
                {
                    passedStore = true;
                }
                else if (round % 2 == 0 && nextPitIndex == 13)
                {
                    passedStore = true;
                }

                //add 1 rock to the next pit and substract it from the count of the clicked pit
                int val = int.Parse(pits[nextPitIndex].Text) + 1;
                //set the new number of rocks (actual + 1)
                pits[nextPitIndex].Text = val.ToString();
                //draw the number of rocks
                DrawRocks(pits[nextPitIndex], val);
                nextPitIndex++;
                count--;
            }
            while (count != 0);

            //get the pit where it was placed the last rock from the clicked pit
            lastPitIndex = nextPitIndex - 1;

            //if the last pit is 1 (it was 0 + the last rock), if it is not one of the stores and if it did not drop a rock in the store,
            // then get the opposite pit's rocks and put them in your store
            if (pits[lastPitIndex].Text == "1" && !(lastPitIndex == 6 || lastPitIndex == 13) && !passedStore)
            {
                //calculate the opposite pit
                int oppositeIndex = 12 - lastPitIndex;

                //if there are rocks in the opposite pit, add them to your store, along with the current 1 rock in the last pit
                if (pits[oppositeIndex].Text != "0") {
                    if (round % 2 == 1)
                    {
                        int val = int.Parse(pits[6].Text) + int.Parse(pits[oppositeIndex].Text) + int.Parse(pits[lastPitIndex].Text);
                        //add the rocks to player's 1 store
                        pits[6].Text = val.ToString();
                        DrawRocks(pits[6], val);
                    }
                    else
                    {
                        int val = int.Parse(pits[13].Text) + int.Parse(pits[oppositeIndex].Text) + int.Parse(pits[lastPitIndex].Text);
                        //add the rocks to player's 1 store
                        pits[13].Text = val.ToString();
                        DrawRocks(pits[13], val);
                    }
                    //set to 0 and draw 0 rocks in the last pit and it's opposite 
                    pits[oppositeIndex].Text = "0";
                    pits[lastPitIndex].Text = "0";
                    DrawRocks(pits[oppositeIndex], 0);
                    DrawRocks(pits[lastPitIndex], 0);
                }
            }
            //after each move, the end game is checked 
            if (CheckEndGame())
            {
                SetAllPitsDisabled();
                nextBtn.Hide();
            }
        }

        private void GameForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            _menuForm.Show();
        }

        //add click event on player's 1 pits
        void SetPits1Enable()
        {
            for (int i = 0; i < 6; i++)
            {
                pits[i].Click += new EventHandler(pitClick);
                pb[i].Click += new EventHandler(pitClick);
            }
        }
        //add click event on player's 2 pits
        void SetPits2Enable()
        {
            for (int i = 7; i < 13; i++)
            {
                pits[i].Click += new EventHandler(pitClick);
                pb[i].Click += new EventHandler(pitClick);
            }
        }
        //remove click event on player's 1 pits (not clickable)
        void SetPits1Disable()
        {
            for (int i = 0; i < 6; i++)
            {
                pits[i].Click -= new EventHandler(pitClick);
                pb[i].Click -= new EventHandler(pitClick);
            }
        }
        //remove click event on player's 2 pits (not clickable)
        void SetPits2Disable()
        {
            for (int i = 7; i < 13; i++)
            {
                pits[i].Click -= new EventHandler(pitClick);
                pb[i].Click -= new EventHandler(pitClick);
            }
        }

        ////remove click event on all pits and stores (not clickable)
        void SetAllPitsDisabled()
        {
            for (int i = 0; i < pits.Length; i++)
            {
                pits[i].Click -= new EventHandler(pitClick);
                pb[i].Click -= new EventHandler(pitClick);
            }
        }

        //the game is ended when no rocks are on one player's side
        bool CheckEndGame()
        {            
            //count how many pits with 0 rocks we find
            int count0s = 0;
            for (int i = 0; i < 6; i++)
            {
                if(pits[i].Text == "0")
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
                    val += int.Parse(pits[i].Text);
                    pits[i].Text = "0";
                    DrawRocks(pits[i], 0);
                }
                //add to the store the rocks
                val += int.Parse(pits[13].Text);
                pits[13].Text = val.ToString();
                MessageBox.Show("No more rocks on player's 1 side! Game ending...");
                return true;
            }

            //the same thing for the second player
            count0s = 0;
            for (int i = 7; i < 13; i++)
            {
                if (pits[i].Text == "0")
                {
                    count0s++;
                }
            }
            if (count0s == 6)
            {
                int val = 0;
                for (int i = 0; i < 6; i++)
                {
                    val += int.Parse(pits[i].Text);
                    pits[i].Text = "0";
                    DrawRocks(pits[i], 0);
                }
                val += int.Parse(pits[6].Text);
                pits[6].Text = val.ToString();
                MessageBox.Show("No more rocks on player's 2 side! Game ending...");                
                return true;
            }
            return false;
        }

        //this event is called when a player moved and he ended his turn by pressing Next
        private void next_Click(object sender, EventArgs e)
        {
            if (!moved)
            {
                MessageBox.Show("Please make a move!");
                return;
            }
            //we count the rounds (par - player 1, impar - player 2)
            round++;
            if (round % 2 == 1)
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
            moved = false;
        }

        private void startGameBtn_Click(object sender, EventArgs e)
        {
            //draw 4 rocks in each pit at the start of the game
            for (int i = 0; i < pits.Length; i++)
            {
                if (!(i == 6 || i == 13))
                {
                    DrawRocks(pits[i], 4);
                }
            }
            //start the game by making the player's 1 pits available to click
            startGameBtn.Hide();
            SetPits1Enable();
        }
    }
}
