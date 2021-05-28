using Mancala.Game.Player;
using System.Windows.Forms;

namespace Mancala.Game
{
    class AIGame : IGame
    {
        public IPlayer _player1 { get; set; }
        public IPlayer _player2 { get; set; }
        public Board.Board _board { get; set; }
        public bool _turn { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }

        public AIGame()
        {
            _player1 = new Player.AIBot();
            _player2 = new Player.Player();
            //_board = new Board.Board();
        }

        public void End()
        {

        }

        public void Run()
        {

        }

        public void SetComponents(Label[] pits1, Label[] pits2)
        {
            throw new System.NotImplementedException();
        }
    }
}
