using System.Collections.Generic;
using System.Windows.Forms;

namespace Mancala.Game.Board
{
    public class Board
    {
        // the 7th Pit is the STORE(Mancala)
        public Pit[] _player1Pits = new Pit[7];
        public Pit[] _player2Pits = new Pit[7];

        public Board(Label[] pits1, Label[] pits2)
        {
            for (int i = 0; i < _player1Pits.Length; i++)
            {
                _player1Pits[i] = new Pit(pits1[i]);
            }
            for (int i = 0; i < _player2Pits.Length; i++)
            {
                _player2Pits[i] = new Pit(pits2[i]);
            }
        }

        public void DisablePits1()
        {
            for (int i = 0; i < _player1Pits.Length; i++)
            {
                _player1Pits[i].DisableClick();
            }
        }

        public void DisablePits2()
        {
            for (int i = 0; i < _player2Pits.Length; i++)
            {
                _player2Pits[i].DisableClick();
            }
        }

        public void EnablePits1()
        {
            for (int i = 0; i < _player1Pits.Length; i++)
            {
                _player1Pits[i].EnableClick();
            }
        }

        public void EnablePits2()
        {
            for (int i = 0; i < _player2Pits.Length; i++)
            {
                _player2Pits[i].EnableClick();
            }
        }
    }
}

