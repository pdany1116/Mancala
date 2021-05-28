using Mancala.Game.Board;
using Mancala.Game.Player;
using System.Windows.Forms;

namespace Mancala.Game
{
    class PvPGame : IGame
    {
        public bool _turn { get; set; }
        public IPlayer _player1 { get; set; }
        public IPlayer _player2 { get; set; }
        public Board.Board _board { get; set; }
        int turns = 0;
        public PvPGame() { }

        public void SetComponents(Label[] pits1, Label[] pits2)
        {
            _player1 = new Player.Player();
            _player2 = new Player.Player();
            _board = new Board.Board(pits1, pits2);
        }

        public void Run()
        {
            bool clicked = false;
            int count = 0;
            while (true)
            {
                //player 1
                if (turns % 2 == 0)
                {
                    _board.EnablePits1();
                    _board.DisablePits2();
                    while(!clicked)
                    {
                        foreach(Pit pit in _board._player1Pits)
                        {
                            if (pit.IsClicked())
                            {
                                clicked = true;
                                count = pit._count;
                                pit.SetCount(0);
                                break;
                            }
                        }
                        if(clicked)
                        {
                            break;
                        }
                    }
                    ShareCount(count);
                }
                //player 2
                else if (turns % 2 == 1)
                {
                    _board.EnablePits2();
                    _board.DisablePits1();
                    while (!clicked)
                    {
                        foreach (Pit pit in _board._player2Pits)
                        {
                            if (pit.IsClicked())
                            {
                                clicked = true;
                                count = pit._count;
                                pit.SetCount(0);
                                break;
                            }
                        }
                        if (clicked)
                        {
                            break;
                        }
                    }
                    ShareCount(count);
                }
                turns++;
            }
        }
        
        void ShareCount(int count)
        {
            _board._player1Pits[7]._count = 99;
        }

        public void End()
        {

        }
    }
}

