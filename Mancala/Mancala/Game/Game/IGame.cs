using Mancala.Game.Player;
using Mancala.Game.Board;
using System.Windows.Forms;

namespace Mancala
{
    public interface IGame
    {
        // 0 - first player, 1 - second player
        bool _turn { get; set; }
        IPlayer _player1 { get; set; }
        IPlayer _player2 { get; set; }
        Board _board { get; set; }

        void SetComponents(Label[] pits1, Label[] pits2);
        void Run();
        void End();
    }
}
